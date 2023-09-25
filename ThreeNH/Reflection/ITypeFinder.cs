using ThreeNH.Dependency;
using ThreeNH.Finder;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThreeNH.Reflection
{
    /// <summary>
    /// 定义类型查找行为
    /// </summary>
    [IgnoreDependency]
    public interface ITypeFinder : IFinder<Type>
    { }
}
