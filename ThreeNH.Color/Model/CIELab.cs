using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Color.Model
{
    [DataContract]
    public class CIELab
    {

        public CIELab() { }

        public CIELab(double _L, double _a, double _b)
        {
            L = _L;
            a = _a;
            b = _b;
        }

        [DataMember]
        public double L { get; set; }

        [DataMember]
        public double a { get; set; }

        [DataMember]
        public double b { get; set; }

        public double C
        {
            get { return Math.Sqrt(a * a + b * b); }
        }
        public double h
        {
            get
            {
                double th = MathHelper.RadiansToDegrees(Math.Atan2(b, a));
                return th < 0 ? th + 360.0 : th;
            }
        }

        public CIELCh ToLCh()
        {
            return new CIELCh(L, C, h);
        }


    }
}
