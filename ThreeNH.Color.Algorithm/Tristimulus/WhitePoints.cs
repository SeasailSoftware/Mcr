using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeNH.Color.Algorithm.Tristimulus;
using ThreeNH.Color.Enums;
using ThreeNH.Color.Model;

namespace ThreeNH.Color.Algorithm.Tristimulus
{
    public static class WhitePoints
    {
        public static CIEXYZ Get(StandardIlluminant illuminant, StandardObserver observer)
        {
            return TristimulusWeightingFactorTable.TRISTIMULUS_WEIGHTING_WEIGHTING_FACTORS_10NM_TABLE[observer, illuminant].WhitePortPosition;
        }
    }
}
