using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeNH.Communication.CommDevice;
using ThreeNH.Communication.CommDevice.Serial;
using ThreeNH.Communication.Detector;

namespace ThreeNH.Communication.EhMc17
{
    public class EhMc17Detector : EhMc17BluetoothCommunication, IDetector
    {
        const string HAZE_METER_PNP_ID = "TNH-HZ"; // 雾度仪
        const string COLOR_HAZE_METER_PNP_ID = "CLR-HZ"; // 色彩雾度仪
        const string MAS_COLOR_METER_PNP_ID = "TNH MAS"; // 色彩雾度仪
        SerialPortDevice _commDevice;
        public EhMc17Detector(SerialPortDevice commDevice)
        {
            _commDevice = commDevice;
        }
        public InstrumentType Detect()
        {
            try
            {
                var serialPort = new SerialPort(_commDevice.DeviceName, 115200);
                serialPort.Open();
                base.AttatchSerialPort(serialPort);
                var pnpId = GetPnpId().Trim();
                base.DetachSerialPort();
                Trace.TraceInformation("GetPnpID:{0}, LENGTH: {1}", pnpId, pnpId.Length);
                serialPort.Close();
                if (string.CompareOrdinal(pnpId, HAZE_METER_PNP_ID) == 0 ||
                    string.CompareOrdinal(pnpId, COLOR_HAZE_METER_PNP_ID) == 0) // 色彩雾度仪
                {
                    return InstrumentType.YH;
                }
                else if (string.CompareOrdinal(pnpId, MAS_COLOR_METER_PNP_ID) == 0)
                {
                    return InstrumentType.MAS;
                }
                return InstrumentType.Unknown;
            }
            catch (Exception ex)
            {
                return InstrumentType.Unknown;
            }
        }

        public string GetPnpId()
        {
            var result = SendPacket(new CmdPacket(CommandType.CALL, 0x0A), 3000);
            if (result != null)
            {
                return Encoding.ASCII.GetString(result).TrimEnd('\0');
            }
            return null;
        }

    }
}
