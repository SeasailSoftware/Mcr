using System;
using System.Collections.Generic;
using System.Text;
using ThreeNH.Color.Model;

namespace ThreeNH.Instrument
{
    public interface ISample
    {
        Guid Id { get; }

        DateTime DateTime { get; }

        Spectrum Spectrum { get; set; }

        float Temperature { get; }
    }
}
