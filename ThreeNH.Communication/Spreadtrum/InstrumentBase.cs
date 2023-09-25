using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Threading;
using ThreeNH.Communication;
using ThreeNH.Communication.CommDevice;
using ThreeNH.Communication.CommDevice.Serial;
using ThreeNH.Communication.EhMc17;
using ThreeNH.Communication.Spreadtrum;

namespace ThreeNH.Communication.Spreadtrum
{
    public class InstrumentBase
    {
        public const string SPREADTRUM_VCP_DESCRIPTION = "SCI USB2Serial";

        private byte _waitingCommand = 0;
        private int _isBusy = 0;
        private DataPacket _receivedPacket = null;
        private EventWaitHandle _receivedPacketEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
        private Mutex _receivedPacketMutex = new Mutex();
        private List<byte> _buffer = new List<byte>();

        private ICommDevice _device;

        // 是否是蓝牙狗设备描述符
        public static bool IsBluetoothDonglePort(string description)
        {
            return string.Compare(description, "USB-SERIAL CH340", StringComparison.CurrentCultureIgnoreCase) == 0;
        }
        // 是否是展讯平台
        public static bool IsSCIUsb2Serial(string description)
        {
            return string.Compare(description, "SCI USB2Serial", StringComparison.CurrentCultureIgnoreCase) == 0;
        }

        public InstrumentBase()
        {
            _device = new SerialPortDevice();
        }

        ~InstrumentBase()
        {
            try
            {
                Close();
            }
            catch
            {
                //
            }
        }

        public ICommDevice CommunicationDevice
        {
            get { return _device; }
            set
            {
                if (IsOpen)
                {
                    throw new CommunicationException(CommunicationError.DeviceHasOpened);
                }

                _device = value;
                if (_device.IsOpen)
                {
                    _device.DiscardInBuffer();
                    _device.DataReceived += OnDataReceived;
                }

                DeviceName = _device.DeviceName;
            }
        }

        public ICommDevice DetachCommunicationDevice()
        {
            if (_device != null)
            {
                _device.DataReceived -= OnDataReceived;
            }

            var tmp = _device;
            _device = null;
            return tmp;
        }

        public string DeviceName { get; protected set; }

        public bool IsOpen
        {
            get
            {
                return _device.IsOpen;
            }
            set { }
        }

        public virtual void Open(object device = null)
        {
            if (device is ICommDevice commDevice)
            {
                CommunicationDevice = commDevice;
                return;
            }

            if (IsOpen)
            {
                throw new CommunicationException(CommunicationError.DeviceHasOpened);
            }

            _isBusy = 0;

            if (device == null)
            {
                foreach (var portInfo in  SerialPortService.SerialPorts)
                {
                    ////if (string.Compare(info.Description, SPREADTRUM_VCP_DESCRIPTION, StringComparison.OrdinalIgnoreCase) != 0)
                    ////{
                    ////    continue;
                    ////}
                    try
                    {
                        _device.Open(portInfo.PortName);
                    }
                    catch (CommunicationException)
                    {
                        throw;
                    }
                    catch (Exception e)
                    {
                        throw new CommunicationException(CommunicationError.DeviceOpenFailed, e.Message);
                    }
                }
            }
            else
            {
                try
                {
                
                    _device.Open(device);
                    _device.DiscardInBuffer();
                }
                catch (IOException)
                {
                    throw new CommunicationException(CommunicationError.DeviceNotFound);
                }
            }

            DeviceName = _device.DeviceName;
            _device.DataReceived += OnDataReceived;
              
        }

        public bool IsBusy
        {
            get { return _isBusy != 0; }
        }

        public virtual void Close()
        {
            if (_device == null)
                return;

            if (_waitingCommand != 0)
            {
                _receivedPacketEvent.Set();
                _receivedPacketMutex.WaitOne();
                _waitingCommand = 0;
                _receivedPacket = null;
                _buffer.Clear();
                _receivedPacketMutex.ReleaseMutex();
            }

            try
            {
                _isBusy = 0;
                _device.Close();
            }
            catch
            {
                // do nothing
            }
        }

        /// <summary>
        /// 发送一个命令
        /// </summary>
        /// <param name="command">要发送的命令</param>
        /// <param name="content">命令附加的数据</param>
        /// <param name="ack">发送给仪器的应答，只有某些命令需要</param>
        /// <param name="waitingAck">是否等待应答命令返回</param>
        /// <param name="timeout">等待超时时间，单位为毫秒</param>
        /// <returns>如果等waitingAck为true且发送的命令的应答命令不为空，等待应答命令返回</returns>
        public DataPacket SendCommand(byte command, PacketContent content = null, byte? ack = null, bool waitingAck = true, int timeout=10000)
        {
            if (!IsOpen)
            {
                throw new CommunicationException(CommunicationError.DeviceNotOpen);
            }

            if (Interlocked.CompareExchange(ref _isBusy, 1, 0) != 0)
            {
                throw new CommunicationException(CommunicationError.DeviceBusy);
            }

            _waitingCommand = waitingAck ? AckCommand(command) : (byte)0;
            _receivedPacketEvent.Reset();

            DataPacket packet = new DataPacket();
            packet.Command = command;
            packet.PacketContent = content;
            packet.Ack = ack;
            try
            {
                var data = packet.ToBytes();
                _device.Write(data, 0, data.Length);
            }
            catch
            {
                _waitingCommand = 0;
                Interlocked.Decrement(ref _isBusy);
                throw;
            }

            if (_waitingCommand == 0)
            {
                Interlocked.Decrement(ref _isBusy);
                return null;
            }

            if (_receivedPacketEvent.WaitOne(timeout))
            {
                _receivedPacketMutex.WaitOne();
                _waitingCommand = 0;

                if (_receivedPacket == null)
                {
                    _receivedPacketMutex.ReleaseMutex();
                    Interlocked.Decrement(ref _isBusy);
                    throw new CommunicationException(CommunicationError.DeviceDisconnected);
                }

                var result = _receivedPacket;
                _receivedPacket = null;
                _receivedPacketMutex.ReleaseMutex();
                Interlocked.Decrement(ref _isBusy);
#if TRACE
                Trace.TraceInformation("[{0}]in SendCommand _isBusy = {1}", this,  _isBusy);
#endif
                return result;
            }
            _waitingCommand = 0;
            Interlocked.Decrement(ref _isBusy);
            throw new CommunicationException(CommunicationError.ReceivingTimeout);
        }


        protected virtual PacketParser PacketParser
        {
            get { throw new NotImplementedException(); }
        }

        //        private void OnDataReceived(object sender, byte[] data, Exception ex)
        private void OnDataReceived(ICommDevice sender, DataType datatype, Exception ex)
        {
            Interlocked.Increment(ref _isBusy);

            if (datatype == DataType.Eof)
            {
                Interlocked.Decrement(ref _isBusy);
//                Close();
                return;
            }

            if (datatype == DataType.Error)
            {
                throw ex;
            }

            byte[] data = new byte[sender.BytesToRead];
            sender.Read(data, 0, data.Length);

            _buffer.AddRange(data);
            DataPacket result = null;
            try
            {
#if TRACE
                Trace.TraceInformation("[{0}]准备角析数据...", this);
#endif
                int n = PacketParser.Parse(_buffer.ToArray(), 0, _buffer.Count);
#if TRACE
                Trace.TraceInformation("[{0}]解析返回", this);
#endif
                _buffer.RemoveRange(0, n);  // 移除已解析的数据

                if (PacketParser.IsCompleted)
                {
#if TRACE
                    Trace.TraceInformation("[{0}]解析完成", this);
#endif
                    result = PacketParser.Result;
                    PacketParser.Reset();       // 重置解析器

                    if (_waitingCommand == result.Command)
                    {
                        _receivedPacketMutex.WaitOne();
                        if (_receivedPacket != null)
                        {
                            Trace.TraceWarning("[{0}] in OnDataReceived, _receivedPacket != null", this);
                        }

                        _receivedPacket = result;
                        _receivedPacketMutex.ReleaseMutex();
                        Interlocked.Decrement(ref _isBusy);
#if TRACE
                        Trace.TraceInformation("[{0}]in OnDataReceived _isBusy = {1}", this, _isBusy);
#endif
                        _receivedPacketEvent.Set();
                    }
                    else
                    {
                        Interlocked.Decrement(ref _isBusy);
                        OnPacketArrived(result);
                    }
                }
                else
                {
                    Interlocked.Decrement(ref _isBusy);
                }
            }
            catch(Exception pex)
            {
                Trace.TraceError("[{0}]解析出错：{1}\n{2}", this, pex.Message, pex.StackTrace);
                Interlocked.Decrement(ref _isBusy);
            }
        }

        // 当收到仪器发送过来的命令时调用此函数
        // 该函数可能在辅助线程中执行
        protected virtual void OnPacketArrived(DataPacket packet, Exception ex = null)
        {
            // do nothing
        }

        /// <summary>
        /// 获取命令的应答命令
        /// </summary>
        /// <param name="command">发送给仪器的命令</param>
        /// <returns>仪器应答的命令；如果没有应答命令返回0</returns>
        protected virtual byte AckCommand(byte command)
        {
            throw new NotImplementedException();
        }
    }
}
