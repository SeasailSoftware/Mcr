using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.i18N
{
    public interface ITranslater
    {
        string Trans(string key);
        string Trans(string key, params object[] args);
    }
}
