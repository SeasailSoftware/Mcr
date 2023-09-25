using Caliburn.Micro;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;

namespace ReagentStripTest.ViewModels.Charts
{
    internal class DataTimeScaleChartViewModel : Screen
    {

        public DataTimeScaleChartViewModel()
        {
            Series = new SeriesCollection() { LineSeries };
            XLabelStep = 1;
        }
        public SeriesCollection Series { get; set; }
        public LineSeries LineSeries { get; set; } = new LineSeries() { Values = new ChartValues<double>() };

        private DateTime _initialDateTime;
        public DateTime InitialDateTime
        {
            get => _initialDateTime;
            set
            {
                _initialDateTime = value;
                NotifyOfPropertyChange(() => InitialDateTime);
            }
        }

        private int _xLabelStep;
        public int XLabelStep
        {
            get => _xLabelStep;
            set
            {
                _xLabelStep = value;
                NotifyOfPropertyChange(() => XLabelStep);
            }
        }

        public List<string> Labels
        {
            get
            {
                var labels = new List<string>();

                for (var index = 0; index <= 300; index += 5)
                {
                    labels.Add(TimeSpan.FromSeconds(index).ToString());
                }
                return labels;
            }
        }

        public void Clear()
        {
            LineSeries.Values.Clear();
        }

        public void AddPoint(double point)
        {
            LineSeries.Values.Add(point);
            if (LineSeries.Values.Count <= 10)
                XLabelStep = 1;
            else if (LineSeries.Values.Count <= 20)
                XLabelStep = 2;
            else if (LineSeries.Values.Count <= 30)
                XLabelStep = 3;
            else if (LineSeries.Values.Count <= 40)
                XLabelStep = 4;
            else
                XLabelStep = 5;
        }
    }
}
