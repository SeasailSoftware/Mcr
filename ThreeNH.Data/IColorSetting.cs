using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeNH.Color.Enums;

namespace ThreeNH.Data
{
    public interface IColorSetting
    {
        string Channel { get; }

        StandardIlluminant Illuminant { get; }

        StandardObserver Observer { get; }
    }
}
