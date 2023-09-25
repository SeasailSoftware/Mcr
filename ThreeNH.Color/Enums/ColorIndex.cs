using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Color.Enums
{
    /// <summary>
    /// 顏色指數
    /// </summary>
    public enum ColorIndex
    {
        /// <summary>
        /// 黄度指数
        /// </summary>
        Yellowness,
        /// <summary>
        /// 白度指数
        /// </summary>
        Whiteness,
        /// <summary>
        /// 力份
        /// </summary>
        Strength,
        /// <summary>
        /// 同色异谱指数
        /// </summary>
        [Description("MI")]
        MetamerismIndex,
        /// <summary>
        /// 遮盖度
        /// </summary>
        Opacity,
        /// <summary>
        /// 555色调分类
        /// </summary>
        Shade555,
        /// <summary>
        /// 沾色牢度
        /// </summary>
        StainingFastness,
        /// <summary>
        /// 透射Garder指数
        /// </summary>
        GardnerIndex,
        /// <summary>
        /// 变色牢度
        /// </summary>
        ColorFastness,
        /// <summary>
        /// 钴铂指数
        /// </summary>
        PtCoIndex,
        /// <summary>
        /// 8 度光泽度
        /// </summary>
        Gloss8,
        /// <summary>
        /// 雾度指标
        /// </summary>
        Haze,
    }
}
