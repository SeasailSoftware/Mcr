using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Data.Implement.SampleProperty
{
    /// <summary>
    /// 样品数据项求值器
    /// </summary>
    /// <param name="settings">样品数据项的属性设置</param>
    /// <param name="sample">样品</param>
    /// <param name="target">样品关联的标样（视乎属性不同，可能不会用到该参数）</param>
    /// <returns>所求数据项</returns>
    public delegate object EvaluateSampleProperty(IDictionary<string, object> settings, ISample sample, ISample target);

    /// <summary>
    /// 样品属性信息
    /// </summary>
    public class SamplePropertyInfo
    {
        /// <summary>
        /// 样品属性名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据项的类型
        /// </summary>
        public Type ValueType { get; set; }

        /// <summary>
        /// 样品属性设置
        /// </summary>
        public IDictionary<string, object> PropertySettings { get; set; }

        /// <summary>
        /// 求值器，第一个参数为属性值，第二个为
        /// </summary>
        public EvaluateSampleProperty Evaluate { get; set; }
    }
}
