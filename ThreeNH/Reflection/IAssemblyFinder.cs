using ThreeNH.Dependency;
using ThreeNH.Finder;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ThreeNH.Reflection
{
    /// <summary>
    /// 定义程序集查找器
    /// </summary>
    [IgnoreDependency]
    public interface IAssemblyFinder : IFinder<Assembly>
    { }
}
