using Caliburn.Micro;
using ReagentStripTest.Core;
using ReagentStripTest.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;
using ThreeNH.i18N;
using ThreeNH.Instrument;

namespace ReagentStripTest.ViewModels.Calibrate
{
    class CalibrateViewModel : ViewModelBase, IHandle<CalibrationMessage>
    {
        public CalibrateViewModel(IInstrument instrument)
        {
            Instrument = instrument;
        }
        public IInstrument Instrument { get; private set; }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            DisplayName = "校正";
            IoC.Get<IEventAggregator>().SubscribeOnUIThread(this);
            ViewModel = new BlackCalibrateViewModel(Instrument);
            return base.OnInitializeAsync(cancellationToken);
        }

        public Task HandleAsync(CalibrationMessage message, CancellationToken cancellationToken)
        {
            switch (message.CalibrationType)
            {
                case CalibrationType.CancelCalibration:
                    TryCloseAsync();
                    break;
                case CalibrationType.BlackCalibration:
                    ViewModel = new BlackCalibrateViewModel(Instrument);
                    break;
                case CalibrationType.WhiteCalibration:
                    ViewModel = new WhiteCalibrateViewModel(Instrument);
                    break;
                case CalibrationType.CalibrationSuccess:
                    ViewModel = new CalibrateSuccessViewModel();
                    break;
            }
            return Task.CompletedTask;
        }

        private Screen _viewModel;
        public Screen ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                NotifyOfPropertyChange(() => ViewModel);
            }
        }




    }
}
