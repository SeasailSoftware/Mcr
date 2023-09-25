using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeNH.Communication.CommDevice;

namespace ThreeNH.Communication.Detector
{
    public interface IDetector
    {
        InstrumentType Detect();
    }
}
