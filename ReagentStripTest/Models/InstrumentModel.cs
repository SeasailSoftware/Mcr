using Caliburn.Micro;
using ReagentStripTest.Core;
using ReagentStripTest.ViewModels.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using ThreeNH.Color.Algorithm;
using ThreeNH.Color.Model;
using ThreeNH.Instrument;

namespace ReagentStripTest.Models
{
    internal class InstrumentModel : Caliburn.Micro.PropertyChangedBase
    {
        public Options Options => IoC.Get<Options>();
        private int _recordIndex;
        public InstrumentModel(int id, string name, string sn)
        {
            _id = id;
            _name = name;
            _sn = sn;
        }
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyOfPropertyChange(() => Id);
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        private string _sn;
        public string SN
        {
            get => _sn;
            set
            {
                _sn = value;
                NotifyOfPropertyChange(() => SN);
            }
        }

        private IInstrument _instrument;
        public IInstrument Instrument
        {
            get => _instrument;
            set
            {
                _instrument = value;
                NotifyOfPropertyChange(() => Instrument);
                NotifyOfPropertyChange(() => IsConnected);
                NotifyOfPropertyChange(() => DeviceName);
            }
        }

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

        private RecordModel _recordModel;
        public RecordModel RecordModel
        {
            get => _recordModel;
            set
            {
                _recordModel = value;
                NotifyOfPropertyChange(() => RecordModel);
                ChartViewModel.Clear();
                var index = 1;
                foreach (var sample in value.Samples)
                {
                    ChartViewModel.AddPoint(index++, sample.Data);
                }
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

        public Spectrum WhiteboardData { get; set; }

        public bool IsConnected => Instrument != null && Instrument.IsOpen;

        private double _temperature;
        public double Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                NotifyOfPropertyChange(() => Temperature);
            }
        }

        public string DeviceName
        {
            get
            {
                var infomation = Instrument?.InstrumentInformation;
                if (infomation == null || infomation.ContainsKey(InstrumentInformationKey.CommunicationDeviceName)) return null;
                return infomation[InstrumentInformationKey.CommunicationDeviceName] as string;
            }
        }

        public ObservableCollection<RecordModel> RecordModels { get; set; } = new ObservableCollection<RecordModel>();

        public ViewModels.Charts.TendencyChartViewModel ChartViewModel { get; set; } = new ViewModels.Charts.TendencyChartViewModel("K\\S");

        public DataTimeScaleChartViewModel DateTimeChart { get; set; } = new DataTimeScaleChartViewModel();

        internal void RefreshInstrumentStatus()
        {
            NotifyOfPropertyChange(() => Instrument);
            NotifyOfPropertyChange(() => IsConnected);
            NotifyOfPropertyChange(() => DeviceName);
        }

        public async Task Start()
        {
            if (!IsConnected)
                return;
            IsRunning = true;
            ExecutedTime = TimeSpan.FromSeconds(0);
            await Task.Run(() =>
            {
                var total = Options.MeasurementDuration;
                var interval = Options.MeasurementInterval / 1000;
                Spectrum first = null;
                var sampleIndex = 1;

                while (IsRunning)
                {
                    var result = Instrument.Measure(true);
                    result = Recalculate(result);
                    if (first == null)
                    {
                        first = result.Spectrum;
                        Thread.Sleep(1000);
                        continue;
                    }

                    if (CheckColorChange(first, result.Spectrum))
                    {
                        OnUIThread(() =>
                        {
                            var recordModel = new RecordModel()
                            {
                                Name = $"Record{_recordIndex++.ToString("000")}",
                                DateTime = DateTime.Now
                            };
                            if (first != null)
                            {
                                var data = first[Options.SpecifiedWaveLength] * 100;
                                var lab = first.ToCIEXYZ(Options.Illuminant, Options.Observer).ToCIELab(Options.Illuminant, Options.Observer);
                                var sRgb = lab.TosRGB(Options.Illuminant, Options.Observer);
                                var firstSample = new SampleModel()
                                {
                                    Index = sampleIndex,
                                    Data = data,
                                    DateTime = TimeSpan.FromSeconds(0),
                                    Illuminant = Options.Illuminant,
                                    Observer = Options.Observer,
                                    Lower = Options.Lower,
                                    Upper = Options.Upper,
                                    SpecifiedWaveLength = Options.SpecifiedWaveLength,
                                    L = lab.L,
                                    a = lab.a,
                                    b = lab.b,
                                    PseudoColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(sRgb.R, sRgb.G, sRgb.B))
                                };
                                recordModel.Samples.Add(firstSample);
                            }
                            RecordModels.Add(recordModel);
                            RecordModel = recordModel;
                            var temp = 1;
                            foreach (var model in RecordModels)
                            {
                                model.Index = temp++;
                            }
                            ChartViewModel.Clear();
                            DateTimeChart.Clear();
                        });

                        var index = 0;
                        while (index <= total && IsRunning)
                        {
                            OnUIThread(() =>
                            {
                                ExecutedTime = TimeSpan.FromSeconds(index);
                            });
                            if (index % interval == 0)
                            {
                                var time = ExecutedTime;
                                Task.Factory.StartNew(() =>
                                {
                                    result = Instrument.Measure(true);
                                    result = Recalculate(result);
                                    //var ks = ThreeNH.Color.Algorithm.MathHelper.K_S(result.Spectrum[Options.SpecifiedWaveLength]);
                                    var data = result.Spectrum[Options.SpecifiedWaveLength] * 100;
                                    var lab = result.Spectrum.ToCIEXYZ(Options.Illuminant, Options.Observer).ToCIELab(Options.Illuminant, Options.Observer);
                                    var sRgb = lab.TosRGB(Options.Illuminant, Options.Observer);
                                    //ExecutedTime = DateTime.Now - start;
                                    if (!double.IsInfinity(data))
                                    {
                                        OnUIThread(() =>
                                        {
                                            Temperature = result.Temperature;
                                            var sample = new SampleModel()
                                            {
                                                Index = sampleIndex,
                                                Data = data,
                                                DateTime = TimeSpan.FromSeconds((long)time.TotalSeconds),
                                                Illuminant = Options.Illuminant,
                                                Observer = Options.Observer,
                                                Lower = Options.Lower,
                                                Upper = Options.Upper,
                                                SpecifiedWaveLength = Options.SpecifiedWaveLength,
                                                Temperature = result.Temperature,
                                                L = lab.L,
                                                a = lab.a,
                                                b = lab.b,
                                                PseudoColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(sRgb.R, sRgb.G, sRgb.B))
                                            };
                                            RecordModel.Samples.Add(sample);
                                            SampleModel = RecordModel.Samples.Last();
                                            ChartViewModel.AddPoint(sampleIndex, data);
                                            DateTimeChart.AddPoint(data);
                                            //(this.GetView() as ShellView).SampleDataGrid.ScrollIntoView(SampleModel);
                                            sampleIndex++;
                                        });
                                    }
                                });
                            }
                            Thread.Sleep(1000);
                            index++;
                        }
                        OnUIThread(() =>
                        {
                            IsRunning = false;
                            IoC.Get<IEventAggregator>().PublishOnUIThreadAsync(this);
                        });
                        break;
                    }

                }

            });
        }
        public void Stop()
        {
            IsRunning = false;
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                _isRunning = value;
                NotifyOfPropertyChange(() => IsRunning);
            }
        }


        private ISample Recalculate(ISample result)
        {
            if (Options.DataChannel == 1 || Options.DataChannel == 2)
            {
                var range = result.Spectrum.Range;
                for (var waveLength = range.Min; waveLength <= range.Max; waveLength += range.Step)
                {
                    var factor =WhiteboardData[waveLength] == 0 ? 1 : WhiteboardData[waveLength];
                    result.Spectrum[waveLength] = result.Spectrum[waveLength] / factor;
                    result.Spectrum[waveLength] = result.Spectrum[waveLength] * Options.ReflectanceFactors[(waveLength - 400) / 10];
                }
            }
            return result;
        }

        private bool CheckColorChange(Spectrum target, Spectrum spectrum)
        {
            var illuminant = Options.Illuminant;
            var observer = Options.Observer;
            var trial = spectrum.ToCIEXYZ(illuminant, observer).ToCIELab(illuminant, observer);
            var waveLength = new WavelengthRange(400, 700, 10);

            var holeTarget = new Spectrum(waveLength, target.Data).ToCIEXYZ(illuminant, observer).ToCIELab(illuminant, observer);
            var deHole = ColorAlgorithm.CalculateDeltaEab(trial, holeTarget);
            return deHole >= 1;
        }
    }
}
