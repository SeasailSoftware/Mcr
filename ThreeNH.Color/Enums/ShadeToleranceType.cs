using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ThreeNH.Color.Enums
{
    public enum ShadeToleranceType
    {
        [Description("ΔL*,Δa*,Δb*")]
        DeltaLab,
        [Description("ΔL*,ΔC*,ΔH*")]
        DeltaLCH,
        [Description("ΔL(Hunter),Δa(Hunter),Δb(Hunter)")]
        DeltaHunterLab,
        [Description("ΔL(DIN99),Δa(DIN99),Δb(DIN99)")]
        DeltaDIN99Lab,
    }
}
