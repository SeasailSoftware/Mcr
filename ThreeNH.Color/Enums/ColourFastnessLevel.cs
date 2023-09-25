using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Color.Enums
{
    // 色牢度等级
    public enum ColourFastnessLevel
    {
        [Description("Invalid")]
        INVALID,        // 无效
        [Description("1")]
        LEVEL_1,        // 1
        [Description("1-2")]
        LEVEL_1_2,      // 1-2
        [Description("2")]
        LEVEL_2,        // 2
        [Description("2-3")]
        LEVEL_2_3,      // 2-3
        [Description("3")]
        LEVEL_3,        // 3
        [Description("3-4")]
        LEVEL_3_4,      // 3-4
        [Description("4")]
        LEVEL_4,        // 4
        [Description("4-5")]
        LEVEL_4_5,      // 4-5
        [Description("5")]
        LEVEL_5,        // 5
    };
}
