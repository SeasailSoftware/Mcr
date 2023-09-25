using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeNH.Color.Model;

namespace ThreeNH.Color.Algorithm
{
    public static class MathHelper
    {
        /// <summary>
        /// 将弧度转换为角度
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        public static double RadiansToDegrees(double radians)
        {
            return radians / Math.PI * 180.0;
        }

        /// <summary>
        /// // 将角度转换为弧度
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static double DegreesToRadians(double degrees)
        {
            return degrees / 180.0 * Math.PI;
        }

        // 计算反射率的K/S值，具體算法參考《The Kubelka-Monk Theory and K/S》
        public static double K_S(double r)
        {
            // if (r < 0.000001)
            // 	r = 0.000001;
            return (1.0 - r) * (1.0 - r) / (2.0 * r);
        }

        // 根据透射率计算光学吸收率
        // 注意吸收率不能是百分比
        public static double Absorbance(double transmittance)
        {
            // if (transmittance < 0.000001)
            // 	transmittance = 0.000001;
            return -Math.Log10(transmittance);
        }


        public static double f(double a)
        {
            if (a > Math.Pow((24.0 / 116.0), 3))
                return Math.Pow(a, (1.0 / 3.0));
            else
                return (841.0 / 108.0 * a + 16.0 / 116.0);
        }



        public static float F_Inv(float x)
        {
            if (x > 24.0f / 116.0f)
            {
                return (float)Math.Pow(x, 3);
            }
            else
            {
                return (x - 16.0f / 116.0f) / (841.0f / 108.0f);
            }
        }

        public static byte Round_To(float x, int precision)
        {
            float result = (int)x;
            float mult = (float)Math.Pow(10.0, precision);
            float temp = (x - result) * mult;
            float t = temp - (int)temp;

            if (t > 0.5f)
                temp += 0.5f;
            else if (t < -0.5f)
                temp -= 0.5f;
            return (byte)(result + ((int)temp) / mult);
        }

        // 求两个角度的平均值
        public static double AverageDegree(double a, double b)
        {
            if (Math.Abs(a - b) > 180)
            {
                return (a + b < 360) ? (a + b) / 2 + 180 : (a + b) / 2 - 180;
            }
            return (a + b) / 2;
        }

        // 将角度转换为弧度
        public static double RADIANS(double degrees)
        {
            return degrees / 180.0 * Math.PI;
        }


    }
}
