using System;
using System.Collections.Generic;
using System.Text;

namespace ThreeNH.Color
{
    internal class MathHelper
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
    }
}
