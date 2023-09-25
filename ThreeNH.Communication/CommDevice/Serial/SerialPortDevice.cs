using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;

namespace ThreeNH.Communication.CommDevice.Serial
{
    /// <summary>
    /// 连接参数
    /// </summary>
    public class SerialPortParameters
    {
        public string PortName { get; set; }
        public int BaudsRate { get; set; }
        public Parity Parity { get; set; } = Parity.None;
        public StopBits StopBits { get; set; } = StopBits.One;
        public int DataBits { get; set; } = 8;
        public bool RtsEnable { get; set; } = false;
    }

    /// <summary>
    /// 串口设备
    /// </summary>
    public class SerialPortDevice : ICommDevice
    {
        private event DataReceivedEventHandler _dataReceivedHandle;
        private SerialPort _serialPort;

        public SerialPortDevice(SerialPortParameters parameters = null)
        {
            _serialPort = new SerialPort();
            if (parameters != null)
            {
                SetParameters(parameters);
            }
        }

        public SerialPortDevice(string portName)
        {
            _serialPort = new SerialPort(portName);
        }

        public void SetParameters(SerialPortParameters parameters)
        {
            _serialPort.PortName = parameters.PortName;
            if (parameters.BaudsRate > 0)
            {
                _serialPort.BaudRate = parameters.BaudsRate;
            }

            if (parameters.DataBits > 0)
            {
                _serialPort.DataBits = parameters.DataBits;
            }

            _serialPort.Parity = parameters.Parity;
            _serialPort.StopBits = parameters.StopBits;
            _serialPort.RtsEnable = parameters.RtsEnable;
        }


        public void Open(object device = null)
        {
            if (_serialPort.IsOpen) return;
            if (device is SerialPortParameters parameters)
            {
                SetParameters(parameters);
            }
            else if (device is string portName)
            {
                _serialPort.PortName = portName;
                _serialPort.BaudRate = 115200;
            }
            _serialPort.Open();
        }

        public void Close()
        {
            try
            {
                if (_serialPort != null && _serialPort.IsOpen)
                    _serialPort.Close();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
        }

        public string DeviceName => _serialPort.PortName;
        public bool IsOpen => _serialPort.IsOpen;
        public int BytesToRead => _serialPort.BytesToRead;
        public int BytesToWrite => _serialPort.BytesToWrite;
        public void DiscardInBuffer()
        {
            _serialPort.DiscardInBuffer();
        }

        public void DiscardOutBuffer()
        {
            _serialPort.DiscardOutBuffer();
        }

        public int ReadTimeout
        {
            get => _serialPort.ReadTimeout;
            set => _serialPort.ReadTimeout = value;
        }

        public int WriteTimeout
        {
            get => _serialPort.WriteTimeout;
            set => _serialPort.WriteTimeout = value;
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            return _serialPort.Read(buffer, offset, count);
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            try
            {
                if (_serialPort != null && _serialPort.IsOpen)
                    _serialPort.Write(buffer, offset, count);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
        }

        public event DataReceivedEventHandler DataReceived
        {
            add
            {
                if (_dataReceivedHandle == null)
                {
                    _serialPort.DataReceived += OnDataReceived;
                    _serialPort.ErrorReceived += OnErrorReceived;
                }

                _dataReceivedHandle += value;
            }
            remove
            {
                _dataReceivedHandle -= value;
                if (_dataReceivedHandle == null)
                {
                    _serialPort.DataReceived -= OnDataReceived;
                    _serialPort.ErrorReceived -= OnErrorReceived;
                }
            }
        }

        private void OnErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            Trace.TraceError("Serial port error: {0}", e.EventType);
            _dataReceivedHandle?.Invoke(this, DataType.Error,
                new CommunicationException(CommunicationError.SystemException,
                    $"Serial port error: {e.EventType}"));
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            _dataReceivedHandle?.Invoke(this, e.EventType == SerialData.Chars ?
                DataType.Bytes : DataType.Eof, null);
        }

        public void Dispose()
        {
            _serialPort?.Dispose();
        }
    }
}
