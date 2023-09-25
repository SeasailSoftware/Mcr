using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Color.Enums
{
    public enum ColorFastnessGrade
    {
        // 色牢度等级
        [Description("Undefine")]
        clralgo_COLOR_FASTNESS_UNDEFINED,   // 色牢度指数在指定条件下未定义
        [Description("1")]
        clralgo_GRADE_1,        // 1
        [Description("1-1")]
        clralgo_GRADE_1_2,      // 1-2
        [Description("2")]
        clralgo_GRADE_2,        // 2
        [Description("2-3")]
        clralgo_GRADE_2_3,      // 2-3
        [Description("3")]
        clralgo_GRADE_3,        // 3
        [Description("3-4")]
        clralgo_GRADE_3_4,      // 3-4
        [Description("4")]
        clralgo_GRADE_4,        // 4
        [Description("4-5")]
        clralgo_GRADE_4_5,      // 4-5
        [Description("5")]
        clralgo_GRADE_5,        // 5
    }
}
