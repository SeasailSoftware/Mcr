using Caliburn.Micro;
using ReagentStripTest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeNH.Color.Enums;

namespace ReagentStripTest.ViewModels.Settings
{
    internal class SettingsViewModel : Screen
    {
        public Options Options => IoC.Get<Options>();
        public SettingsViewModel()
        {
            _illuminant = Options.Illuminant;
            _observer = Options.Observer;
            _specifiedWaveLength = Options.SpecifiedWaveLength;
            _lower = Options.Lower;
            _upper = Options.Upper;
            _rawDataPath = Options.RawDataPath;
            _reportDataPath = Options.ReportDataPath;
            _measurementDuration = Options.MeasurementDuration;
            _measurementInterval = Options.MeasurementInterval;
            _dataChannel = Options.DataChannel;
            _reflectanceFactors = string.Join(",", Array.ConvertAll(Options.ReflectanceFactors, x => x.ToString("F2")));
        }
        public StandardIlluminant[] Illuminants
        {
            get
            {
                var list = new List<StandardIlluminant>();
                foreach (var item in Enum.GetValues(typeof(StandardIlluminant)))
                {
                    list.Add((StandardIlluminant)item);
                }
                return list.ToArray();
            }
        }

        private StandardIlluminant _illuminant;
        public StandardIlluminant Illuminant
        {
            get => _illuminant;
            set
            {
                _illuminant = value;
                NotifyOfPropertyChange(() => Illuminant);
            }
        }

        public StandardObserver[] Observers => new StandardObserver[] { StandardObserver.CIE1931, StandardObserver.CIE1964 };

        public int[] WaveLengths
        {
            get
            {
                var waveLengths = new List<int>();
                for (var i = 400; i < 700; i += 10)
                {
                    waveLengths.Add(i);
                }
                return waveLengths.ToArray();
            }
        }

        private int _specifiedWaveLength;
        public int SpecifiedWaveLength
        {
            get => _specifiedWaveLength;
            set
            {
                _specifiedWaveLength = value;
                NotifyOfPropertyChange(() => SpecifiedWaveLength);
            }
        }

        private StandardObserver _observer;
        public StandardObserver Observer
        {
            get => _observer;
            set
            {
                _observer = value;
                NotifyOfPropertyChange(() => Observer);
            }
        }

        private double _lower;
        public double Lower
        {
            get => _lower;
            set
            {
                _lower = value;
                NotifyOfPropertyChange(() => Lower);
            }
        }

        private double _upper;
        public double Upper
        {
            get => _upper;
            set
            {
                _upper = value;
                NotifyOfPropertyChange(() => Upper);
            }
        }

        private string _rawDataPath;
        public string RawDataPath
        {
            get => _rawDataPath;
            set
            {
                _rawDataPath = value;
                NotifyOfPropertyChange(() => RawDataPath);
            }
        }

        private string _reportDataPath;
        public string ReportDataPath
        {
            get => _reportDataPath;
            set
            {
                _reportDataPath = value;
                NotifyOfPropertyChange(() => ReportDataPath);
            }
        }

        private int _measurementDuration;
        public int MeasurementDuration
        {
            get => _measurementDuration;
            set
            {
                _measurementDuration = value;
                NotifyOfPropertyChange(() => MeasurementDuration);
            }
        }

        private int _measurementInterval;
        public int MeasurementInterval
        {
            get => _measurementInterval;
            set
            {
                _measurementInterval = value;
                NotifyOfPropertyChange(() => MeasurementInterval);
            }
        }

        private int _dataChannel;
        public int DataChannel
        {
            get => _dataChannel;
            set
            {
                _dataChannel = value;
                NotifyOfPropertyChange(() => DataChannel);
                NotifyOfPropertyChange(() => IsDataChannel3);
            }
        }

        public bool IsDataChannel3 => _dataChannel == 2;

        private string _reflectanceFactors;
        public string ReflectanceFactors
        {
            get => _reflectanceFactors;
            set
            {
                _reflectanceFactors = value;
                NotifyOfPropertyChange(() => ReflectanceFactors);
            }
        }

        public RelayCommand RawDataPathCommand => new RelayCommand(x =>
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
                RawDataPath = dialog.SelectedPath;
        });

        public RelayCommand ReportDataPathCommand => new RelayCommand(x =>
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
                ReportDataPath = dialog.SelectedPath;
        });

        public RelayCommand AcceptCommand => new RelayCommand(async x =>
        {
            try
            {
                Options.Illuminant = _illuminant;
                Options.Observer = _observer;
                Options.SpecifiedWaveLength = _specifiedWaveLength;
                Options.Lower = _lower;
                Options.Upper = _upper;
                Options.RawDataPath = _rawDataPath;
                Options.ReportDataPath = _reportDataPath;
                Options.MeasurementInterval = _measurementInterval;
                Options.MeasurementDuration = _measurementDuration;
                Options.DataChannel = _dataChannel;
                var factors = new List<double>();
                foreach (var data in _reflectanceFactors.Split(','))
                {
                    factors.Add(double.Parse(data));
                }
                Options.ReflectanceFactors = factors.ToArray();
                await TryCloseAsync(true);
            }
            catch (Exception ex)
            {
                await Utils.DialogHelper.ShowErrorAsync("Error", ex.Message + ",31个反射率数据,中间用逗号隔开");
            }

        });
    }
}
