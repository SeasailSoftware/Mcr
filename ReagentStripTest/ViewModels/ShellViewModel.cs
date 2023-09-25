using Caliburn.Micro;
using Microsoft.Win32;
using ReagentStripTest.Core;
using ReagentStripTest.Utils;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ThreeNH.Communication;
using ThreeNH.Communication.CommDevice.Serial;
using ThreeNH.Communication.Detector;
using ThreeNH.Instrument;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReagentStripTest.Models;
using ThreeNH.Color.Algorithm;
using System.Windows.Media;
using ReagentStripTest.Views;
using ThreeNH.Color.Model;
using System.ComponentModel.Design;
using ThreeNH.Database;
using Newtonsoft.Json;

namespace ReagentStripTest.ViewModels
{
    [Export(typeof(IShell))]
    internal class ShellViewModel : ViewModelBase, IShell, IHandle<PortsChangedArgs>, IHandle<InstrumentModel>
    {
        private BackgroundWorker _connectionWorker;
        public ColorDatabase Database => IoC.Get<ColorDatabase>();
        public ShellViewModel()
        {
            Translater.Trans(nameof(ShellViewModel));
            EventAggregator.SubscribeOnUIThread(this);
            InitializeBackgroundWorker();
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            RefreshInstruments();
            return base.OnActivateAsync(cancellationToken);
        }

        private void RefreshInstruments()
        {
            InstrumentModels.Clear();
            var entities = Database.Instruments.ToList();
            foreach (var entity in entities)
            {
                var model = new InstrumentModel(entity.Id, entity.Name, entity.SN);
                if (!string.IsNullOrEmpty(entity.WhiteboardData))
                    model.WhiteboardData = JsonConvert.DeserializeObject<Spectrum>(entity.WhiteboardData);
                InstrumentModels.Add(model);
            }
        }

        public Options Options => IoC.Get<Options>();


        public ObservableCollection<InstrumentModel> InstrumentModels { get; set; } = new ObservableCollection<InstrumentModel>();

        private InstrumentModel _instrumentModel;
        public InstrumentModel InstrumentModel
        {
            get => _instrumentModel;
            set
            {
                _instrumentModel = value;
                NotifyOfPropertyChange(() => InstrumentModel);
            }
        }


        private SampleModel _sampleModel;
        public SampleModel SampleModel
        {
            get => _sampleModel;
            set
            {
                _sampleModel = value;
                NotifyOfPropertyChange(() => SampleModel);
            }
        }



        public ConnectionWay ConnectionWay
        {
            get => Options.ConnectionWay;
            set
            {
                Options.ConnectionWay = value;
                NotifyOfPropertyChange(() => ConnectionWay);
            }
        }

        public IDictionary<string, object> InstrumentInformations => Instrument?.InstrumentInformation;
        public bool ConnectByUsb
        {
            get => ConnectionWay == ConnectionWay.ByUsb;
            set
            {
                ConnectionWay = ConnectionWay.ByUsb;
                NotifyOfPropertyChange(() => ConnectByUsb);
                NotifyOfPropertyChange(() => ConnectByBluetooth);
            }
        }

        public bool ConnectByBluetooth
        {
            get => ConnectionWay == ConnectionWay.ByBluetooth;
            set
            {
                ConnectionWay = ConnectionWay.ByBluetooth;
                NotifyOfPropertyChange(() => ConnectByBluetooth);
                NotifyOfPropertyChange(() => ConnectByUsb);
            }
        }


        public bool IsRunning => InstrumentModels.FirstOrDefault(o => o.IsRunning) != null;

        public override Task<bool> CanCloseAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(!IsRunning);
        }
        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            foreach (var model in InstrumentModels)
            {
                if (model.IsConnected)
                    model.Instrument.Close();
            }
            return base.OnDeactivateAsync(close, cancellationToken);
        }

        public new IInstrument Instrument => InstrumentModel?.Instrument;

        private string _statusText;
        public string StatusText
        {
            get => _statusText;
            set
            {
                _statusText = value;
                NotifyOfPropertyChange(() => StatusText);
            }
        }




        private string _portName;
        public string PortName
        {
            get => _portName;
            set
            {
                _portName = value;
                NotifyOfPropertyChange(() => PortName);
            }
        }














        private string _connectionStatus;
        public string ConnectionStatus
        {
            get => _connectionStatus;
            set
            {
                _connectionStatus = value;
                NotifyOfPropertyChange(() => ConnectionStatus);
            }
        }
        private void InitializeBackgroundWorker()
        {
            _connectionWorker = new BackgroundWorker();
            _connectionWorker.WorkerSupportsCancellation = true;
            _connectionWorker.WorkerReportsProgress = true;
            _connectionWorker.DoWork += DoConnectionWork;
            _connectionWorker.ProgressChanged += ConnectionProgressChanged;
            _connectionWorker.RunWorkerCompleted += ConnectionWorkCompleted;
            _connectionWorker.RunWorkerAsync();
        }
        private void DoConnectionWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            if (worker is null) return;

            var address = e.Argument;
            var progress = 1;
            SerialPortInformation[] serialPorts;
            if (address is string portName && !string.IsNullOrEmpty(portName))
            {
                serialPorts = new[] { new SerialPortInformation(portName) };
            }
            else
            {
                worker.ReportProgress(progress, "Looking up device...");
                serialPorts = SerialPortService.SerialPorts;
            }
            foreach (var portInfo in serialPorts)
            {
                worker.ReportProgress(progress++, "Checking device type...");
                PortName = portInfo.PortName;
                if (InstrumentModels.Select(o => o.DeviceName).Contains(PortName))
                    continue;
                //if (!portInfo.IsSTM32VirtualPort()) continue;
                var detector = DetectorFactory.CreateInstance(portInfo);
                var instrumentType = detector?.Detect();
                if (instrumentType != null && instrumentType == InstrumentType.MCR)
                {
                    IInstrument instrument = new ThreeNH.Instrument.MicroColorReader();
                    instrument.Open(portInfo.PortName);
                    if (instrument.IsOpen)
                    {
                        e.Result = instrument;
                    }
                }
            }
        }

        private void ConnectionProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is string text)
                StatusText = text;

        }

        private void ConnectionWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                StatusText = Translater.Trans(e.Error.Message);
            else if (e.Result is IInstrument instrument)
            {
                if (instrument.IsOpen && instrument.InstrumentInformation.ContainsKey(InstrumentInformationKey.SerialNumber))
                {
                    foreach (var model in InstrumentModels)
                    {
                        if (instrument.InstrumentInformation[InstrumentInformationKey.SerialNumber] as string == model.SN)
                        {
                            model.Instrument = instrument;
                            break;
                        }
                    }
                }
                else
                {
                    instrument.Close();
                }
            }
            StatusText = Translater.Trans("Ready");
        }





        public ICommand AddInstrumentCommand => new RelayCommand(async x =>
        {
            var model = new ViewModels.Instruments.AddInstrumentViewModel();
            var result = await DialogHelper.ShowDialogAsync(model);
            if (result == true)
            {
                try
                {
                    var entity = new InstrumentEntity() { Name = model.Name, SN = model.SN };
                    await Database.AddInstrument(entity);
                    entity = Database.FindInstrumentBySN(entity.SN);
                    if (entity != null)
                    {
                        InstrumentModels.Add(new InstrumentModel(entity.Id, entity.Name, entity.SN));
                        TryConnectInstrument();
                    }

                }
                catch (Exception ex)
                {
                    await DialogHelper.ShowErrorAsync("添加设备失败", ex.Message);
                }
            }

        }, y => !IsRunning);

        public ICommand DeleteInstrumentCommand => new RelayCommand(async x =>
        {
            var result = await DialogHelper.AskUserYesNoAsync("删除确认", $"是否要删除当前设备[{InstrumentModel.SN}]?");
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await Database.DeleteInstrument(InstrumentModel.Id);
                    InstrumentModels.Remove(InstrumentModel);
                }
                catch (Exception ex)
                {
                    await DialogHelper.ShowErrorAsync("删除失败", ex.Message);
                }
            }
        }, y => !IsRunning && InstrumentModel != null);

        public ICommand AirSamplingCommand => new RelayCommand(async x =>
        {
            try
            {
                var sample = InstrumentModel.Instrument?.Measure(true);
                InstrumentModel.WhiteboardData = sample.Spectrum;
                Database.UpdateWhiteboardData(InstrumentModel.Id, JsonConvert.SerializeObject(sample.Spectrum));
            }
            catch (Exception ex)
            {
                await DialogHelper.ShowErrorAsync("对空采样失败", ex.Message);
            }
        }, y => InstrumentModel != null && InstrumentModel.IsConnected && !IsRunning);

        public RelayCommand CloseCommand => new RelayCommand(async x =>
        {
            await TryCloseAsync();
        });


        public Task HandleAsync(PortsChangedArgs message, CancellationToken cancellationToken)
        {
            var port = message.SerialPorts[0];
            switch (message.EventType)
            {
                case EventType.Insertion:
                    TryConnectInstrument(port.PortName);

                    break;
                case EventType.Removal:
                    foreach (var model in InstrumentModels)
                    {
                        model.RefreshInstrumentStatus();
                    }
                    break;
            }
            return Task.CompletedTask;
        }

        public void TryConnectInstrument(string portName = "")
        {
            if (Instrument == null || !Instrument.IsOpen)
            {
                if (_connectionWorker != null && !_connectionWorker.IsBusy)
                {
                    _connectionWorker.RunWorkerAsync(portName);
                }
            }
        }

        public void TryDisconnectInstrument(string portName = "")
        {
            if (Instrument == null) return;
            Instrument.Close();
            Instrument.Dispose();
        }

        #region Commands

        public RelayCommand OpenCommand => new RelayCommand(x =>
       {
           OpenFileDialog fileDlg = new OpenFileDialog();
           fileDlg.Title = "打开测试数据...";
           fileDlg.CheckFileExists = true;
           fileDlg.Filter =
               "JSON File（*.json）|*.json"; // "计量工具原始测量数据（*.json;*.xml）|*.json;*.xml|新计量工具原始数据(*.json)|*.json|旧计量工具原始数据(*.xml)|*.xml";
           if (fileDlg.ShowDialog().GetValueOrDefault())
           {

           }
       });

        public RelayCommand SaveCommand => new RelayCommand(async x =>
        {
            SaveFileDialog fileDlg = new SaveFileDialog();
            fileDlg.Title = "存储测试数据...";
            fileDlg.Filter = "JSON File（*.json）|*.json";
            if (fileDlg.ShowDialog().GetValueOrDefault())
            {

            }
        });








        private TimeSpan _executedTime;
        public TimeSpan ExecutedTime
        {
            get => _executedTime;
            set
            {
                _executedTime = value;
                NotifyOfPropertyChange(() => ExecutedTime);
            }
        }


        public RelayCommand AboutCommand => new RelayCommand(async x =>
        {
            var model = new Helps.AboutViewModel();
            await Utils.DialogHelper.ShowDialogAsync(model);
        });

        public RelayCommand ThemeSettingCommand => new RelayCommand(async x =>
        {
            var model = new ThemeSettingViewModel();
            await Utils.DialogHelper.ShowDialogAsync(model);
        });

        public RelayCommand LanguageSettingCommand => new RelayCommand(async x =>
        {
            var model = new LanguageSettingViewModel();
            await Utils.DialogHelper.ShowDialogAsync(model);
        });

        public RelayCommand SettingsCommand => new RelayCommand(async x =>
        {
            var model = new Settings.SettingsViewModel();
            await DialogHelper.ShowDialogAsync(model);
        }, y => !IsRunning);



        public RelayCommand StartCommand => new RelayCommand(async x =>
        {
            if (Options.DataChannel == 1 || Options.DataChannel == 2)
            {
                var model = InstrumentModels.FirstOrDefault(o => o.IsConnected && o.WhiteboardData == null);
                if (model != null)
                {
                    await DialogHelper.ShowErrorAsync("Error", $"仪器[{model.SN}]尚未获取白板数据,请先校正。");
                    return;
                }
            }
            foreach (var model in InstrumentModels)
            {
                await model.Start();
            }

        }, y => !IsRunning);





        public RelayCommand StopCommand => new RelayCommand(x =>
        {
            foreach (var model in InstrumentModels)
            {
                model.Stop();
            }
        }, y => IsRunning);

        public RelayCommand RenameCommand => new RelayCommand(async x =>
        {
            var model = new RenameViewModel(InstrumentModel.RecordModel.Name);
            var result = await Utils.DialogHelper.ShowDialogAsync(model);
            if (result == true)
                InstrumentModel.RecordModel.Name = model.RecordName;
        }, y => InstrumentModel != null && InstrumentModel.RecordModel != null && !IsRunning);

        public RelayCommand DeleteCommand => new RelayCommand(async x =>
        {
            var result = await DialogHelper.AskUserYesNoAsync(Translater.Trans("s_Delete"), "Are you sure to delete the selected record?");
            if (result == MessageBoxResult.Yes)
            {
                InstrumentModel.RecordModels.Remove(InstrumentModel.RecordModel);
                InstrumentModel.ChartViewModel.Clear();
            }
        }, y => !IsRunning && InstrumentModel != null && InstrumentModel.RecordModel != null);

        public RelayCommand ClearCommand => new RelayCommand(async x =>
        {
            var result = await DialogHelper.AskUserYesNoAsync(Translater.Trans("s_Delete"), "Are you sure to clear curent records?");
            if (result == MessageBoxResult.Yes)
            {
                InstrumentModel.RecordModels.Clear();
                InstrumentModel.ChartViewModel.Clear();
                InstrumentModel.DateTimeChart.Clear();
            }
        }, y => InstrumentModel != null && InstrumentModel.RecordModels.Count > 0 && !IsRunning);

        public ICommand ClearAllCommand => new RelayCommand(async x =>
        {
            var result = await DialogHelper.AskUserYesNoAsync(Translater.Trans("s_Delete"), "Are you sure to clear all records?");
            if (result == MessageBoxResult.Yes)
            {
                foreach (var model in InstrumentModels)
                {
                    model.RecordModels.Clear();
                    model.ChartViewModel.Clear();
                    model.DateTimeChart.Clear();
                }
            }
        }, y => !IsRunning);


        public RelayCommand ExportToExcelCommand => new RelayCommand(async x =>
        {
            var dlg = new SaveFileDialog() { Filter = "Excel 2007|*.xlsx|Excel 2003|*.xls" };
            var result = dlg.ShowDialog();
            if (result == true)
            {
                try
                {
                    ExcelHelper.SaveRecords(InstrumentModel.RecordModels, dlg.FileName);
                    Process.Start(dlg.FileName);
                }
                catch (Exception ex)
                {
                    await DialogHelper.ShowErrorAsync("Error", ex.Message);
                }
            }

        }, y => InstrumentModel != null && InstrumentModel.RecordModels.Count > 0 && !IsRunning);

        public RelayCommand CalibrateCommand => new RelayCommand(async x =>
        {
            try
            {
                InstrumentModel.Instrument.CalibrateWhite();
                var spectrum = Instrument.Measure(true).Spectrum;
                InstrumentModel.WhiteboardData = spectrum;
                Database.UpdateWhiteboardData(InstrumentModel.Id, JsonConvert.SerializeObject(spectrum));
                await DialogHelper.ShowInfoAsync("Info", Translater.Trans("s_CalibratedSuccessfully"));
            }
            catch (Exception ex)
            {
                await DialogHelper.ShowErrorAsync("Error", ex.Message);
            }
        }, y => InstrumentModel != null && InstrumentModel.IsConnected && !IsRunning);


        #endregion




        public void MouseDown(object obj)
        {
            try
            {
                Application.Current.MainWindow.DragMove();
            }
            catch
            {

            }
        }

        private void OnMessage(string msg)
        {
            OnUIThread(() =>
            {
                StatusText = msg;
            });
        }

        public Task HandleAsync(InstrumentModel message, CancellationToken cancellationToken)
        {
            NotifyOfPropertyChange(() => IsRunning);
            return Task.CompletedTask;
        }
    }
}
