using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeNH.Communication.CommDevice.Serial;
using ThreeNH.Communication.EhMc17;

namespace ThreeNH.Communication.Detector
{
    public static class DetectorFactory
    {
        public static IDetector CreateInstance(SerialPortInformation information)
        {
            var commDevice = new SerialPortDevice(information.PortName);
            if (information.IsBluetoothDonglePort())
                return new EhMc17Detector(commDevice);
            else if (information.IsSCIUsb2Serial())
                return new Spreadtrum.SpreadtrumDetector(commDevice);
            else if (information.IsSTM32VirtualPort())
                return new STM32.STM32Detector(commDevice);
            return new STM32.STM32Detector(commDevice);
        }

    }
}
