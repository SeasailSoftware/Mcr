using ThreeNH.Color.Enums;
using ThreeNH.Color.Model;

namespace ThreeNH.Color.Algorithm
{
    public class TristimulusWeightingFactor
    {

        /// <summary>
        /// 标准光源<see cref="StandardIlluminant"/>
        /// </summary>
        public StandardIlluminant Illuminant { get; set; }

        /// <summary>
        /// 观察者
        /// </summary>
        public StandardObserver Observer { get; set; }

        /// <summary>
        /// 白点位置
        /// </summary>
        public CIEXYZ WhitePortPosition { get; set; }

        public float[,] Factors { get; set; }

    }
}


