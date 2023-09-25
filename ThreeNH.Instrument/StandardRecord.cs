using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using ThreeNH.Color.Enums;
using ThreeNH.Color.Model;
using ThreeNH.Communication.Utility;

namespace ThreeNH.Instrument
{
    // 产品参数字段
    public enum ProductInfoField : byte
    {
        INTERNAL_MODEL,				// 内部型号
        DEFAULT_LANGUAGE,				// 默认语言
        DEVICE_MODEL_NAME,             // 仪器内部型号名
        SOFTWARE_VERSION,              // 软件版本
        HARDWARE_VERSION,              // 硬件版本
        SN,
        BLACK_PLATE,
        WHITE_PLATE,
        SPECTROGRAPH,
        UPDATE,
        REFLECT_MODIFY, // 反射率修正系数，其他各种修正参数在这里增加  
        CAMERA_CENTER,  // 相机中心点
        MOTOR_TIME, // 马达时间
        THRESHOLD_8MM, // PHI8白板限制
        THRESHOLD_4MM, // PHI4白板限制
        CUSTOMIZE_CALIBER,  // 定制口径
        SENSOR_TYPE,        // 感应器极性设置
        IS_DEMONSTRATOR,     // 是否是演示机型
        //-------
        ALL = 0xFF,	// 获取整个结构体
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct StandardRecord
    {
        private RecordFlags _flags;

        private int _id;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.RECORD_NAME_MAX)]
        private string _name;

        private RtcDateTime _dateTime;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.VALID_WAVE_NUM * 2)]
        private byte[] _sci;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.VALID_WAVE_NUM * 2)]
        private byte[] _sce;

        private Tolerance _tolerance;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 45)]
        private byte[] _labs;

        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 77)]
        //private byte[] _reserve; // 保留字节，对齐到256字节，flash一页的长度

        private MNRecord _record;

        private ushort _checkSum; // 校验和



        public RecordFlags Flags
        {
            get { return _flags; }
            set { _flags = value; }
        }

        public string Name
        {
            get { return _name; }

            set { _name = value; }
        }

        internal DateTime DateTime
        {
            get { return _dateTime.ToDateTime(); }

            set { _dateTime = new RtcDateTime(value); }
        }

        //public float Temperature
        //{
        //    get { return _temperature; }

        //    set { _temperature = value; }
        //}

        internal Tolerance Tolerance
        {
            get { return _tolerance; }

            set { _tolerance = value; }
        }

        public ushort CheckSum
        {
            get { return _checkSum; }

            set { _checkSum = value; }
        }

        public double[] SciSpectrum
        {
            get { return Flags.IsSpectrum ? StructConverter.BytesToStruct<RecordSpectrumData>(_sci).Spectrum : null; }
            set
            {
                if (!Flags.IsSpectrum)
                {
                    throw new ArgumentException("Flags.IsSpectrum is false");
                }

                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                var tmp = new RecordSpectrumData { Spectrum = value };
                _sci = StructConverter.StructToBytes(tmp);
            }
        }

        public double[] SceSpectrum
        {
            get { return Flags.IsSpectrum ? StructConverter.BytesToStruct<RecordSpectrumData>(_sce).Spectrum : null; }
            set
            {
                if (!Flags.IsSpectrum)
                {
                    throw new ArgumentException("Flags.IsSpectrum is false");
                }

                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                var tmp = new RecordSpectrumData { Spectrum = value };
                _sce = StructConverter.StructToBytes(tmp);
            }
        }

        public RecordColorData? SciColorData
        {
            get
            {
                return !Flags.IsSpectrum ? StructConverter.BytesToStruct<RecordColorData>(_sci) : (RecordColorData?)null;
            }
            set
            {
                if (Flags.IsSpectrum)
                {
                    throw new ArgumentException("不能是光谱数据");
                }

                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                _sci = new byte[Constants.VALID_WAVE_NUM * 2];
                var temp = StructConverter.StructToBytes(value);
                Array.Copy(temp, _sci, temp.Length);
            }
        }

        public RecordColorData? SceColorData
        {
            get
            {
                return !Flags.IsSpectrum ? StructConverter.BytesToStruct<RecordColorData>(_sce) : (RecordColorData?)null;
            }
            set
            {
                if (Flags.IsSpectrum)
                {
                    throw new ArgumentException("不能是光谱数据");
                }

                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                _sce = new byte[Constants.VALID_WAVE_NUM * 2];
                var temp = StructConverter.StructToBytes(value);
                Array.Copy(temp, _sce, temp.Length);
            }
        }

        public int Id
        {
            get { return _id; }

            set { _id = value; }
        }


        public float Temperature => _record.Temperature;

    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct MNRecord
    {
        private byte _id;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
        private byte[] _name;
        private ushort _year;
        private byte _month;
        private byte _date;
        private byte _hour;
        private byte _minute;
        private float _a;
        private float _b;
        private float _result;
        private byte _unit;
        private float _temperature;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
        private byte[] _reverse;

        public float Temperature
        {
            get => _temperature;
            set => _temperature = value;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct TrialRecord
    {
        private RecordFlags _flags;

        private int _id;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.RECORD_NAME_MAX)]
        private string _name;

        private RtcDateTime _dateTime;

        private uint _stdId;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.VALID_WAVE_NUM * 2)]
        private byte[] _sci;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.VALID_WAVE_NUM * 2)]
        private byte[] _sce;

        //        private float _temperature;


        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 101)]
        private byte[] _reserve; // 保留字节，对齐到256字节，flash一页的长度

        private ushort _checkSum; // 校验和

        public RecordFlags Flags
        {
            get { return _flags; }
            set { _flags = value; }
        }

        public string Name
        {
            get { return _name; }

            set { _name = value; }
        }

        internal DateTime DateTime
        {
            get { return _dateTime.ToDateTime(); }

            set { _dateTime = new RtcDateTime(value); }
        }

        //public float Temperature
        //{
        //    get { return _temperature; }

        //    set { _temperature = value; }
        //}


        public ushort CheckSum
        {
            get { return _checkSum; }

            set { _checkSum = value; }
        }

        public double[] SciSpectrum
        {
            get { return Flags.IsSpectrum ? StructConverter.BytesToStruct<RecordSpectrumData>(_sci).Spectrum : null; }
            set
            {
                if (!Flags.IsSpectrum)
                {
                    throw new ArgumentException("Flags.IsSpectrum is false");
                }

                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                var tmp = new RecordSpectrumData { Spectrum = value };
                _sci = StructConverter.StructToBytes(tmp);
            }
        }

        public double[] SceSpectrum
        {
            get { return Flags.IsSpectrum ? StructConverter.BytesToStruct<RecordSpectrumData>(_sce).Spectrum : null; }
            set
            {
                if (!Flags.IsSpectrum)
                {
                    throw new ArgumentException("Flags.IsSpectrum is false");
                }

                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                var tmp = new RecordSpectrumData { Spectrum = value };
                _sce = StructConverter.StructToBytes(tmp);
            }
        }


        private static RecordColorData? ToRecordColorData(RecordFlags flags, byte[] data)
        {
            if (!flags.IsSpectrum)
            {
                Observer observer = (Observer)data[0];
                Illuminant illuminant = (Illuminant)data[1];
                float[] values = new[]
                {
                        BitConverter.ToSingle(data, 2),
                        BitConverter.ToSingle(data, 6),
                        BitConverter.ToSingle(data, 10)
                };

                return new RecordColorData
                {
                    Observer = (byte)observer,
                    Illuminant = (byte)illuminant,
                    Values = values
                };
            }
            return null;
        }


        public RecordColorData? SciColorData
        {
            get => ToRecordColorData(Flags, _sci);
            set
            {
                if (!Flags.IsSpectrum)
                {
                    throw new ArgumentException("Flags.IsXyz is false");
                }

                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                _sci = StructConverter.StructToBytes(value);
            }
        }

        public RecordColorData? SceColorData
        {
            get => ToRecordColorData(Flags, _sce);
            set
            {
                if (!Flags.IsSpectrum)
                {
                    throw new ArgumentException("Flags.IsXyz is false");
                }

                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                _sce = StructConverter.StructToBytes(value);
            }
        }

        public uint StandardId
        {
            get { return _stdId; }
            set { _stdId = value; }
        }

        public int Id
        {
            get { return _id; }

            set { _id = value; }
        }

    }

    [StructLayout(LayoutKind.Sequential, Size = Constants.VALID_WAVE_NUM, Pack = 1)]
    public struct RecordColorData
    {
        public byte Observer; // 观察者角度
        public byte Illuminant; // 光源
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] Values;

    }

    [StructLayout(LayoutKind.Sequential, Size = Constants.VALID_WAVE_NUM, Pack = 1)]
    public struct RecordSpectrumData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.VALID_WAVE_NUM)]
        private ushort[] _rawData;

        public double[] Spectrum
        {
            get { return _rawData != null ? Array.ConvertAll(_rawData, x => x * 0.0001) : null; }
            set
            {
                if (value != null && value.Length != Constants.VALID_WAVE_NUM)
                {
                    throw new ArgumentException($"Length must be is {Constants.VALID_WAVE_NUM}");
                }

                _rawData = value == null ? null : Array.ConvertAll(value, x => (ushort)(x * 10000));
            }
        }

    }

    /// <summary>
    /// RTC时间
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct RtcDateTime
    {
        public ushort year;
        public byte month;
        public byte date;
        public byte hour;
        public byte minute;
        public byte second;
        public byte reserved;

        public DateTime ToDateTime()
        {
            try
            {
                return new DateTime(year, month, date, hour, minute, second);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public RtcDateTime(DateTime dateTime)
        {
            year = (ushort)dateTime.Year;
            month = (byte)dateTime.Month;
            date = (byte)dateTime.Day;
            hour = (byte)dateTime.Hour;
            minute = (byte)dateTime.Minute;
            second = (byte)dateTime.Second;
            reserved = 0;
        }

    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Tolerance
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public float[] Values;

        public float dE
        {
            get { return Values[0]; }
            set { Values[0] = value; }
        }

        public float dL_Lower
        {
            get { return Values[1]; }
            set { Values[1] = value; }
        }

        public float da_Lower
        {
            get { return Values[2]; }
            set { Values[2] = value; }
        }

        public float db_Lower
        {
            get { return Values[3]; }
            set { Values[3] = value; }
        }

        public float dL_Upper
        {
            get { return Values[4]; }
            set { Values[4] = value; }
        }

        public float da_Upper
        {
            get { return Values[5]; }
            set { Values[5] = value; }
        }

        public float db_Upper
        {
            get { return Values[6]; }
            set { Values[6] = value; }
        }

    }

    /// <summary>
    /// 设备信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct DeviceInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        string model; // 仪器型号

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        string optical; // 光学结构

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        string sn; // 序列号

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        string software_version; // 软件版本号

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        string hardware_version; // 硬件版本号

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        string white_board_number; // 白板编号

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        string black_bucket_number; // 黑桶编号

        ushort standard_capacity; // 标样容量
        ushort trial_capacity; // 试样容量
        //ushort standard_count; // 已存标样数
        //ushort trial_count; // 已存试样数
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        private byte[] reserved;

        public string Model
        {
            get { return model; }

            set { model = value; }
        }

        public string Optical
        {
            get { return optical; }

            set { optical = value; }
        }

        public string Sn
        {
            get { return sn; }

            set { sn = value; }
        }

        public string SoftwareVersion
        {
            get { return software_version; }

            set { software_version = value; }
        }

        public string HardwareVersion
        {
            get { return hardware_version; }

            set { hardware_version = value; }
        }

        public string WhiteBoardNumber
        {
            get { return white_board_number; }

            set { white_board_number = value; }
        }

        public string BlackBucketNumber
        {
            get { return black_bucket_number; }

            set { black_bucket_number = value; }
        }

        public ushort StandardCapacity
        {
            get { return standard_capacity; }

            set { standard_capacity = value; }
        }

        public ushort TrialCapacity
        {
            get { return trial_capacity; }

            set { trial_capacity = value; }
        }
    };

    /// <summary>
    /// 白板参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct CalibrationBoard
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        private string _sn;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        private string _innerNumber;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.VALID_WAVE_NUM)]
        private float[] _sci;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.VALID_WAVE_NUM)]
        private float[] _sce;

        public string Sn
        {
            get
            {
                return _sn;
            }

            set
            {
                if (value?.Length >= 16)
                {
                    throw new ArgumentException("Length of value must be less than 16");
                }
                _sn = value ?? string.Empty;
            }
        }

        public string InnerNumber
        {
            get => _innerNumber;
            set
            {
                if (value?.Length >= 16)
                {
                    throw new ArgumentException("Length of value must be less than 16");
                }

                _innerNumber = value ?? String.Empty;
            }
        }

        public float[] Sci
        {
            get
            {
                return _sci ?? (_sci = new float[Constants.VALID_WAVE_NUM]);
            }

            set
            {
                if (value?.Length != Constants.VALID_WAVE_NUM)
                {
                    throw new ArgumentException(string.Format("Length of value must be is {0}", Constants.VALID_WAVE_NUM));
                }
                _sci = value;
            }
        }

        public float[] Sce
        {
            get
            {
                return _sce ?? (_sce = new float[Constants.VALID_WAVE_NUM]);
            }

            set
            {
                if (value?.Length != Constants.VALID_WAVE_NUM)
                {
                    throw new ArgumentException(string.Format("Length of value must be is {0}", Constants.VALID_WAVE_NUM));
                }
                _sce = value;
            }
        }
    }

    // 产品参数 - 版本1
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct ProductInfo
    {
        private UInt32 flags; // 为1

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        private string device_model_name;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        private string sn;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        private string software_version;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        private string hardward_version;
        private SpectrographParameter spectrograph;
        private CalibrationBoard white_plate;
        private CalibrationBoard black_plate;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.VALID_WAVE_NUM)]
        private float[] reflect_modify; // 反射率修正系数
        UInt16 motor_time;
        UInt16 camera_x;
        UInt16 camera_y;
        UInt16 encrypt;
        byte device_type;
        byte default_language; 	// 默认语言，恢复出厂设置时要恢到该语言

        float threshold_8mm; // 8mm口径白板限制
        float threshold_4mm; // 4mm口径白板限制

        Caliber customize_caliber;  //定制口径大小 0为4mm 1为8mm
        SensorType sensor_type;		  // 极性设置，有效值在_SENSOR_TYPE中定义

        private void checkSpectralData(float[] spectralData)
        {
            if (spectralData == null || spectralData.Length != Constants.VALID_WAVE_NUM)
            {
                throw new ArgumentException();
            }
        }

        public string DeviceModelName
        {
            get { return device_model_name; }
            set { device_model_name = value; }
        }

        public string SoftwareVersion
        {
            get { return software_version; }
            set { software_version = value; }
        }

        public string HardwardVersion
        {
            get { return hardward_version; }
            set { hardward_version = value; }
        }

        public string SN
        {
            get { return sn; }
            set { sn = value; }
        }

        public CalibrationBoard BlackPlate
        {
            get { return black_plate; }
            set { black_plate = value; }
        }

        public CalibrationBoard WhitePlate
        {
            get { return white_plate; }
            set { white_plate = value; }
        }

        public Spectrum WhiteboardData => new Spectrum(new WavelengthRange(Constants.VALID_WAVE_BEGIN, Constants.VALID_WAVE_END, Constants.VALID_WAVE_STEP), Array.ConvertAll(WhitePlate.Sci, x => Convert.ToDouble(x)));


        public SpectrographParameter Spectrograph
        {
            get { return spectrograph; }
            set { spectrograph = value; }
        }

        public float[] ReflectModify
        {
            get { return reflect_modify; }
            set { reflect_modify = value; }
        }


        public ushort MotorTime
        {
            get => motor_time;
            set => motor_time = value;
        }

        public ushort CameraX
        {
            get => camera_x;
            set => camera_x = value;
        }

        public ushort CameraY
        {
            get => camera_y;
            set => camera_y = value;
        }

        public ushort Encrypt
        {
            get => encrypt;
            set => encrypt = value;
        }



        public byte DeviceType
        {
            get => device_type;
            set => device_type = (byte)value;
        }

        public float Threshold8Mm
        {
            get => threshold_8mm;
            set => threshold_8mm = value;
        }

        public float Threshold4Mm
        {
            get => threshold_4mm;
            set => threshold_4mm = value;
        }

        public Caliber CustomizeCaliber
        {
            get => customize_caliber;
            set => customize_caliber = value;
        }

        public SensorType SensorType1
        {
            get => sensor_type;
            set => sensor_type = value;
        }

        public uint Flags => flags;

        public byte Version => (byte)(flags & 0xFF);

        public bool IsDemonstrator
        {
            get => (flags & 0x00000100) != 0;
            set
            {
                if (value)
                {
                    flags |= 0x00000100U;
                }
                else
                {
                    flags &= ~0x00000100U;
                }
            }
        }

        public void Init()
        {
            spectrograph.Init();
            ReflectModify = new float[Constants.VALID_WAVE_NUM];
        }

    }

    /// <summary>
    /// 光谱仪参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct SpectrographParameter
    {
        /// <summary>
        /// 编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string SN;

        /// <summary>
        /// 系数
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.SPECTROGRAPH_COEFFICIENT_COUNT)]
        public float[] Coefficients;

        /// <summary>
        /// 偏移1
        /// </summary>
        public float Offset1;

        /// <summary>
        /// 偏移2
        /// </summary>
        public float Offset2;

        /// <summary>
        /// 保留位
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public float[] Reserved;

        public void Init()
        {
            Coefficients = new float[40];
        }
    }
}
