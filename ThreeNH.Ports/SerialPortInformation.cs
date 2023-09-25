using System;
using System.Linq;
using System.Management;

namespace ThreeNH.Ports
{
    public class SerialPortInformation
    {
        public SerialPortInformation()
        {
            // do nothing
        }

        public static readonly string[] AdaptivePorts =
        {
            "SCI USB2Serial",
            "STMicroelectronics Virtual COM Port",
            "USB-SERIAL CH340"
        };

        public bool IsAdaptivePort => AdaptivePorts.Contains(Description);

        public bool IsEhMc17At => Description.CompareTo(AdaptivePorts[2]) == 0;

        /// <summary>
        /// 用给定串口的信息构造一个实例
        /// </summary>
        /// <param name="portName">串口ID号（COM + 数字）</param>
        public SerialPortInformation(string portName)
        {
            PortName = portName;
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\cimv2", "SELECT * FROM Win32_PnPEntity WHERE ClassGuid=\"{4d36e978-e325-11ce-bfc1-08002be10318}\""))
            {
                portName = portName.ToUpper();
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    string caption = queryObj["Caption"].ToString().ToUpper();
                    if (string.Compare(portName, queryObj["DeviceID"].ToString()) == 0 || caption.Contains(portName))
                    {
                        PortName = queryObj["DeviceID"].ToString();
                        Description = queryObj["Description"].ToString();
                        Caption = queryObj["Caption"].ToString();
                        break;
                    }
                }
            }
        }

        public string PortName { get; set; }
        public string Description { get; set; }
        public string Caption { get; set; }
        //public string PNPDeviceID;
        //public uint MaxBaudRate;
    }
}
