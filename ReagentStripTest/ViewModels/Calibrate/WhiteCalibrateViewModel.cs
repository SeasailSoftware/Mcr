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
    class WhiteCalibrateViewModel : ViewModelBase
    {
        public new IInstrument Instrument { get; private set; }

        public WhiteCalibrateViewModel(IInstrument instrument)
        {
            _SyncContext = System.Threading.SynchronizationContext.Current;
            //WhiteCalibratePrompt = Translater.Trans("s_WhiteCalibratePrompt");
            //var imageUri =$@"{Environment.CurrentDirectory}\Images\white_calibrate.jpg";
            //WhiteCalibrateImage = CreateImageSource(imageUri);
            Instrument = instrument;
        }

        private ImageSource CreateImageSource(string imageUri)
        {
            BitmapImage imgSource = new BitmapImage(new Uri(imageUri, UriKind.Absolute));
            return imgSource;
        }
        private bool? _calibrated;
        public bool? Calibrated
        {
            get => _calibrated;
            set
            {
                _calibrated = value;
                NotifyOfPropertyChange(() => Calibrated);
            }
        }

        private string _whiteCalibratePrompt;
        public string WhiteCalibratePrompt
        {
            get => _whiteCalibratePrompt;
            set
            {
                _whiteCalibratePrompt = value;
                NotifyOfPropertyChange(() => WhiteCalibratePrompt);
            }
        }

        private ImageSource _whiteCalibrateImage;
        public ImageSource WhiteCalibrateImage
        {
            get => _whiteCalibrateImage;
            set
            {
                _whiteCalibrateImage = value;
                NotifyOfPropertyChange(() => WhiteCalibrateImage);
            }
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
                    Instrument.CalibrateWhite();
                    IsCalibrating = true;
                    await IoC.Get<IEventAggregator>().PublishOnUIThreadAsync(new CalibrationMessage(CalibrationType.CalibrationSuccess));
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
