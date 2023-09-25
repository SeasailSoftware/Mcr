using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Color.Enums
{
    public enum ColorSpace
    {
        [Description("CIE XYZ")]
        CIE_XYZ,    // CIE XYZ
        [Description("CIE Yxy")]
        CIE_Yxy,    // CIE Yxy
        [Description("CIE Lab")]
        CIE_LAB,    // CIE LAB
        [Description("CIE LCh")]
        CIE_LCH,    // CIE LCH
        [Description("CIE Luv")]
        CIE_LUV,    // CIE LUV
        [Description("Hunter Lab")]
        HUNTER_LAB, // Hunter Lab
        [Description("sRGB")]
        sRGB,
        [Description("DIN99 Lab")]
        DIN99_LAB,
        [Description("Munsell")]
        Munsell,    // 孟赛尔
        [Description("βxy")]
        BETA_xy,    // βxy
    }
}
