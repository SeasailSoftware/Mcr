using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using ThreeNH.Communication.CommDevice.Bluetooth;

namespace ThreeNH.Communication.CommDevice
{
    public class BluetoothDevice : ICommDevice
    {

        public const string _bluetoothDeviceName = "3nh";
        private const int _port = 1;

        private event DataReceivedEventHandler _dataReceivedEventHandle;

        private BluetoothClient _bluetooth = null;
        private IAsyncResult _asyncResult = null;
        private List<ArraySegment<byte>> _buffer = new List<ArraySegment<byte>>();
        private BthEndPoint _endPoint = null;

        public string DeviceName
        {
            get
            {
                return IsOpen ? _bluetooth.RemoteMachineName : null;
            }
        }

        public bool IsOpen
        {
            get
            {
                return _bluetooth != null && (_bluetooth.Connected || _endPoint != null);
            }
        }

        public int BytesToRead => _bluetooth.Client.Available;

        public int BytesToWrite => 0;

        public void DiscardInBuffer()
        {
            // do nothing
        }

        public void DiscardOutBuffer()
        {
            // do nothing
        }

        public int ReadTimeout
        {
            get { return _bluetooth.Client.ReceiveTimeout; }
            set { _bluetooth.Client.ReceiveTimeout = value; }
        }

        public int WriteTimeout
        {
            get { return _bluetooth.Client.SendTimeout;}
            set { _bluetooth.Client.SendTimeout = value; }
        }

        public void Close()
        {
            _bluetooth.Close();
            _bluetooth = null;
            _endPoint = null;
        }

        /// <summary>
        /// 打开蓝牙设备
        /// </summary>
        /// <param name="device">蓝牙地址，类型为BluetoothAddress</param>
        public void Open(object device = null)
        {
            _bluetooth = new BluetoothClient();
            BthEndPoint endPoint = null;

            if (device is BluetoothDeviceInfo)
            {
                var address = device as BluetoothDeviceInfo;
                endPoint = new BthEndPoint(address.Address, 1);
            }
            else if (device is string)
            {
                if (!BthEndPoint.TryParse(device as string, out endPoint))
                    endPoint = null;
            }
            else if (device is BthEndPoint)
            {
                endPoint = device as BthEndPoint;
            }
            else if (device is BluetoothAddress)
            {
                endPoint = new BthEndPoint((BluetoothAddress)device, 1);
            }

            if (endPoint == null)
            {
                BluetoothDetector detector = new BluetoothDetector(true, true, true, true, false);
                var deviceInfo = detector.SelectDevice();
                if (deviceInfo == null)
                {
                    throw new CommunicationException(CommunicationError.DeviceOpenFailed, "Connection was canceled by user");
                }
                endPoint = new BthEndPoint(deviceInfo.Address, _port);
            }

            try
            {
                _bluetooth.Connect(endPoint);
                _endPoint = endPoint;
                if (_dataReceivedEventHandle != null)
                {
                    _bluetooth.Client.BeginReceive(_buffer, SocketFlags.None, ReceiveCallback, null);
                }
            }
            catch (Exception ex)
            {
                _bluetooth = null;
                _endPoint = null;
                throw new CommunicationException(CommunicationError.DeviceOpenFailed, ex.Message);
            }
            
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            return _bluetooth.Client.Receive(buffer, offset, count, SocketFlags.None);
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            _bluetooth.Client.Send(buffer, offset, count, SocketFlags.None);
        }

        public event DataReceivedEventHandler DataReceived
        {
            add
            {
                if (_dataReceivedEventHandle == null && _bluetooth.Connected)
                {
                    _asyncResult = _bluetooth.Client.BeginReceive(_buffer, SocketFlags.None, ReceiveCallback, _bluetooth.Client);
                }

                _dataReceivedEventHandle += value;
            }
            remove
            {
                _dataReceivedEventHandle -= value;
                if (_dataReceivedEventHandle == null)
                {
                    _bluetooth.Close();
                }
            }
        }


        private void ReceiveCallback(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;

            // 连接是否已断开
            int n = _bluetooth.Client.EndReceive(_asyncResult, out var errorCode);
            if (errorCode != SocketError.Success)
            {
                Trace.TraceError("Socket error: {0}", errorCode.ToString());
                _dataReceivedEventHandle?.Invoke(this, DataType.Error,
                    new CommunicationException(CommunicationError.SystemException, errorCode.ToString()));
            }
            else
            {
                
            }
        }

        public void Dispose()
        {
            _bluetooth.Close();
        }
    }
}
