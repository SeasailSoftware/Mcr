using System;
using System.Collections.Generic;
using System.Text;

namespace ThreeNH.Instrument
{
    public static class Constants
    {
        public const int VALID_WAVE_BEGIN = 400;
        public const int VALID_WAVE_END = 700;
        public const int VALID_WAVE_STEP = 10;
        public const int VALID_WAVE_NUM = ((VALID_WAVE_END - VALID_WAVE_BEGIN) / VALID_WAVE_STEP + 1);
        public const int VALID_WAVE_NUM_1NM = (VALID_WAVE_END - VALID_WAVE_BEGIN + 1);
        public const int RECORD_NAME_MAX = 9;  // 名称最大长度

        public const int CUSTOM_COMMAND_START = 0x21;   // 仪器相关命令起始编号
        public const int DEBUG_COMMAND_START = 0xA1;    // 调试命令起始编号

        public const int SPECTROGRAPH_COEFFICIENT_COUNT = 40; // 光谱仪系数个数

        public const int DEVICE_MODEL_NAME_LENGTH = 16;// 仪器型号最大长度

        public const int LOGO_WIDTH_MAX = 320; // LOGO宽
        public const int LOGO_HEIGHT_MAX = 480; // LOGO高

        public const int MAX_CCD_NUM = 40;
        public const int VALID_CCD_BEGIN = 0; // 340.38
        public const int VALID_CCD_END = 39; // 800.65,  1012-418 = 594
        public const int VALID_CCD_NUM = (VALID_CCD_END - VALID_CCD_BEGIN + 1);

        public const int BLE_NAME_LENGTH = 20;  // 蓝牙名称支持的最大长度
    }
}
