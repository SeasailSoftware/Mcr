using System;
using System.Collections.Generic;
using System.Text;

namespace ThreeNH.Data
{
    public interface IShade555Setting
    {
        Color.Enums.ShadeToleranceType ShadeToleranceType { get; set; }
        IDictionary<string, double> Factors { get; set; }
    }
}
