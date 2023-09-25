﻿using System;
using System.Runtime.Serialization;

namespace ThreeNH.Color.Model
{
    [DataContract]
    public class CIELUV1976
    {
        public CIELUV1976() { }
        public CIELUV1976(double L, double u, double v)
        {
            this.L = L;
            this.u = u;
            this.v = v;
        }

        [DataMember]
        public double L { get; set; }
        [DataMember]
        public double u { get; set; }
        [DataMember]
        public double v { get; set; }
        public double s
        {
            get
            {
                return (L != 0.0) ? (C / L) : 0.0;
            }
        }
        public double C
        {
            get { return Math.Sqrt(u * u + v * v); }
        }
        public double h
        {
            get
            {
                double t = Math.Atan2(v, u) / Math.PI * 180.0;
                if (t < 0)
                    t += 360.0;
                return t;
            }
        }


        public static DeltaLuv operator -(CIELUV1976 trial, CIELUV1976 standard)
        {
            return new DeltaLuv(trial, standard);
        }
    }

    public class DeltaLuv
    {
        public DeltaLuv(CIELUV1976 trial, CIELUV1976 standard)
        {
            dL = trial.L - standard.L;
            du = trial.u - standard.u;
            dv = trial.v - standard.v;
            dC = trial.C - standard.C;
            dh = trial.h - standard.h;
            if (dh > 180.0)
                dh -= 360.0;
            else if (dh < -180.0)
                dh += 360.0;
            dH = 2.0 * Math.Sqrt(trial.C * standard.C)
                    * Math.Sin(MathHelper.DegreesToRadians(dh / 2.0));
            dE = Math.Sqrt(dL * dL + du * du + dv * dv);
        }

        public double dL { get; private set; }
        public double du { get; private set; }
        public double dv { get; private set; }
        public double dC { get; private set; }
        public double dh { get; private set; }
        public double dH { get; private set; }
        public double dE { get; private set; }
    }
}
