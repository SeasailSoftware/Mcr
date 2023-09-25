using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ThreeNH.Data.Implement
{
    public class SpectralData : ISpectralData
    {
        public SpectralData()
        {

        }
        private double[] _data;


        #region ISpectraData

        public int Count { get; set; }
        public int WavelengthInterval { get; set; }
        public int WavelengthBegin { get; set; }
        public int WavelengthEnd => WavelengthBegin + (WavelengthInterval) * (Count - 1);

        public bool TransmissionFlag { get; set; }

        public double[] Data
        {
            get => _data;
            set => _data = value;
        }

        public double this[int wavelength]
        {
            get
            {
                var index = IndexOf(wavelength);
                Trace.Assert(index >= 0);
                return _data[index];
            }
            set
            {
                var index = IndexOf(wavelength);
                Trace.Assert(index >= 0);
                _data[index] = value;
            }
        }

        #endregion


        public SpectralData(int begin, int interval, int count, bool transmissionFlag = false)
            : this(begin, interval, count, new double[count], transmissionFlag)
        {

        }

        public SpectralData(int begin, int interval, int count, double[] data, bool transmissionFlag = false)
        {
            Trace.Assert(begin >= 0 && interval > 0 && count > 0
                         && data != null && count == data.Length);
            WavelengthBegin = begin;
            WavelengthInterval = interval;
            Count = count;
            _data = data;
        }

        public SpectralData(ISpectralData spectralData)
            : this(spectralData.WavelengthBegin, spectralData.WavelengthInterval, spectralData.Count)
        {
            for (int i = 0; i < Count; i++)
            {
                int wavelength = WavelengthBegin + WavelengthInterval * i;
                this[wavelength] = spectralData[wavelength];
            }

        }

        public SpectralData(ISpectralData spectralData, bool transmissionFlag)
            : this(spectralData.WavelengthBegin, spectralData.WavelengthInterval, spectralData.Count, transmissionFlag)
        {
            for (int i = 0; i < Count; i++)
            {
                int wavelength = WavelengthBegin + WavelengthInterval * i;
                this[wavelength] = spectralData[wavelength];
            }
        }

        public SpectralData(IEnumerable<ISpectralData> spectralDatas)
        {
            var spectralData = spectralDatas.FirstOrDefault();
            WavelengthBegin = spectralData.WavelengthBegin;
            WavelengthInterval = spectralData.WavelengthInterval;
            Count = spectralData.Count;
            var datas = new List<double>();
            for (int i = 0; i < Count; i++)
            {
                var data = spectralDatas.Average(x => x.Data[i]);
                datas.Add(data);
            }
            Data = datas.ToArray();
        }

        /// <summary>
        /// 是否包含给定波长
        /// </summary>
        /// <param name="wavelength">要判断是否包含的波长</param>
        /// <returns>如果包含返回真</returns>
        public bool Contains(int wavelength)
        {
            return wavelength >= WavelengthBegin
                   && wavelength % WavelengthInterval == 0
                   && (wavelength - WavelengthBegin) / WavelengthInterval < Count;
        }

        /// <summary>
        /// 判断给定波长对应的反射率的索引
        /// </summary>
        /// <param name="wavelength"></param>
        /// <returns>如果在反射率范围内则返回相应索引，否则返回-1</returns>
        public int IndexOf(int wavelength)
        {
            if (wavelength % WavelengthInterval == 0)
            {
                int index = (wavelength - WavelengthBegin) / WavelengthInterval;
                return index >= 0 && index < Count ? index : -1;
            }

            return -1;
        }


        #region ICloneable

        public object Clone()
        {
            return new SpectralData(this);
        }

        #endregion
    }


}
