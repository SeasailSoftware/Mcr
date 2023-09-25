using System;
using System.Collections.Generic;
using System.Text;
using ThreeNH.Color.Model;

namespace ThreeNH.Instrument
{
    public interface IInstrument : IDisposable
    {
        /// <summary>
        /// 仪器信息
        /// </summary>
        IDictionary<string, object> InstrumentInformation { get; }



        /// <summary>
        /// 是否已打开
        /// </summary>
        bool IsOpen { get; }

        /// <summary>
        /// 打开连接
        /// </summary>
        /// <param name="device">连接参数</param>
        void Open(object device = null);

        /// <summary>
        /// 关闭连接
        /// </summary>
        void Close();

        /// <summary>
        /// 测量
        /// </summary>
        /// <returns>成功返回测量如果，否则抛出异常</returns>
        ISample Measure(bool standard = false);

        /// <summary>
        /// 黑校
        /// </summary>
        void CalibrateBlack();

        /// <summary>
        /// 白校
        /// </summary>
        void CalibrateWhite();

        Spectrum GetWhiteboardData();
    }
}
