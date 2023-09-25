using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Data
{
    public interface ISpectralData
    {
        /// <summary>
        /// 光谱个数
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 光谱结束波长
        /// </summary>
        int WavelengthEnd { get; }

        /// <summary>
        /// 波长间隔
        /// </summary>
        int WavelengthInterval { get; }

        /// <summary>
        /// 光谱起始波长
        /// </summary>
        int WavelengthBegin { get; }

        /// <summary>
        /// 反射或投射标志
        /// </summary>
        bool TransmissionFlag { get; }

        /// <summary>
        /// 光谱数据
        /// </summary>
        double[] Data { get; }

        double this[int wavelength] { get; }

        bool Contains(int wavelength);

    }
}
