using System.ComponentModel;

namespace ThreeNH.Color.Enums
{
    /// <summary>
    /// 标准光源类型
    /// </summary>
    public enum StandardIlluminant
    {
        D65,
        D50,
        A,
        C,
        D55,
        D75,
        F1,
        /// <summary>
        /// Fcw/CWF
        /// </summary>
        [Description("F2(Fcw/CWF)")]
        F2,
        F3,
        F4,
        F5,
        F6,
        /// <summary>
        /// DLF
        /// </summary>
        [Description("F7(DLF)")]
        F7,
        F8,
        F9,
        /// <summary>
        /// TPL5
        /// </summary>
        [Description("F10(TPL5")]
        F10,
        /// <summary>
        /// TL4/TL84
        /// </summary>
        [Description("F11(TL4/TL84)")]
        F11,
        /// <summary>
        /// TL83/U30/U3000
        /// </summary>
        [Description("F12(TL83/U30/U3000)")]
        F12
    }
}
