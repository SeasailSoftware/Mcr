using System;

namespace ThreeNH.Color.Model
{
    public class CIELCh
    {
        public CIELCh() { }
        public CIELCh(double _L, double _C, double _h)
        {
            L = _L;
            C = _C;
            h = _h;
        }
        public double L { get; set; }

        public double C { get; set; }

        public double h { get; set; }

        public double a
        {
            get
            {
                return C * Math.Cos(MathHelper.DegreesToRadians(h));
            }
        }
        public double b
        {
            get
            {
                return C * Math.Sin(MathHelper.DegreesToRadians(h));
            }
        }

        public CIELab ToLab()
        {
            return new CIELab(L, a, b);
        }
    }
}
