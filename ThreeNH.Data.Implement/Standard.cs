using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeNH.Color.Enums;

namespace ThreeNH.Data.Implement
{
    public class Standard : Sample, IStandard
    {
        public Standard()
        {

        }
        public Standard(ISample sample) : base(sample)
        {

        }
        public ITolerance Tolerance { get; set; }
    }
}
