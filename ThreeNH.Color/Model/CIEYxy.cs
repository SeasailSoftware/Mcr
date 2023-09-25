using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Color.Model
{
    [DataContract]
    public class CIEYxy
    {
        [DataMember]
        public double Y { get; set; }

        [DataMember]
        public double x { get; set; }

        [DataMember]
        public double y { get; set; }

        public double X
        {
            get
            {
                if (y != 0 && Y != 0)
                    return Y / y * x;
                return 0.0;
            }
        }
        public double Z
        {
            get
            {
                if (y != 0 && Y != 0)
                    return Y / y * z;
                return 0.0;
            }
        }
        public double z
        {
            get
            {
                return 1.0 - x - y;
            }
        }
    }
}
