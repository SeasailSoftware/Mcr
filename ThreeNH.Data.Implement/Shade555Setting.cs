using System;
using System.Collections.Generic;
using System.Text;
using ThreeNH.Color.Enums;

namespace ThreeNH.Data.Implement
{
    public class Shade555Setting : IShade555Setting
    {
        public ShadeToleranceType ShadeToleranceType { get; set; }
        public IDictionary<string, double> Factors { get; set; }
    }
}
