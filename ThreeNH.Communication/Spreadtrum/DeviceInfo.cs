using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Communication.Spreadtrum
{
    internal class DeviceInfo
    {
        /// <summary>
        /// 仪器型号
        /// </summary>
        public string InstrumentMode
        {
            get; set;
        }
        /// <summary>
        /// 光学结构
        /// </summary>
        public string OpticalStruct
        {
            get; set;
        }
        /// <summary>
        /// 硬件版本号
        /// </summary>
        public string HardwareVersion
        {
            get; set;
        }
        /// <summary>
        /// 软件版本号
        /// </summary>
        public string SoftwareVersion
        {
            get; set;
        }
        /// <summary>
        /// 内部白板编号
        /// </summary>
        public string WhiteBoardInnerNumber
        {
            get; set;
        }
        /// <summary>
        /// 外部白板编号
        /// </summary>
        public string WhiteBoardOuterNumber
        {
            get; set;
        }
        /// <summary>
        /// 容量
        /// </summary>
        public UInt16 Capacity
        {
            get; set;
        }
        /// <summary>
        /// 标样数
        /// </summary>
        public UInt16 StandardCount
        {
            get; set;
        }
        /// <summary>
        /// 试样数
        /// </summary>
        public UInt16 TestCount
        {
            get; set;
        }
        /// <summary>
        /// 黑校正标志
        /// </summary>
        public bool BlackCalibration
        {
            get; set;
        }
        /// <summary>
        /// 白校正标志
        /// </summary>
        public bool WhiteCalibration
        {
            get; set;
        }
        /// <summary>
        /// SN
        /// </summary>
        public string SN
        {
            get; set;
        }
        /// <summary>
        /// 定制名称
        /// </summary>
        public string ModelName
        {
            get; set;
        }
        /// <summary>
        /// 系统黑认容差
        /// </summary>
        public object Tolerance
        {
            get; set;
        }

        public override string ToString()
        {
            return string.Format("InstrumentMode: {0}\n" +
                "OpticalStruct: {1}\n" +
                "HardwareVersion: {2}\n" +
                "SoftwareVersion: {3}\n" +
                "WhiteBoardInnerNumber: {4}\n" +
                "WhiteBoardOuterNumber: {5}\n" +
                "Capacity: {6}\n" +
                "StandardCount: {7}\n" +
                "TestCount: {8}\n" +
                "BlackCalibration: {9}\n" +
                "WhiteCalibration: {10}\n" +
                "SN: {11}\n" +
                "ModelName: {12}\n" +
                "Tolerance: {13}\n",
                InstrumentMode,
                OpticalStruct,
                HardwareVersion,
                SoftwareVersion,
                WhiteBoardInnerNumber,
                WhiteBoardOuterNumber,
                Capacity,
                StandardCount,
                TestCount,
                BlackCalibration,
                WhiteCalibration,
                SN,
                ModelName,
                Tolerance);
        }
    }
}
