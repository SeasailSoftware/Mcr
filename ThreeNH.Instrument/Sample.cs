using System;
using System.Collections.Generic;
using System.Text;
using ThreeNH.Color.Model;

namespace ThreeNH.Instrument
{
    public class Sample : ISample
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }

        public Spectrum Spectrum { get; set; }

        public float Temperature { get; set; }
    }
}
