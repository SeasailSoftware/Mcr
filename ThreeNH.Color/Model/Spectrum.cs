using System;
using System.Runtime.Serialization;

namespace ThreeNH.Color.Model
{
    [DataContract]
    public class Spectrum : ICloneable
    {
        [DataMember(Order = 0)]
        private WavelengthRange _range;
        [DataMember(Order = 1)]
        private double[] _data;

		public Spectrum()
        {
            _range = new WavelengthRange { Min = 360, Max = 780, Step = 10 };
            _data = new double[_range.Count];
        }

        public Spectrum(WavelengthRange range)
        {
            _range = range;
            _data = new double[range.Count];
        }

        public Spectrum(WavelengthRange range, double[] data)
        {
            if (range.Count != data.Length)
            {
                throw new ArgumentException("data.Length not equal range.Count");
            }
            _range = range;
            _data = data;
        }

        public Spectrum(Spectrum spectrum)
        {
            _range = spectrum.Range;
            _data = new double[_range.Count];
            Array.Copy(spectrum.Data, _data, _range.Count);
        }



        public WavelengthRange Range { get { return _range; } }

        public double[] Data { get { return _data; } }

        public double this[int wavelength]
        {
            get { return Data[Range.IndexOf(wavelength)]; }
            set { Data[Range.IndexOf(wavelength)] = value; }
        }

        public object Clone()
        {
            return new Spectrum(this);
        }

    }
}
