using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ThreeNH.Communication.Utility;
using ThreeNH.Communication;

namespace ThreeNH.Instrument
{
    public enum Command
    {
        InvalidCommand = 0,
        GetDeviceInfo = Constants.CUSTOM_COMMAND_START, // 获取设备信息
        GetInstrumentStatus,        // 获取仪器状态
        MeasureStandard,               // 测量标样
        MeasureTrial,          // 测量试样
        Calibrate,  // 校正
        GetWhiteBoardParameter,  // 获取白板参数
        SetWhiteBoardParameter,	// 设置校正板参数
        RtcGetDatetime,           // 读取RTC时间
        RtcSetDatetime,           // 设置RTC时间
        GetStandardCount,     // 获取标样个数
        GetTrialCount,        // 获取试样记录个数
        UploadStandards,       // 上传标样
        UploadTrials,           // 上传试样
        DownloadStandards,        // 下载标样
        Backup,
        Restore,
        GenerateAdLimit,        // 生成校准的AD限制
        SaveAdLimits,           // 存储生成的AD限制值
        GetInternalModel,       // 获取型号
        SetInternalModel,       // 设置型号
        DeleteStandard,        // 删除标样
        DeleteSample,          // 删除试样
        GetCameraImage,         // 获取相机影像
        GetSystemCfg,			// 获取系统配置
        SetSystemCfg,			// 设置系统配置
        GetFactors = 0x40,//获取K系数
        SetFactors = 0x41,
        GetWhiteBoardFactor = 0x42,//获取白板修正系数
        SetWhiteBoardFactor = 0x43,//设置白板修正系数
        ProductBlackCalibration = 0x47,      //生产黑校
        ProductWhiteCalibration = 0x48,    //生产白校
        GetKmtcMode = 0x49,      //读取Kmtc模式
        SetKmtcMode = 0x4A,       //设置Kmtc模式
        GetStandardNames = 0x4B,       //获取10个标样名称
        SetStandardNames = 0x4C,       //设置10个标样名称
        GetCIE1976Tolerance = 0x4D,       //获取CIE1976容差
        SetCIE1976Tolerance = 0x4E,       //设置CIE1976容差
        //调试命令
        GetProductEquipInfo = Constants.DEBUG_COMMAND_START,
        SetProductEquipInfo,        //A2
        DcMotorTimeGet,       // 获取DC马达旋转时间
        DcMotorTimeSet,       // 设置DC马达旋转时间
        DcMotorReset,          // 复位DC马达
        DcMotorTest,                // DC 马达测试A6
        LedTest,                    // LED测试A7
        CameraTest,                 // 相机测试A8
        FlashTest,                  // Flash测试A9
        SdramTest,                  // SDRAM测试AA
        BuzzerTest,                 // 蜂鸣器测试AB
        LcdTest,                    // LCD测试AC
        TouchScreenTest,            // 触屏测试AD
        AutoGetIntegralTime,        // 自动获取积分时间AE
        GetAdcData,				    // 获取ADC采样数据AF
        PrinterTest,                // 测试打印机B0
        GetCpuId,                   // 获取CPU ID B1
        SetEncryption,              // 设置加密码 B2
        GetMeasurementParameter,    // 获取测量参数B3
        SetMeasurementParameter,    // 设置测量参数B4
        FaultCheck,                 // 硬件检测B5
        BleSetName,				    // 设置BLE的名称
        BleSwitch,					// 开关蓝牙
        BleSendData                 // BLE发送串行数据
    }

    public static class Commands
    {
        /// <summary>
        /// 判断命令是否有应答数据
        /// </summary>
        /// <param name="command">要判断的命令</param>
        /// <returns>如果有返回true</returns>
        public static bool HasAckData(Command command)
        {
            switch (command)
            {
                case Command.GetDeviceInfo:
                case Command.GetInstrumentStatus:
                case Command.MeasureStandard:
                case Command.MeasureTrial:
                //                case Command.GetWavelengthCoef:
                case Command.GetWhiteBoardParameter:
                case Command.RtcGetDatetime:
                case Command.GetProductEquipInfo:
                case Command.AutoGetIntegralTime:
                //                case Command.Calibrate:
                case Command.GetStandardCount:
                case Command.GetTrialCount:
                case Command.UploadStandards:
                case Command.UploadTrials:
                case Command.GetInternalModel:
                case Command.GetCpuId:
                case Command.GetMeasurementParameter:
                case Command.DcMotorTimeGet:
                case Command.GetCameraImage:
                case Command.GetAdcData:
                case Command.GetSystemCfg:
                case Command.GetFactors:
                case Command.GetWhiteBoardFactor:
                case Command.GetKmtcMode:
                case Command.GetCIE1976Tolerance:
                case Command.GetStandardNames:
                    return true;

                default: return false;
            }
        }

        public static CommandPacket Measure(bool isStandard, ScMode? sc = null)
        {
            Command cmd = isStandard ? Command.MeasureStandard : Command.MeasureTrial;
            return sc == null ? new CommandPacket(cmd) : new CommandPacket(cmd, new byte[] { (byte)sc });
        }

        public static CommandPacket GetProductInfo(ProductInfoField field)
        {
            return new CommandPacket(Command.GetProductEquipInfo, new[] { (byte)field });
        }

        public static CommandPacket DcMotorTimeGet()
        {
            return new CommandPacket(Command.DcMotorTimeGet);
        }

        public static CommandPacket DcMotorTimeSet(UInt16 time)
        {
            return new CommandPacket(Command.DcMotorTimeSet, BitConverter.GetBytes(time));
        }

        public static CommandPacket DcMotorReset()
        {
            return new CommandPacket(Command.DcMotorReset);
        }


        /// <summary>
        /// DC马达测试
        /// </summary>
        /// <param name="scMode">测光方式</param>
        /// <param name="time">脉冲时间</param>
        public static CommandPacket DCMotorTest(ScMode scMode, ushort time)
        {
            Packer packer = new Packer();
            packer.Enqueue((byte)scMode);
            packer.Enqueue(time);
            return new CommandPacket(Command.DcMotorTest, packer.Bytes);
        }

        /// <summary>
        /// 下载标样到仪器
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
        public static CommandPacket DownloadStandards(StandardRecord[] records)
        {
            Packer packer = new Packer();
            foreach (var record in records)
            {
                packer.Enqueue(StructConverter.StructToBytes(record));
            }
            return new CommandPacket(Command.DownloadStandards, packer.Bytes);
        }




        /// <summary>
        /// 测试LED
        /// </summary>
        /// <param name="ledIndex">要测试LED的索引</param>
        /// <param name="on">如果为真点亮灯，否则关掉灯</param>
        public static CommandPacket LedTest(LedIndex ledIndex, bool on)
        {
            return new CommandPacket(Command.LedTest, new byte[] { (byte)ledIndex, (byte)(on ? 1 : 0) });
        }

        /// <summary>
        /// 相机测试
        /// </summary>
        /// <param name="on">如果为true打开相机，否则关掉相机</param>
        public static CommandPacket CameraTest(CameraTestCode code)
        {
            return new CommandPacket(Command.CameraTest, new byte[] { (byte)code });
        }

        /// <summary>
        /// 检测Flash
        /// </summary>
        public static CommandPacket FlashTest()
        {
            return new CommandPacket(Command.FlashTest);
        }

        /// <summary>
        /// 检测外部RAM
        /// </summary>
        public static CommandPacket SdramTest()
        {
            return new CommandPacket(Command.SdramTest);
        }

        /// <summary>
        /// 检测蜂鸣器
        /// </summary>
        public static CommandPacket BuzzerTest()
        {
            return new CommandPacket(Command.BuzzerTest);
        }

        /// <summary>
        /// 测试显示屏
        /// </summary>
        public static CommandPacket LcdTest()
        {
            return new CommandPacket(Command.LcdTest);
        }

        /// <summary>
        /// 测试触屏，显示指定数格子
        /// </summary>
        /// <param name="rowCount">格子行数</param>
        /// <param name="colCount">格子列数</param>
        public static CommandPacket TouchScreenTest(ushort rowCount, ushort colCount)
        {
            Packer packer = new Packer();
            packer.Enqueue(rowCount);
            packer.Enqueue(colCount);
            return new CommandPacket(Command.TouchScreenTest, packer.Bytes);
        }

        /// <summary>
        /// 获取RTC时间
        /// </summary>
        /// <returns>返回获取的时间</returns>
        /// <exception cref="CommunicationException"></exception>
        public static CommandPacket GetRtcDateTime()
        {
            return new CommandPacket(Command.RtcGetDatetime);
        }

        /// <summary>
        /// 设置RTC时间
        /// </summary>
        /// <param name="dateTime">要设置的日期</param>
        public static CommandPacket SetRtcDateTime(DateTime dateTime)
        {
            return new CommandPacket(Command.RtcSetDatetime, StructConverter.StructToBytes(new RtcDateTime
            {
                year = (ushort)dateTime.Year,
                date = (byte)dateTime.Day,
                month = (byte)dateTime.Month,
                hour = (byte)dateTime.Hour,
                minute = (byte)dateTime.Minute,
                second = (byte)dateTime.Second,
            }));
        }

        public static CommandPacket Restore()
        {
            return new CommandPacket(Command.Restore);
        }

        /// <summary>
        /// 自动获取积分时间
        /// </summary>
        /// <param name="isTransmission">如果为真获取透射模式的积分时间，否则获取反射</param>
        /// <param name="maxAdcValue">ADC最大采样值</param>
        /// <returns>相应的积分时间</returns>
        //public static CommandPacket AutoGetIntegralTime(bool isTransmission, ushort maxAdcValue)
        //{
        //    Packer packer = new Packer();
        //    packer.Enqueue((byte)(isTransmission ? 1 : 0));
        //    packer.Enqueue(maxAdcValue);

        //    return new CommandPacket(Command.AutoGetIntegralTime, packer.Bytes);
        //}

        /// <summary>
        /// 测试打印机
        /// </summary>
        public static CommandPacket TestPrinter()
        {
            return new CommandPacket(Command.PrinterTest);
        }

        public static CommandPacket GetDeviceInfo()
        {
            return new CommandPacket(Command.GetDeviceInfo);
        }

        public static CommandPacket GetInstrumentStatus()
        {
            return new CommandPacket(Command.GetInstrumentStatus);
        }

        public static CommandPacket Calibrate(bool isBlack)
        {
            return new CommandPacket((byte)Command.Calibrate, new byte[] { (byte)(isBlack ? 0 : 1) });
        }

        public static CommandPacket GetStandardCount()
        {
            return new CommandPacket((byte)Command.GetStandardCount);
        }

        public static CommandPacket GetWhiteBoardFactors()
        {
            return new CommandPacket((byte)Command.GetWhiteBoardFactor);
        }

        public static CommandPacket GetFactors()
        {
            return new CommandPacket((byte)Command.GetFactors);
        }

        /// <summary>
        /// 获取试样ID
        /// </summary>
        /// <param name="standardId">试样关联的标样ID，如果为-1获取所有试样数</param>
        /// <returns></returns>
        public static CommandPacket GetTrialCount(int standardId)
        {
            return new CommandPacket((byte)Command.GetTrialCount, BitConverter.GetBytes(standardId));
        }

        /// <summary>
        /// 上传标样
        /// </summary>
        /// <param name="from">起始序号</param>
        /// <param name="count">上传标样数，如果为-1上传from起的所有标样</param>
        /// <returns></returns>
        public static CommandPacket UploadStandards(int from = 0, int count = -1)
        {
            Packer packer = new Packer();
            packer.Enqueue(from);
            packer.Enqueue(count);
            return new CommandPacket((byte)Command.UploadStandards, packer.Bytes);
        }

        /// <summary>
        /// 上传试样
        /// </summary>
        /// <param name="standardId">关联的标样ID</param>
        /// <param name="from">起始序号</param>
        /// <param name="count">上传的试样数，如果为-1上传从from起的所有试样</param>
        /// <returns></returns>
        public static CommandPacket UploadTrials(int standardId, int from = 0, int count = -1)
        {
            Packer packer = new Packer();
            packer.Enqueue(standardId);
            packer.Enqueue(from);
            packer.Enqueue(count);
            return new CommandPacket((byte)Command.UploadTrials, packer.Bytes);
        }

        /// <summary>
        /// 生成校准的AD限制
        /// </summary>
        /// <param name="isTransmission">是否是透射模式</param>
        /// <param name="isWhite">是否是白校准</param>
        /// <returns></returns>
        public static CommandPacket GenerateAdLimit(Caliber caliber)
        {
            return new CommandPacket((byte)Command.GenerateAdLimit, new byte[] { (byte)caliber });
        }

        public static CommandPacket GetAdData()
        {
            return new CommandPacket((byte)Command.GetAdcData);
        }

        ///// <summary>
        ///// 存储生成的AD限制
        ///// </summary>
        ///// <returns></returns>
        //public static CommandPacket SaveAdLimits()
        //{
        //    return new CommandPacket((byte)Command.SaveAdLimits);
        //}

        /// <summary>
        /// 获取内部型号
        /// </summary>
        /// <returns></returns>
        public static CommandPacket GetInternalModel()
        {
            return new CommandPacket((byte)Command.GetInternalModel);
        }

        public static CommandPacket GetStandardNames()
        {
            return new CommandPacket((byte)Command.GetStandardNames);
        }

        public static CommandPacket GetCIE1976Tolerance()
        {
            return new CommandPacket((byte)Command.GetCIE1976Tolerance);
        }

        public static CommandPacket GetKmtcMode()
        {
            return new CommandPacket((byte)Command.GetKmtcMode);
        }

        public static CommandPacket ProductBlackCalibration()
        {
            return new CommandPacket((byte)Command.ProductBlackCalibration);
        }

        public static CommandPacket ProductWhiteCalibration()
        {
            return new CommandPacket((byte)Command.ProductWhiteCalibration);
        }



        /// <summary>
        /// 设置内部型号
        /// </summary>
        /// <param name="modelType">仪器内部型号</param>
        /// <returns></returns>
        public static CommandPacket SetInternalModel(byte modelType)
        {
            return new CommandPacket((byte)Command.SetInternalModel, new[] { modelType });
            //20191101 lilin change byte to 16位整形；因为ColorReader下位机为unsigned short; 之前为 new byte[] {(byte)modelType}
        }


        public static CommandPacket SetFactors(float factor)
        {
            return new CommandPacket((byte)Command.SetFactors, BitConverter.GetBytes(factor));
            //20191101 lilin change byte to 16位整形；因为ColorReader下位机为unsigned short; 之前为 new byte[] {(byte)modelType}
        }

        /// <summary>
        /// 获取仪器的CPU ID
        /// </summary>
        /// <returns></returns>
        public static CommandPacket GetCpuId()
        {
            return new CommandPacket((byte)Command.GetCpuId);
        }

        /// <summary>
        /// 设置数据加密密钥
        /// </summary>
        /// <param name="encryption">加密密钥</param>
        /// <returns></returns>
        public static CommandPacket SetEncryption(ushort encryption)
        {
            return new CommandPacket((byte)Command.SetEncryption, BitConverter.GetBytes(encryption));
        }

        public static CommandPacket SetKmtcMode(byte kmtcMode)
        {
            return new CommandPacket((byte)Command.SetKmtcMode, new byte[] { kmtcMode });
        }

        public static CommandPacket GetWhiteBoardParameter()
        {
            return new CommandPacket((byte)Command.GetWhiteBoardParameter);
        }


        //        public static CommandPacket GetWavelengthCoef()
        //        {
        //            return new CommandPacket((byte)Command.GetWavelengthCoef);
        //        }
        //
        //        public static CommandPacket SetWavelengthCoef(ColorReader70SpectrometerParameters coefficients)
        //        {
        //            return new CommandPacket((byte)Command.SetWavelengthCoef, 
        //                StructConverter.StructToBytes(coefficients.ToSpectrometerParameters()));
        //        }

        /// <summary>
        /// 删除指定序号的标样
        /// </summary>
        /// <param name="standardIndex">要删除的标样的序号；如果为-1删除所有样品</param>
        /// <returns>删除标样的命令包</returns>
        public static CommandPacket DeleteStandard(int standardIndex)
        {
            return new CommandPacket((byte)Command.DeleteStandard, BitConverter.GetBytes(standardIndex));
        }

        /// <summary>
        /// 删除指定试样
        /// </summary>
        /// <param name="standardId">试样关联的标样的ID；如果为-1删除所有试样</param>
        /// <param name="sampleIndex">试样的序号；如果为-1且标样ID不为-1删除相应标样关联的所有试样</param>
        /// <returns></returns>
        public static CommandPacket DeleteSample(int standardId, int sampleIndex)
        {
            Packer packer = new Packer();
            packer.Enqueue(standardId);
            packer.Enqueue(sampleIndex);
            return new CommandPacket((byte)Command.DeleteSample, packer.Bytes);
        }






        public static CommandPacket FaultCheck(FaultMode faultMode)
        {
            return new CommandPacket((byte)Command.FaultCheck, new[] { (byte)faultMode });
        }

        public static CommandPacket GetCameraImage()
        {
            return new CommandPacket((byte)Command.GetCameraImage);
        }

        /// <summary>
        /// 设置蓝牙名
        /// </summary>
        /// <param name="name">蓝牙名称，转换成UTF8编码后长度不能超过Constants.BLE_NAME_LENGTH</param>
        /// <returns></returns>
        public static CommandPacket BleSetName(string name)
        {
            var bytes = Encoding.UTF8.GetBytes(name);
            if (bytes.Length > Constants.BLE_NAME_LENGTH)
            {
                throw new CommunicationException(CommunicationError.InvalidParameter,
                    $"The length of bluetooth name can't greater than {Constants.BLE_NAME_LENGTH}'");
            }
            return new CommandPacket((byte)Command.BleSetName, bytes);
        }

        /// <summary>
        /// 开关蓝牙命令
        /// </summary>
        /// <param name="on">true开启，否则关闭</param>
        /// <returns></returns>
        public static CommandPacket BleSwitch(bool on)
        {
            return new CommandPacket((byte)Command.BleSwitch, new byte[] { on ? (byte)1 : (byte)0 });
        }

        public static CommandPacket BleSendData(byte[] bytes)
        {
            return new CommandPacket((byte)Command.BleSendData, bytes);
        }

        public static CommandPacket GetSystemCfg(SystemCfgField field)
        {
            return new CommandPacket((byte)Command.GetSystemCfg, new byte[] { (byte)field });
        }

    }
}
