using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Color.Enums
{
    public enum DeltaEMethod
    {
        [Description("ΔE *")]
        CIEDE1976,
        [Description("ΔE*94")]
        CIEDE1994,
        [Description("ΔE*00")]
        CIEDE2000,
        [Description("ΔE*cmc")]
        DECMC,
        [Description("ΔE*uv")]
        DEUV,
        [Description("ΔE(DIN99)")]
        DEDIN99,
        [Description("ΔE(Hunter)")]
        DEHUNTER,
    }
}
