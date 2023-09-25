using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using ThreeNH.Color.Model;
using ThreeNH.Communication;
using ThreeNH.Communication.STM32;
using ThreeNH.Communication.Utility;
using ThreeNH.Reflection;

namespace ThreeNH.Instrument
{

    /// <summary>
    /// 命令处理代理
    /// </summary>
    /// <param name="command">接收到的命令</param>
    /// <param name="data">接收到的数据</param>
    /// <param name="remoteError">远端传过来的错误码</param>
    /// <returns>返回真表示已处理该命令，假表示未处理</returns>
    delegate bool CommandHandler(byte command, byte[] data, int remoteError, bool isLast);
    public class MicroColorReader : ClientProtocol, IInstrument
    {
        private const int _defaultTimeout = 5000;
        private ManualResetEvent _connectedEvent = new ManualResetEvent(false);
        private AutoResetEvent _ackEvent = new AutoResetEvent(false);
        private AutoResetEvent _recvEvent = new AutoResetEvent(false);
        private int _commError = 0;
        private Mutex _mutex = new Mutex(false);
        private byte _currentCommand = 0; // 当前正在接收的命令
        private Command _waitingCommand = Command.InvalidCommand; // 当前正在等待的命令
        private List<byte> _recvBuffer = new List<byte>();
        private Dictionary<byte, CommandHandler> _commandHandlers = new Dictionary<byte, CommandHandler>();
        private UploadSpecimenAsyncContext _uploadSpecimenAsyncContext;
        public IDictionary<string, object> InstrumentInformation { get; private set; } = new Dictionary<string, object>();



        public MicroColorReader()
        {
            Connected += OnSessionConnected;
            Disconnected += OnSessionDisconnected;
            Received += OnDataReceived;
            _commandHandlers[(byte)Command.MeasureStandard] = HandleMeasureCommand;
            _commandHandlers[(byte)Command.MeasureTrial] = HandleMeasureCommand;
        }

        private bool HandleMeasureCommand(byte command, byte[] data, int remoteError, bool isLast)
        {
            if (_uploadSpecimenAsyncContext == null)
            {
                return false;
            }

            lock (_uploadSpecimenAsyncContext)
            {
                if (remoteError != 0)
                {
                    _uploadSpecimenAsyncContext.Handler.BeginInvoke(this,
                        new SpecimenReceivedEventArgs(new CommunicationException(
                            CommunicationError.ExecuteCommandFailed,
                            $"Error Code: {remoteError}")), null, null);
                }
                else
                {
                    _uploadSpecimenAsyncContext.Data.AddRange(data);
                    int recordSize = command == (byte)Command.UploadStandards
                        ? Marshal.SizeOf(typeof(StandardRecord))
                        : Marshal.SizeOf(typeof(TrialRecord));
                    List<object> list = new List<object>();
                    for (int i = 0; i * recordSize < _uploadSpecimenAsyncContext.Data.Count; i++)
                    {
                        if ((byte)Command.UploadStandards == command)
                        {
                            list.Add(StructConverter
                                .BytesToStruct<StandardRecord>(_uploadSpecimenAsyncContext.Data, i * recordSize));
                        }
                        else
                        {
                            list.Add(StructConverter
                                .BytesToStruct<TrialRecord>(_uploadSpecimenAsyncContext.Data, i * recordSize));
                        }
                    }

                    if (list.Count > 0)
                    {
                        _uploadSpecimenAsyncContext.Data.RemoveRange(0, list.Count * recordSize);
                        _uploadSpecimenAsyncContext.Handler.BeginInvoke(this,
                            new SpecimenReceivedEventArgs(list.ToArray(), !isLast), null, null);
                    }
                }

                if (isLast)
                {
                    _uploadSpecimenAsyncContext = null;
                }

                return true;
            }
        }

        private void OnDataReceived(object sender, ReceivedEventArgs e)
        {
            if (e.Operation == 0)
            {
                return;
            }

            //            Trace.TraceInformation($"接收到：{(Command)e.Operation}, offset = {e.Offset}, length = {e.Data.Length}");

            if (e.Offset == 0)
            {
                _currentCommand = e.Operation;
            }

            if (e.Data != null && e.Data.Length > 0)
            {
                lock (_recvBuffer)
                {
                    _recvBuffer.AddRange(e.Data);
                }
            }

            if (!_commandHandlers.ContainsKey(e.Operation) ||
                !_commandHandlers[e.Operation](e.Operation, e.Data, e.RemoteErrorCode, e.IsLast))
            {
                if (e.IsLast || e.Data == null)
                {
                    _commError = e.RemoteErrorCode;
                    _ackEvent.Set();
                }

                _recvEvent.Set();
            }
        }

        private void OnSessionDisconnected(object sender, EventArgs e)
        {
            _ackEvent.Set();
        }

        private void OnSessionConnected(object sender, ConnectedEventArgs e)
        {
            _connectedEvent.Set();
        }

        public void CalibrateBlack()
        {
            throw new NotImplementedException();
        }

        public void CalibrateWhite()
        {
            SendAndWait(Commands.Calibrate(true), 10000);
            SendAndWait(Commands.Calibrate(false), 10000);
        }

        public Spectrum GetWhiteboardData()
        {
            var result = SendAndWait(Commands.GetProductInfo(ProductInfoField.ALL));
            if (result.Data.Length == Marshal.SizeOf(typeof(ProductInfo)))
            {
                var product = StructConverter.BytesToStruct<ProductInfo>(result.Data);
                return product.WhiteboardData;
            }

            throw new CommunicationException(CommunicationError.ParsedBadData);
        }



        public new bool IsOpen => base.IsConnected;


        public ISample Measure(bool standard = false)
        {
            var result = SendAndWait(Commands.Measure(standard, null), 30000);
            var sample = new Sample();
            var record = StructConverter.BytesToStruct<StandardRecord>(result.Data);
            sample.Id = Guid.NewGuid();
            sample.DateTime = DateTime.Now;
            sample.Spectrum = new Color.Model.Spectrum(new Color.Model.WavelengthRange(Constants.VALID_WAVE_BEGIN, Constants.VALID_WAVE_END, Constants.VALID_WAVE_STEP), record.SciSpectrum);
            sample.Temperature = record.Temperature;
            return sample;
        }

        public void Open(object device = null)
        {
            try
            {
                base.Open(device as string);
                Connect();
                if (IsConnected)
                {
                    Thread.Sleep(1000);
                    var devInfo = GetDeviceInfo();
                    InstrumentInformation.Add(InstrumentInformationKey.CommunicationDeviceName, device as string);
                    InstrumentInformation.Add(InstrumentInformationKey.InstrumentModel, devInfo.Model);
                    InstrumentInformation.Add(InstrumentInformationKey.Optical, devInfo.Optical);
                    InstrumentInformation.Add(InstrumentInformationKey.SerialNumber, devInfo.Sn);
                    InstrumentInformation.Add(InstrumentInformationKey.SoftwareVersion, devInfo.SoftwareVersion);
                    InstrumentInformation.Add(InstrumentInformationKey.HardwareVersion, devInfo.HardwareVersion);
                    InstrumentInformation.Add(InstrumentInformationKey.WhiteBoardNumber, devInfo.WhiteBoardNumber);
                }
            }
            catch (Exception ex)
            {
                ThreeNH.Logging.LogManager.Error(ex);
            }
        }

        public DeviceInfo GetDeviceInfo()
        {
            var cmd = Commands.GetDeviceInfo();
            Send(cmd);
            var result = Wait(cmd.Command, 5000);
            if (result == null)
            {
                throw new CommunicationException(CommunicationError.ReceivingTimeout);
            }

            if (result.Data == null)
            {
                throw new CommunicationException(CommunicationError.ExecuteCommandFailed);
            }

            return StructConverter.BytesToStruct<DeviceInfo>(result.Data);
        }

        public void Send(CommandPacket packet)
        {
            if (!_mutex.WaitOne(0))
            {
                throw new CommunicationException(CommunicationError.DeviceBusy);
            }

            try
            {
                if (!IsConnected)
                {
                    throw new CommunicationException(CommunicationError.DeviceDisconnected);
                }

                _recvBuffer.Clear();
                _ackEvent.Reset();
                _commError = 0;
                _currentCommand = 0;
                if (!Send((byte)packet.Command, null, packet.Data))
                {
                    throw new CommunicationException(CommunicationError.DeviceBusy);
                }
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        public CommandPacket Wait(byte command = 0, int timeout = Int32.MaxValue)
        {
            _waitingCommand = (Command)command;
            while (_ackEvent.WaitOne(timeout) && IsConnected)
            {
                _mutex.WaitOne();
                try
                {
                    if (command == 0 || command == _currentCommand)
                    {
                        var result = new CommandPacket(_currentCommand);
                        result.AckCode = _commError;
                        lock (_recvBuffer)
                        {
                            if (_commError == 0 && _recvBuffer.Count > 0)
                            {
                                result.Data = _recvBuffer.ToArray();
                                _recvBuffer.Clear();
                            }
                        }

                        return result;
                    }
                }
                finally
                {
                    _mutex.ReleaseMutex();
                    _waitingCommand = Command.InvalidCommand;
                }
            }

            _currentCommand = 0;
            _waitingCommand = Command.InvalidCommand;
            return null;
        }

        private CommandPacket SendAndWait(CommandPacket packet, int timeout = 5000)
        {
            Send(packet);
            var result = Wait(packet.Command, timeout);
            if (result == null)
            {
                throw new CommunicationException(CommunicationError.ReceivingTimeout);
            }

            if (result.AckCode != 0)
            {

                throw new CommunicationException(((RemoteError)result.AckCode).ToCommError());
            }

            if (!IsOpen)
            {
                // 连接已断开
                throw new CommunicationException(CommunicationError.DeviceDisconnected);
            }

            return result;
        }
    }

    class UploadSpecimenAsyncContext
    {
        public List<byte> Data = new List<byte>();
        public EventHandler<SpecimenReceivedEventArgs> Handler;
    }

    public class SpecimenReceivedEventArgs : EventArgs
    {
        public SpecimenReceivedEventArgs(object[] specimens, bool hasMore = false)
        {
            Specimens = specimens;
            HasMore = hasMore;
        }
        public SpecimenReceivedEventArgs(Exception ex)
        {
            Exception = ex;
        }
        public SpecimenReceivedEventArgs(object specimen)
        {
            Specimens = new object[] { specimen };
        }

        /// <summary>
        /// 接数到的样品数据
        /// </summary>
        public object[] Specimens { get; set; }
        /// <summary>
        /// 接收到的异常；当Specimens为null时可能会附带异常数据
        /// </summary>
        public Exception Exception { get; set; }
        /// <summary>
        /// 指示是否有更多数据
        /// </summary>
        public bool HasMore { get; set; }
    }
}
