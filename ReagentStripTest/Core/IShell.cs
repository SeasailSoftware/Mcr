using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeNH.Instrument;

namespace ReagentStripTest.Core
{
    public interface  IShell
    {
        IInstrument Instrument { get; }
    }
}
