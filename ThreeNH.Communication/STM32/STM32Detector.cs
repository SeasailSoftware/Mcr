using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThreeNH.Communication.CommDevice;
using ThreeNH.Communication.Detector;
using ThreeNH.Communication.Utility;

namespace ThreeNH.Communication.STM32
{
    public class STM32Detector : ClientProtocol, IDetector
    {
        private List<byte> _recvList = new List<byte>();
        private ManualResetEvent _recvEvent = new ManualResetEvent(false);
        private Exception _ex = null;
        private PacketParser _parser = null;
        private ShakeHandsAckPacket _ackPacket;

        public STM32Detector(ICommDevice commDevice)
        {
            CommDevice = commDevice;
        }

        public InstrumentType Detect()
        {
            try
            {
                Open(CommDevice.DeviceName);
                Connect();
                var instrumentType = ProductTypeToInstrumentType();
                Disconnect();
                Close();
                return instrumentType;
            }
            catch(Exception ex)
            {
                return InstrumentType.Unknown;
            }
        }

        private InstrumentType ProductTypeToInstrumentType()
        {
            switch (ProductType)
            {
                case 0:
                    return InstrumentType.BTS;
                case 2:
                    return InstrumentType.PTS;
                case 3:
                    return InstrumentType.YH;
                case 4:
                    return InstrumentType.MCR;
                case 8:
                    return InstrumentType.MAS;
                default:
                    return InstrumentType.Unknown;
            }
        }
    }
}
