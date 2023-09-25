using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReagentStripTest.ViewModels.Calibrate
{

    public class CalibrationMessage
    {
        public CalibrationMessage(CalibrationType calibrationType)
        {
            CalibrationType = calibrationType;
        }
        public CalibrationType CalibrationType { get; set; }
    }

    public enum CalibrationType
    {
        BlackCalibration,
        WhiteCalibration,
        CancelCalibration,
        CalibrationSuccess
    }
}
