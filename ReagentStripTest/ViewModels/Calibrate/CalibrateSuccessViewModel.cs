using Caliburn.Micro;
using ReagentStripTest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReagentStripTest.ViewModels.Calibrate
{
    class CalibrateSuccessViewModel : ViewModelBase
    {
        public RelayCommand CloseCommand => new RelayCommand(async x =>
        {
            await IoC.Get<IEventAggregator>().PublishOnUIThreadAsync(new CalibrationMessage(CalibrationType.CancelCalibration));
        });
    }
}
