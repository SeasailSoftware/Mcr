using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Color.Model
{
    [DataContract]
    public class CIEXYZ
    {
        public CIEXYZ() { }

        public CIEXYZ(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        [DataMember]
        public double X { get; set; }

        [DataMember]
        public double Y { get; set; }

        [DataMember]
        public double Z { get; set; }

        public double x
        {
            get
            {
                double tmp = X + Y + Z;
                return tmp == 0.0 ? 0.0 : X / tmp;
            }
        }

        public double y
        {
            get
            {
                double tmp = X + Y + Z;
                return tmp == 0.0 ? 0.0 : Y / tmp;
            }
        }

        public double z
        {
            get
            {
                double tmp = X + Y + Z;
                return tmp == 0.0 ? 0.0 : Z / tmp;
            }
        }
    }
}
