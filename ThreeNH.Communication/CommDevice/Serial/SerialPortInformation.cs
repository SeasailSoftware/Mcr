using System;
using System.Management;

namespace ThreeNH.Communication.CommDevice.Serial
{
    public class SerialPortInformation
    {
        public SerialPortInformation()
        {
            // do nothing
        }

        /// <summary>
        /// 用给定串口的信息构造一个实例
        /// </summary>
        /// <param name="portName">串口ID号（COM + 数字）</param>
        public SerialPortInformation(string portName)
        {
            PortName = portName;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\cimv2",
                "SELECT * FROM Win32_PnPEntity WHERE ClassGuid=\"{4d36e978-e325-11ce-bfc1-08002be10318}\"");

            portName = portName.ToUpper();
            foreach (ManagementObject queryObj in searcher.Get())
            {
                string caption = queryObj["Caption"].ToString().ToUpper();
                if (string.Compare(portName, queryObj["DeviceId"].ToString()) == 0 || caption.Contains(portName))
                {
                    DeviceId = queryObj["DeviceId"].ToString();
                    PortName = portName;
                    Description = queryObj["Description"].ToString();
                    Caption = queryObj["Caption"].ToString();
                    break;
                }
            }
        }
        public string DeviceId { get; set; }
        public string PortName { get; set; }
        public string Description { get; set; }
        public string Caption { get; set; }
        //public string PNPDeviceID;
        //public uint MaxBaudRate;

        // 是否是蓝牙狗设备描述符
        public bool IsBluetoothDonglePort()
        {
            return string.Compare(Description, "USB-SERIAL CH340", StringComparison.CurrentCultureIgnoreCase) == 0;
        }
        // 是否是STM32平台设备
        public bool IsSTM32VirtualPort()
        {
            return string.Compare(Description, "STMicroelectronics Virtual COM Port", StringComparison.CurrentCultureIgnoreCase) == 0;
        }
        // 是否是展讯平台
        public bool IsSCIUsb2Serial()
        {
            return string.Compare(Description, "SCI USB2Serial", StringComparison.CurrentCultureIgnoreCase) == 0;
        }
    }
}
