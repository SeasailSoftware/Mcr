using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ThreeNH.Color.Enums
{
    // 颜色偏向
    [Flags]
    public enum ColorOffset
    {
        [Description("ColorOffset_None")]
        None, // 良好的颜色，偏向基本可忽略
        [Description("ColorOffset_Bright")]
        Bright = 1,  // 偏亮
        [Description("ColorOffset_Dark")]
        Dark = 2,   // 偏暗
        [Description("ColorOffset_Red")]
        Red = 4,    // 偏红
        [Description("ColorOffset_LackRed")]
        LackRed = 8, // 缺少红
        [Description("ColorOffset_Green")]
        Green = 16,  // 偏绿
        [Description("ColorOffset_LackGreen")]
        LackGreen = 32, // 缺少绿
        [Description("ColorOffset_Blue")]
        Blue = 64,  // 偏蓝
        [Description("ColorOffset_LackBlue")]
        LackBlue = 128, // 缺少蓝
        [Description("ColorOffset_Yellow")]
        Yellow = 256, // 偏黄
        [Description("ColorOffset_LackYellow")]
        LackYellow = 512, // 缺少黄
    }
}
