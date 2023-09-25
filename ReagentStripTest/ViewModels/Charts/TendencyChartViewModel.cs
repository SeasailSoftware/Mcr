using Caliburn.Micro;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using ThreeNH.i18N;

namespace ReagentStripTest.ViewModels.Charts
{
    public class TendencyChartViewModel : Screen
    {

        private ITranslater Translater => IoC.Get<ITranslater>();

        public TendencyChartViewModel(string title, Func<ChartPoint, string> labelFunc = null, Brush brush = null)
        {
            //AxisXTitle = $"X->{Translater.Trans("s_Index")}";
            AxisYTitle = title;
            AxisXMaxRange = 10;
            AxisXMaxValue = 10;
            LineSeries = new LineSeries()
            {
                PointGeometry = DefaultGeometries.Diamond,
                DataLabels = true,
                Fill = brush == null ? new SolidColorBrush() { Opacity = 1 } : brush,
                LabelPoint = labelFunc == null ? LabelPointFunc : labelFunc,
                Title = title,
                FontSize = 10,
                Values = new ChartValues<ScatterPoint>()
            };
            SeriesCollection.Add(LineSeries);

        }

        private void SetMaxIndex(double max)
        {
            AxisXMaxRange = max;
            AxisXMaxValue = max;
        }

        private string LabelPointFunc(ChartPoint arg)
        {
            var temp = arg.Instance;
            return $"{arg.Y.ToString("F2")}";
        }


        public double AxisXMinRange { get; set; } = 0;

        private double _AxisXMaxRange;
        public double AxisXMaxRange
        {
            get => _AxisXMaxRange;
            set
            {
                _AxisXMaxRange = value;
                NotifyOfPropertyChange(() => AxisXMaxRange);
            }
        }

        public double HazeAxisXMinValue { get; set; } = 0;

        private double _AxisXMaxValue;
        public double AxisXMaxValue
        {
            get => _AxisXMaxValue;
            set
            {
                _AxisXMaxValue = value;
                NotifyOfPropertyChange(() => AxisXMaxValue);
            }
        }

        private double _axisXStep = 1;
        public double AxisXStep
        {
            get => _axisXStep;
            set
            {
                _axisXStep = value;
                NotifyOfPropertyChange(() => AxisXStep);
            }
        }

        public double AxisYMinRange { get; set; } = 0;

        public double AxisYMaxRange { get; set; } = 100;

        public double AxisYMinValue { get; set; } = 0;

        public double AxisYMaxValue { get; set; } = 100;

        public double AxisYStep { get; set; } = 25;



        /// <summary>
        /// XY轴坐标图
        /// </summary>
        public SeriesCollection SeriesCollection { get; set; } = new SeriesCollection();

        public LineSeries LineSeries { get; set; }


        public string AxisXTitle { get; set; }

        public string AxisYTitle { get; set; }

        public void AddPoint(int index, double value)
        {
            LineSeries.Values.Add(new ScatterPoint() { Y = value, X = index, Weight = 10 });
            AxisXMaxValue = LineSeries.Values.Count + 1;
            AxisXStep = AxisXMaxValue < 5 ? 1 : (int)(AxisXMaxValue / 5);
        }

        public void Clear()
        {
            LineSeries.Values.Clear();
        }

    }
}
