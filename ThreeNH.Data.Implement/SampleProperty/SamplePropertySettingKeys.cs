using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Data.Implement.SampleProperty
{
    /// <summary>
    /// 样品属性设置的键
    /// </summary>
    public static class SamplePropertySettingKeys
    {

        public const string Shade555Setting = "Shade555Setting";
        /// <summary>
        /// 555色调分类类型
        /// </summary>
        public const string ShadeToleranceType = "ShadeToleranceType";
        /// <summary>
        /// 555 shade number 的L*的容差
        /// </summary>
        public const string ShadeToleranceL = "ShadeTolerance_L";

        /// <summary>
        /// 555 shade number 的a*的容差
        /// </summary>
        public const string ShadeToleranceA = "ShadeTolerance_A";

        /// <summary>
        /// 555 shade number 的b*的容差
        /// </summary>
        public const string ShadeToleranceB = "ShadeTolerance_B";

        /// <summary>
        /// 555 shade number 的C*的容差
        /// </summary>
        public const string ShadeToleranceC = "ShadeTolerance_C";

        /// <summary>
        /// 555 shade number 的H*的容差
        /// </summary>
        public const string ShadeToleranceH = "ShadeTolerance_H";

        /// <summary>
        /// 555 shade number 的LDIN99的容差
        /// </summary>
        public const string ShadeToleranceLDIN99 = "ShadeTolerance_LDIN99";
        /// <summary>
        /// 555 shade number 的aDIN99的容差
        /// </summary>
        public const string ShadeToleranceADIN99 = "ShadeTolerance_ADIN99";
        /// <summary>
        /// 555 shade number 的bDIN99的容差
        /// </summary>
        public const string ShadeToleranceBDIN99 = "ShadeTolerance_BDIN99";

        /// <summary>
        /// 555 shade number 的LHunter的容差
        /// </summary>
        public const string ShadeToleranceLHunter = "ShadeTolerance_LHunter";
        /// <summary>
        /// 555 shade number 的aHunter的容差
        /// </summary>
        public const string ShadeToleranceAHunter = "ShadeTolerance_AHunter";
        /// <summary>
        /// 555 shade number 的bHunter的容差
        /// </summary>
        public const string ShadeToleranceBHunter = "ShadeTolerance_BHunter";

        /// <summary>
        /// CIEDE 2000 L的参数因子
        /// </summary>
        public const string CIEDE00FactorL = "KL00";

        /// <summary>
        /// CIEDE 2000 C的参数因子
        /// </summary>
        public const string CIEDE00FactorC = "KC00";

        /// <summary>
        /// CIEDE 2000 H的参数因子
        /// </summary>
        public const string CIEDE00FactorH = "KH00";

        /// <summary>
        /// CIEDE 1994 L的参数因子
        /// </summary>
        public const string CIEDE94FactorL = "KL94";

        /// <summary>
        /// CIEDE 1994 C的参数因子
        /// </summary>
        public const string CIEDE94FactorC = "KC94";

        /// <summary>
        /// CIEDE 1994 H的参数因子
        /// </summary>
        public const string CIEDE94FactorH = "KH94";

        /// <summary>
        /// CMCDE L的参数因子
        /// </summary>
        public const string CMCDEFactorL = "KLcmc";

        /// <summary>
        /// CMCDE C的参数因子
        /// </summary>
        public const string CMCDEFactorC = "KCcmc";

        /// <summary>
        /// CIEDE2000的参数因子，类型为ICIEDE2000Factors
        /// </summary>
        public const string CIEDE00Factors = "CIEDE00Factors";

        /// <summary>
        /// CIEDE 1994的参数因子，类型为ICIEDE1994Factors
        /// </summary>
        public const string CIEDE94Factors = "CIEDE94Factors";

        /// <summary>
        /// CMCDE 的参数因子，类型为ICMCDEFactors
        /// </summary>
        public const string CMCDEFactors = "CMCDEFactors";

        /// <summary>
        /// 颜色通道
        /// </summary>
        public const string Channel = "Channel";

        /// <summary>
        /// 颜色设置
        /// </summary>
        public const string ColorSetting = "ColorSetting";

        /// <summary>
        /// 光源
        /// </summary>
        public const string Illuminant = "Illuminant";

        public const string ColorSpace = "ColorSpace";

        /// <summary>
        /// 观察者
        /// </summary>
        public const string Observer = "Observer";

        public const string RgbColorSystem = "RgbColorSystem";

        public const string RgbGamma = "RgbGamma";

        /// <summary>
        /// 参考光源设置，其值类型为StandardIlluminant
        /// </summary>
        public const string RefIlluminant = "RefIlluminant";

        /// <summary>
        /// 参考观察者角度设置，其值类型为StandardObserver
        /// </summary>
        public const string RefObserver = "RefObserver";
    }
}
