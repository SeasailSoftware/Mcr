using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeNH.Communication.CommDevice;
using ThreeNH.Communication.Detector;

namespace ThreeNH.Communication.Spreadtrum
{
    public class SpreadtrumDetector : InstrumentBase, IDetector
    {
        private ICommDevice _device;
        public SpreadtrumDetector(ICommDevice commDevice)
        {
            _device = commDevice;
        }
        public InstrumentType Detect()
        {
            try
            {
                Open(_device);
                var deviceInfo = GetDeviceInfo();
                return DetectByParser(deviceInfo);
            }
            catch
            {
                return InstrumentType.Unknown;
            }
            finally
            {
                Close();
            }
        }

        private DeviceInfo GetDeviceInfo()
        {
            var result = SendCommand(0xA1);
            if (!result.IsAckOk)
            {
                throw new CommunicationException(CommunicationError.ExecuteCommandFailed,
                    result.Ack.ToString());
            }

            DeviceInfo info = new DeviceInfo();
            var packetContent = result.PacketContent;

            packetContent.Dequeue(out string strValue);
            info.InstrumentMode = strValue;
            packetContent.Dequeue(out strValue);
            info.OpticalStruct = strValue;
            packetContent.Dequeue(out strValue);
            info.HardwareVersion = strValue;
            packetContent.Dequeue(out strValue);
            info.SoftwareVersion = strValue;
            packetContent.Dequeue(out strValue);
            info.WhiteBoardInnerNumber = strValue;
            packetContent.Dequeue(out strValue);
            info.WhiteBoardOuterNumber = strValue;
            packetContent.Dequeue(out ushort shortValue);
            info.Capacity = shortValue;
            packetContent.Dequeue(out shortValue);
            info.StandardCount = shortValue;
            packetContent.Dequeue(out shortValue);
            info.TestCount = shortValue;
            packetContent.Dequeue(out byte byteValue);
            info.BlackCalibration = byteValue != 0;
            packetContent.Dequeue(out byteValue);
            info.WhiteCalibration = byteValue != 0;
            packetContent.Dequeue(out strValue);
            info.SN = strValue;
            packetContent.Dequeue(out strValue);
            info.ModelName = strValue;

            info.Tolerance = null;
            return info;
        }

        private InstrumentType DetectByParser(DeviceInfo info)
        {
            if (info.InstrumentMode != null &&
                info.OpticalStruct != null &&
                info.HardwareVersion != null &&
                info.SoftwareVersion != null &&
                info.WhiteBoardInnerNumber != null &&
                info.WhiteBoardOuterNumber != null &&
                info.SN != null &&
                info.ModelName != null)
            {
                if (string.Compare(info.InstrumentMode, "Colorimeter") == 0)
                {
                    return InstrumentType.YR;
                }
                else if (string.Compare(info.InstrumentMode, "DensitoMeter") == 0)
                {
                    return InstrumentType.YD;
                }
                else
                {
                    return InstrumentType.YS;
                }


            }
            if (info.InstrumentMode != null &&
                info.OpticalStruct != null &&
                info.HardwareVersion != null &&
                info.WhiteBoardInnerNumber != null &&
                info.WhiteBoardOuterNumber != null)
            {
                return InstrumentType.NS;
            }

            if (info.InstrumentMode != null &&
                info.OpticalStruct != null &&
                info.HardwareVersion != null)
            {
                return InstrumentType.NR;
            }


            return InstrumentType.Unknown;
        }
    }
}
