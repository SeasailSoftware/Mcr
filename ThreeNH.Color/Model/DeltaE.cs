using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Color.Model
{
    public class CIEDE1976
    {
        public CIEDE1976(CIELab trial, CIELab standard)
        {
            dL = trial.L - standard.L;
            da = trial.a - standard.a;
            db = trial.b - standard.b;
            dC = trial.C - standard.C;
            dh = trial.h - standard.h;
            if (dh > 180.0)
                dh -= 360.0;
            else if (dh < -180.0)
                dh += 360.0;
            dH = 2.0 * Math.Sqrt(trial.C * standard.C) * Math.Sin(MathHelper.DegreesToRadians((dh / 2.0)));
        }
        public CIEDE1976(CIELCh trial, CIELCh standard)
            : this(trial.ToLab(), standard.ToLab())
        {

        }

        public double dL { get; private set; }
        public double da { get; private set; }
        public double db { get; private set; }
        public double dC { get; private set; }
        public double dh { get; private set; }
        public double dH { get; private set; }
        public double dE
        {
            get
            {
                return Math.Sqrt(dL * dL + dC * dC + dH * dH);
            }
        }

    };



    public abstract class BetterLabDE
    {
        protected double _Sl = 1.0f;
        protected double _Sc = 1.0f;
        protected double _Sh = 1.0f;
        protected double _Kl = 1.0f;
        protected double _Kc = 1.0f;
        protected double _Kh = 1.0f;
        protected CIEDE1976 _dE1976 = null;

        protected BetterLabDE(CIELab trial, CIELab standard, double KL = 1.0f, double KC = 1.0f, double KH = 1.0f)
        {
            _dE1976 = new CIEDE1976(trial, standard);
            _Kl = KL;
            _Kc = KC;
            _Kh = KH;
        }
        protected BetterLabDE(CIEDE1976 dE1976, double KL = 1.0f, double KC = 1.0f, double KH = 1.0f)
        {
            _dE1976 = dE1976;
            _Kl = KL;
            _Kc = KC;
            _Kh = KH;
        }

        public virtual double dE
        {
            get
            {
                return Math.Sqrt(dL * dL + dC * dC + dH * dH);
            }
        }

        public double dL
        {
            get
            {
                return _dE1976.dL / (Kl * _Sl);
            }
        }
        public double dC
        {
            get
            {
                return _dE1976.dC / (Kc * _Sc);
            }
        }
        public double dH
        {
            get
            {
                return _dE1976.dH / (Kh * _Sh);
            }
        }
        public double Kl
        {
            get { return _Kl; }
            set { _Kl = value; }
        }
        public double Kc
        {
            get { return _Kc; }
            set { _Kc = value; }
        }
        public virtual double Kh
        {
            get { return _Kh; }
            set { _Kh = value; }
        }
    }

    public class CIEDE1994 : BetterLabDE
    {
        public CIEDE1994(CIELab trial, CIELab standard, double KL = 1.0f, double KC = 1.0f, double KH = 1.0f)
                : this(trial, standard, new CIEDE1976(trial, standard), KL, KC, KH) { }

        public CIEDE1994(CIELab trial, CIELab standard, CIEDE1976 dE1976, double KL = 1.0f, double KC = 1.0f, double KH = 1.0f)
                : base(dE1976, KL, KC, KH)
        {
            _Sl = 1.0f;
            _Sc = 1.0f + 0.045f * standard.C;
            _Sh = 1.0f + 0.015f * standard.C;
        }

        public CIEDE1994(CIELCh trial, CIELCh standard)
            : this(trial.ToLab(), standard.ToLab())
        {

        }

        public double da => _dE1976.da;

        public double db => _dE1976.db;
    }

    public class CMCDE : BetterLabDE
    {
        public CMCDE(CIELab trial, CIELab standard, double KL = 1.0f, double KC = 1.0f, double KH = 1.0f)
                : this(trial, standard, new CIEDE1976(trial, standard), KL, KC, KH) { }
        public CMCDE(CIELCh trial, CIELCh standard, double KL = 1.0f, double KC = 1.0f, double KH = 1.0f)
            : this(trial.ToLab(), standard.ToLab(), KL, KC, KH)
        {

        }
        public CMCDE(CIELab trial, CIELab standard, CIEDE1976 dE1976, double KL = 1.0f, double KC = 1.0f, double KH = 1.0f)
            : base(dE1976, KL, KC, KH)
        {
            _Sl = (standard.L < 16.0) ?
                0.511 : (0.040975 * standard.L / (1.0 + 0.01765 * standard.L));
            _Sc = 0.0638 * standard.C / (1.0 + 0.0131 * standard.C) + 0.638;
            double f = Math.Sqrt(Math.Pow(standard.C, 4) / (Math.Pow(standard.C, 4) + 1900.0));
            double T = (164.0 < standard.h && standard.h < 345.0) ?
                        (0.56 + Math.Abs(0.2 * Math.Cos(MathHelper.DegreesToRadians((standard.h + 168.0f))))) :
                        (0.36 + Math.Abs(0.4 * Math.Cos(MathHelper.DegreesToRadians((standard.h + 35.0f)))));
            _Sh = _Sc * (float)(T * f + 1.0f - f);
        }

        // CMC 不能设置Kh的值
        public override double Kh
        {
            get { return base.Kh; }
            set { _Kh = 1.0f; }
        }

        public double da => _dE1976.da;
        public double db => _dE1976.db;
    };

    public class CIEDE2000 : BetterLabDE
    {
        double _Rt = 1.0;

        public CIEDE2000(CIELab trial, CIELab standard, double KL = 1.0f, double KC = 1.0f, double KH = 1.0f)
            : this(trial, standard, new CIEDE1976(trial, standard), KL, KC, KH) { }
        public CIEDE2000(CIELCh trial, CIELCh standard, double KL = 1.0f, double KC = 1.0f, double KH = 1.0f)
            : this(trial.ToLab(), standard.ToLab(), KL, KC, KH)
        {

        }

        public CIEDE2000(CIELab trial, CIELab standard, CIEDE1976 dE1976, double KL = 1.0f, double KC = 1.0f, double KH = 1.0f)
            : base(dE1976, KL, KC, KH)
        {
            double CC = (trial.C + standard.C) / 2.0;
            double G = 0.5 * (1.0 - Math.Sqrt(Math.Pow(CC, 7) /
                                              (Math.Pow(CC, 7) + Math.Pow(25, 7))));

            CIELab lab1 = new CIELab(trial.L, trial.a * (1.0 + G), trial.b);
            CIELab lab0 = new CIELab(standard.L, standard.a * (1.0 + G), standard.b);
            _dE1976 = new CIEDE1976(lab1, lab0);  // 用 L',a',b' 代替原来的Lab

            double mL = (lab1.L + lab0.L) / 2.0;
            double mC = (lab1.C + lab0.C) / 2.0;
            double mh = Math.Abs(lab1.h - lab0.h) > 180 ?
                        (lab1.h + lab0.h + 360) / 2.0 : (lab1.h + lab0.h) / 2.0;

            _Sl = 1.0 + 0.015 * Math.Pow(mL - 50.0, 2) / Math.Sqrt(20.0 + Math.Pow(mL - 50.0, 2));
            _Sc = 1.0 + 0.045 * mC;
            double T = 1.0 - 0.17 * Math.Cos(MathHelper.DegreesToRadians((mh - 30.0))) + 0.24 * Math.Cos(MathHelper.DegreesToRadians((2.0 * mh)))
                    + 0.32 * Math.Cos(MathHelper.DegreesToRadians((3.0 * mh + 6.0))) - 0.20 * Math.Cos(MathHelper.DegreesToRadians((4.0 * mh - 63.0)));
            _Sh = 1.0f + 0.015 * mC * T;

            double Rc = 2.0 * (float)Math.Sqrt(Math.Pow(mC, 7) / (float)(Math.Pow(mC, 7) + Math.Pow(25.0, 7)));
            double dTheta = 30.0 * Math.Exp(-Math.Pow((mh - 275.0) / 25.0, 2));
            _Rt = -Math.Sin(MathHelper.DegreesToRadians((2.0f * dTheta))) * Rc;
        }

        public override double dE
        {
            get
            {
                return Math.Sqrt(dL * dL + dC * dC + dH * dH +
                    _Rt * dC * dH);
            }
        }

        public double da => _dE1976.da;
        public double db => _dE1976.db;
    }

    public class DEDIN99
    {
        public DEDIN99(CIELab trial, CIELab target)
        {
            var delta = new CIEDE1976(trial, target);
            dL = delta.dL;
            dC = delta.dC;
            dH = delta.dH;
            dE = delta.dE;
        }

        public double dL { get; set; }

        public double dC { get; set; }

        public double dH { get; set; }

        public double dE { get; set; }
    }
}
