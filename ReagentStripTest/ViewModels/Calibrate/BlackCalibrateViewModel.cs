using Caliburn.Micro;
using ReagentStripTest.Core;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ThreeNH.Instrument;

namespace ReagentStripTest.ViewModels.Calibrate
{
    class BlackCalibrateViewModel : ViewModelBase
    {
        public new IInstrument Instrument { get; private set; }

        public BlackCalibrateViewModel(IInstrument instrument)
        {
            _SyncContext = System.Threading.SynchronizationContext.Current;
            //BlackCalibratePrompt = Translater.Trans("s_BlackCalibratePrompt");
            //string imageUri = $@"{Environment.CurrentDirectory}\Images\black_calibrate.jpg";
            //BlackCalibrationImage = CreateImageSource(imageUri);
            Instrument = instrument;
        }

        private System.Threading.SynchronizationContext _SyncContext;
        private bool _isCalibrating;
        public bool IsCalibrating
        {
            get => _isCalibrating;
            set
            {
                _isCalibrating = value;
                NotifyOfPropertyChange(() => IsCalibrating);
            }
        }

        private ImageSource CreateImageSource(string imageUri)
        {
            BitmapImage imgSource = new BitmapImage(new Uri(imageUri, UriKind.Absolute));
            return imgSource;
        }


        private string _blackCalibratePrompt;
        public string BlackCalibratePrompt
        {
            get => _blackCalibratePrompt;
            set
            {
                _blackCalibratePrompt = value;
                NotifyOfPropertyChange(() => BlackCalibratePrompt);
            }
        }

        private ImageSource _blackCalibrationImage;
        public ImageSource BlackCalibrationImage
        {
            get => _blackCalibrationImage;
            set
            {
                _blackCalibrationImage = value;
                NotifyOfPropertyChange(() => BlackCalibrationImage);
            }
        }
        private string _errorMsg;
        public string ErrorMsg
        {
            get => _errorMsg;
            set
            {
                _errorMsg = value;
                NotifyOfPropertyChange(() => ErrorMsg);
            }
        }

        public RelayCommand CancelCommand => new RelayCommand(x =>
        {
            IoC.Get<IEventAggregator>().PublishOnUIThreadAsync(new CalibrationMessage(CalibrationType.CancelCalibration));
        }, y => !IsCalibrating);

        public RelayCommand CalibrateCommand => new RelayCommand(async x =>
        {
            await Task.Run(async () =>
           {
               _SyncContext.Send(o => { IsCalibrating = true; }, null);
               try
               {
                   ErrorMsg = string.Empty;
                   Instrument.CalibrateBlack();
                   IsCalibrating = true;
                   await IoC.Get<IEventAggregator>().PublishOnUIThreadAsync(new CalibrationMessage(CalibrationType.WhiteCalibration));
               }
               catch (Exception ex)
               {
                   ErrorMsg = Translater.Trans(ex.Message);
               }
               finally
               {
                   _SyncContext.Send(o => 
                   {
                       IsCalibrating = false;
                       CommandManager.InvalidateRequerySuggested();
                   }, null);
               }
           });

        }, y => Instrument != null && Instrument.IsOpen && !IsCalibrating);
    }
}
