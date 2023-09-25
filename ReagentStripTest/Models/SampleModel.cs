using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ThreeNH.Color.Enums;
using ThreeNH.Color.Model;

namespace ReagentStripTest.Models
{
    public class SampleModel : Caliburn.Micro.PropertyChangedBase
    {


        private int _index;
        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                NotifyOfPropertyChange(() => Index);
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

        private TimeSpan _dateTime;
        public TimeSpan DateTime
        {
            get => _dateTime;
            set
            {
                _dateTime = value;
                NotifyOfPropertyChange(() => DateTime);
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

        private Brush _pseudoColor;
        public Brush PseudoColor
        {
            get => _pseudoColor;
            set
            {
                _pseudoColor = value;
                NotifyOfPropertyChange(() => PseudoColor);
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



        private double _l;
        public double L
        {
            get => _l;
            set
            {
                _l = value;
                NotifyOfPropertyChange(() => L);
            }
        }

        private double _a;
        public double a
        {
            get => _a;
            set
            {
                _a = value;
                NotifyOfPropertyChange(() => a);
            }
        }

        private double _b;
        public double b
        {
            get => _b;
            set
            {
                _b = value;
                NotifyOfPropertyChange(() => b);
            }
        }

        private double _data;
        public double Data
        {
            get => _data;
            set
            {
                _data = value;
                NotifyOfPropertyChange(() => Data);
            }
        }
    }
}
