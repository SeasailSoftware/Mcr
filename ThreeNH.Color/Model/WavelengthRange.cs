using System;
using System.Runtime.Serialization;

namespace ThreeNH.Color.Model
{
    [DataContract]
    public struct WavelengthRange
    {
        [DataMember(Order = 0)]
        public int Min;
        [DataMember(Order = 1)]
        public int Max;
        [DataMember(Order = 2)]
        public int Step;

        public WavelengthRange(int min = 360, int max = 780, int step = 10)
        {
            Min = min;
            Max = max;
            Step = step;
        }

        public bool IsValid
        {
            get { return Min > 0 && Max > 0 && (Min < Max) && (Max - Min) % Step == 0; }
        }

        public int Count
        {
            get { return IsValid ? (Max - Min) / Step + 1 : 0; }
        }

        public bool Contains(int wavelength)
        {
            return (IsValid && wavelength >= Min &&
                            wavelength <= Max && (wavelength - Min) % Step == 0);
        }

        public bool Contains(WavelengthRange range)
        {
            return (Min <= range.Min) && (Max >= range.Max)
               && (range.Step % Step == 0) && ((range.Min - Min) % Step == 0);
        }

        public int IndexOf(int wavelength)
        {
            if (Contains(wavelength))
            {
                return (wavelength - Min) / Step;
            }
            return -1;
        }

        public int[] Wavelengths
        {
            get
            {
                int[] wavelengths = new int[Count];
                for (int wavelength = Min, i = 0; wavelength <= Max; wavelength += Step, i++)
                {
                    wavelengths[i] = wavelength;
                }
                return wavelengths;
            }
        }

        public int this[int index]
        {
            get { return Min + index * Step; }
        }

    }
}
