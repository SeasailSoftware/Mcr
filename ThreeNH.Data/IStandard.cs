using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeNH.Color.Enums;

namespace ThreeNH.Data
{
    public interface IStandard : ISample
    {
        ITolerance Tolerance { get; set; }

    }
}
