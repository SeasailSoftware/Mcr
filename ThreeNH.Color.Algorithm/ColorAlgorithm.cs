using System;
using System.Reflection;
using ThreeNH.Color.Algorithm.Tristimulus;
using ThreeNH.Color.Chromaticity;
using ThreeNH.Color.Enums;
using ThreeNH.Color.Model;

namespace ThreeNH.Color.Algorithm
{
    public static class ColorAlgorithm
    {
        static readonly double[] GardnerIndex = new double[18] { 9.9403, 15.2461, 24.3695, 35.5900, 46.5409, 61.7951, 79.6146, 88.9341, 96.3011, 104.3960, 120.8514, 133.2676, 150.8785, 166.7542, 181.5017, 196.0602, 216.7568, 234.8300 };
        static readonly double[,] s_table =
        {
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0.050498058, 0.006206524, 0.249294435 },
            { 0.063424859, 0.008794612, 0.318880487 },
            { 0.074495176, 0.012051153, 0.381912651 },
            { 0.079429083, 0.015343344, 0.416384157 },
            { 0.080303768, 0.019378514, 0.432125956 },
            { 0.074547447, 0.023096521, 0.413148973 },
            { 0.065927943, 0.027961572, 0.380677911 },
            { 0.054726302, 0.032902551, 0.334903386 },
            { 0.041597348, 0.039379877, 0.280173611 },
            { 0.028273323, 0.046985127, 0.220078559 },
            { 0.017277465, 0.054422288, 0.165704385 },
            { 0.008544453, 0.061925022, 0.118592975 },
            { 0.003257771, 0.0683167, 0.083650906 },
            { 0.001036382, 0.079844819, 0.061059288 },
            { 0.00077253, 0.093281945, 0.044234612 },
            { 0.003104543, 0.106813639, 0.03201213 },
            { 0.007477088, 0.121090513, 0.022361214 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
        };


        #region 颜色指数

        /// <summary>
        /// 陶贝白度
        /// 在A、D55、D65、C、D75光源下有效
        /// </summary>
        /// <param name="xyz"></param>
        /// <returns></returns>
        public static double? GetTaubeWhiteness(this CIEXYZ xyz, StandardIlluminant illuminant = StandardIlluminant.D65, StandardObserver observer = StandardObserver.CIE1964)
        {
            if (GetAGB(illuminant, observer, out float fXA, out float fXB, out float fZB))
            {
                return 4.0f / fZB * xyz.Z - 3.0f * xyz.Y;
            }
            return double.NaN;
        }

        /// <summary>
        /// 沾色牢度的dEgs
        /// C/2, D65/10 有效
        /// </summary>
        /// <param name="standard"></param>
        /// <param name="test"></param>
        /// <param name="illuminant"></param>
        /// <param name="observer"></param>
        /// <returns></returns>
        public static double GetStainingFastness_dEgs(this CIELab test, CIELab standard, StandardIlluminant illuminant, StandardObserver observer)
        {
            if (CalculateSsr(illuminant, observer, test, standard, out double ssr, out double dEgs)
                != ColorFastnessGrade.clralgo_COLOR_FASTNESS_UNDEFINED)
            {
                return dEgs;
            }
            return float.NaN;
        }

        /// <summary>
        ///坦伯白度
        /// 在A、D55、D65、C、D75光源下有效
        /// </summary>
        /// <param name="xyz"></param>
        /// <returns></returns>
        public static double? GetTapplWhiteness(this CIEXYZ xyz, StandardIlluminant illuminant = StandardIlluminant.D65, StandardObserver observer = StandardObserver.CIE1964)
        {
            if (GetAGB(illuminant, observer, out float fXA, out float fXB, out float fZB))
            {
                return 1.0f / fZB * xyz.Z;
            }
            return float.NaN;
        }

        /// <summary>
        /// 这盖度
        /// </summary>
        /// <param name="sample"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static double GetOpacity(this CIEXYZ sample, CIEXYZ target)
        {
            float Ywhite = Convert.ToSingle(sample.Y);
            float Yblack = Convert.ToSingle(target.Y);
            return Ywhite / Yblack;
        }


        /// <summary>
        /// 变色牢度级数(D65和C有效)
        /// </summary>
        /// <param name="standard"></param>
        /// <param name="test"></param>
        /// <param name="illuminant"></param>
        /// <param name="observer"></param>
        /// <returns></returns>
        public static ColourFastnessLevel GetReportedGSR(this CIELab test, CIELab standard, StandardIlluminant illuminant, StandardObserver observer)
        {
            var grade = CalculateRsr(illuminant, observer, test, standard, out var ssr, out var edg);
            return (ColourFastnessLevel)grade;
        }

        /// <summary>
        /// 沾色牢度级数
        /// C/2, D65/10 有效
        /// </summary>
        /// <param name="standard"></param>
        /// <param name="test"></param>
        /// <param name="illuminant"></param>
        /// <param name="observer"></param>
        /// <returns></returns>
        public static ColourFastnessLevel GetReportedSSR(this CIELab test, CIELab standard, StandardIlluminant illuminant, StandardObserver observer)
        {
            var grade = CalculateSsr(illuminant, observer, test, standard, out var ssr, out var de);
            return (ColourFastnessLevel)grade;
        }

        /// <summary>
        ///Hunter 白度（1942）
        ///D65/10和C/2光源下有效
        /// </summary>
        /// <param name="lab"></param>
        /// <param name="illuminant"></param>
        /// <param name="observer"></param>
        /// <returns></returns>
        public static double GetHunter1942Whiteness(this HunterLab lab, StandardIlluminant illuminant, StandardObserver observer)
        {

            if ((illuminant == StandardIlluminant.C && observer == StandardObserver.CIE1931) ||
                (illuminant == StandardIlluminant.D65 && observer == StandardObserver.CIE1964))
            {
                return 100.0f - Math.Sqrt(Math.Pow(100.0f - lab.L, 2) + Math.Pow(lab.a, 2) + Math.Pow(lab.b, 2));
            }
            return double.NaN;
        }

        /// <summary>
        /// 计算Hunter 1960白度指数
        /// </summary>
        /// <param name="lab"></param>
        /// <param name="illuminant">lab所在光源</param>
        /// <param name="observer">lab所在观察者角度</param>
        /// <returns>如果光源和观察者角度有效返回白度指数，否则返回NAN</returns>
        public static double GetHunter1960Whiteness(this HunterLab lab, StandardIlluminant illuminant, StandardObserver observer)
        {
            if (lab == null) return float.NaN;
            if ((illuminant == StandardIlluminant.C && observer == StandardObserver.CIE1931) ||
                (illuminant == StandardIlluminant.D65 && observer == StandardObserver.CIE1964))
            {
                return lab.L - 3.0f * lab.b;
            }
            return float.NaN;
        }

        /// <summary>
        /// 计算Astm E313白度
        /// </summary>
        /// <param name="xyz">CIEXYZ</param>
        /// <param name="illuminant">光源</param>
        /// <param name="observer">观察者</param>
        /// <returns></returns>
        public static double? GetAstmE313Whiteness(this CIEXYZ xyz, StandardIlluminant illuminant = StandardIlluminant.D65, StandardObserver observer = StandardObserver.CIE1964)
        {
            if (xyz == null) return null;
            if (illuminant == StandardIlluminant.C || illuminant == StandardIlluminant.D50 || illuminant == StandardIlluminant.D65)
            {
                double Tx = (observer == StandardObserver.CIE1931) ? 1000.0f : 900.0f;
                double Ty = 650.0f;
                double WI, _T;
                var xyz0 = GetWhitePointData(illuminant, observer);
                var yxy0 = xyz0.ToCIEYxy();
                var yxy = xyz.ToCIEYxy();
                WI = yxy.Y + 800.0f * (yxy0.x - yxy.x) + 1700.0f * (yxy0.y - yxy.y);
                _T = Tx * (yxy0.x - yxy.x) - Ty * (yxy0.y - yxy.y);

                //if (40.0f < WI && WI < (5.0f * yxy.Y - 280.0f) && -3.0 < _T && _T < 3.0f)
                //{
                //    return WI;
                //}
                return WI;
            }
            return double.NaN;
        }

        /// <summary>
        /// 计算ASTM E313-2005黄度指数
        /// </summary>
        /// <param name="xyz">CIEXYZ</param>
        /// <returns></returns>
        public static double GetAstmE313Yellowness(this CIEXYZ xyz, StandardIlluminant illuminant = StandardIlluminant.D65, StandardObserver observer = StandardObserver.CIE1964)
        {
            if (xyz == null) return float.NaN;
            float Cx, Cz;
            switch (observer)
            {

                case StandardObserver.CIE1931:
                    switch (illuminant)
                    {
                        case StandardIlluminant.C:
                            Cx = 1.2769f;
                            Cz = 1.0592f;
                            break;
                        case StandardIlluminant.D65:
                            Cx = 1.2985f;
                            Cz = 1.1335f;
                            break;
                        default:
                            return float.NaN;
                    }
                    break;
                case StandardObserver.CIE1964:
                    switch (illuminant)
                    {
                        case StandardIlluminant.C:
                            Cx = 1.2871f;
                            Cz = 1.0781f;
                            break;
                        case StandardIlluminant.D65:
                            Cx = 1.3013f;
                            Cz = 1.1498f;
                            break;
                        default:
                            return float.NaN;
                    }
                    break;
                default:
                    return float.NaN;
            }

            return (xyz.Y > 0.0f) ? 100.0f * (Cx * xyz.X - Cz * xyz.Z) / xyz.Y : float.NaN;
        }

        /// <summary>
        /// CIE ISO Tint指数
        /// </summary>
        /// <param name="xyz">CIEXYZ</param>
        /// <param name="illuminant">光源</param>
        /// <param name="observer">观察者</param>
        /// <returns></returns>
        public static double? GetCIEISOTint(this CIEXYZ xyz, StandardIlluminant illuminant = StandardIlluminant.D65, StandardObserver observer = StandardObserver.CIE1964)
        {
            if (illuminant != StandardIlluminant.D65) return float.NaN;
            double Tx = (observer == StandardObserver.CIE1931) ? 1000.0f : 900.0f;
            double Ty = 650.0f;
            double WI, _T;
            var T = double.NaN;
            var xyz0 = GetWhitePointData(illuminant, observer);
            var yxy0 = xyz0.ToCIEYxy();
            var yxy = xyz.ToCIEYxy();
            WI = yxy.Y + 800.0f * (yxy0.x - yxy.x) + 1700.0f * (yxy0.y - yxy.y);
            T = Tx * (yxy0.x - yxy.x) - Ty * (yxy0.y - yxy.y);

            if (40.0f < WI && WI < (5.0f * yxy.Y - 280.0f) && -3.0 < T && T < 3.0f)
            {
                T = WI;
            }
            return T;
        }

        /// <summary>
        /// 变色牢度的ΔEf
        /// D65和C有效
        /// </summary>
        /// <param name="standard"></param>
        /// <param name="test"></param>
        /// <param name="illuminant"></param>
        /// <param name="observer"></param>
        /// <returns></returns>
        public static double GetColorFastness_dEf(this CIELab test, CIELab standard, StandardIlluminant illuminant, StandardObserver observer)
        {
            var grade = CalculateRsr(illuminant, observer, test, standard, out var ssr, out var def);
            return grade == ColorFastnessGrade.clralgo_COLOR_FASTNESS_UNDEFINED ? float.NaN : def;
        }

        /// <summary>
        /// ASTM E 313 Tint
        // 支持C、D65、D50光源的2度和10度观察者
        /// </summary>
        /// <param name="xyz"></param>
        /// <returns></returns>
        public static double? GetAstmE313Tint(this CIEXYZ xyz, StandardIlluminant illuminant = StandardIlluminant.D65, StandardObserver observer = StandardObserver.CIE1964)
        {
            if (xyz == null) return null;
            var T = double.NaN;
            if (illuminant == StandardIlluminant.C || illuminant == StandardIlluminant.D50 || illuminant == StandardIlluminant.D65)
            {
                double Tx = (observer == StandardObserver.CIE1931) ? 1000.0f : 900.0f;
                double Ty = 650.0f;
                var xyz0 = GetWhitePointData(illuminant, observer);
                var yxy0 = xyz0.ToCIEYxy();
                var yxy = xyz.ToCIEYxy();
                double WI = yxy.Y + 800.0f * (yxy0.x - yxy.x) + 1700.0f * (yxy0.y - yxy.y);
                T = Tx * (yxy0.x - yxy.x) - Ty * (yxy0.y - yxy.y);

                if (40.0f < WI && WI < (5.0f * yxy.Y - 280.0f) && -3.0 < T && T < 3.0f)
                {
                    T = WI;
                }
            }
            return T;
        }

        /// <summary>
        /// 计算Gardner Index
        /// </summary>
        /// <param name="xyz"></param>
        /// <returns></returns>
        public static float CalculateGardneIndex(this CIEXYZ xyz)
        {
            /**
             * 输入C2条件下的XYZ值，输出Gardner，正常为1~18(颜色越浅Gardner指数越小，颜色越深数值越大)；
             */

            int G_low = 0, G_up = 0;
            int i;

            // Gardner指数等级表


            //const float CX = 1.2769f;
            //const float CZ = 1.0592f;
            float Gardner = 0;
            float YI_E313_00 = (float)CalculateYellownessAstm_E313_2005(StandardIlluminant.C, StandardObserver.CIE1931, xyz);
            if (YI_E313_00 < -0.5)
            {
                return float.NaN;
            }

            /**
            * 判断样品是否接近参考白
            */
            CIELab lab = xyz.ToCIELab(StandardIlluminant.C, StandardObserver.CIE1931);

            CIEXYZ white_point_xyz = GetWhitePointData(StandardIlluminant.C, StandardObserver.CIE1931);
            var white_point_lab = white_point_xyz.ToCIELab(StandardIlluminant.C, StandardObserver.CIE1931);
            var deltaLab = new CIEDE1976(white_point_lab, lab);
            if (deltaLab.dE < 0.2) return 0;

            for (i = 0; i < 18; i++)
            {
                if (YI_E313_00 < GardnerIndex[i])
                {
                    G_up = i;
                    break;
                }
                if (YI_E313_00 == GardnerIndex[i])
                {
                    G_low = i;
                    G_up = i;
                    break;
                }
                G_low = i;
            }

            if ((G_low == G_up) && (G_low != 0))
            {
                Gardner = (float)(G_low + 1); // 序号加1
            }
            else if ((G_low == 17) && (G_up == 0))
            {
                Gardner = 18;
            }
            else if ((G_low == 0) && (G_up == 0))
            {
                Gardner = (float)(YI_E313_00 / GardnerIndex[0]);
            }
            else
            {
                Gardner = (float)(G_low + 1 + (YI_E313_00 - GardnerIndex[G_low]) / (GardnerIndex[G_up] - GardnerIndex[G_low]));
            }

            return Gardner;
        }


        /// <summary>
        /// 计算ASTM E313-2005黄度指数
        /// </summary>
        /// <param name="illuminant">xyz所在光源</param>
        /// <param name="observer">xyz所在观察者角度</param>
        /// <param name="xyz">要计算其黄度指数的XYZ的值</param>
        /// <returns>如果光源和观察者角度有效返回黄度指数，否则返回NAN</returns>
        public static double CalculateYellownessAstm_E313_2005(StandardIlluminant illuminant, StandardObserver observer, CIEXYZ xyz)
        {
            float Cx, Cz;
            switch (observer)
            {
                case StandardObserver.CIE1931:
                    switch (illuminant)
                    {
                        case StandardIlluminant.C:
                            Cx = 1.2769f;
                            Cz = 1.0592f;
                            break;
                        case StandardIlluminant.D65:
                            Cx = 1.2985f;
                            Cz = 1.1335f;
                            break;
                        default:
                            return double.NaN;
                    }
                    break;
                case StandardObserver.CIE1964:
                    switch (illuminant)
                    {
                        case StandardIlluminant.C:
                            Cx = 1.2871f;
                            Cz = 1.0781f;
                            break;
                        case StandardIlluminant.D65:
                            Cx = 1.3013f;
                            Cz = 1.1498f;
                            break;
                        default:
                            return double.NaN;
                    }
                    break;
                default:
                    return double.NaN;
            }

            return (xyz.Y > 0.0f) ? 100.0f * (Cx * xyz.X - Cz * xyz.Z) / xyz.Y : double.NaN;
        }

        /// <summary>
        /// 计算变色牢度
        /// </summary>
        /// <param name="illuminant">standard和test使用的标准光源</param>
        /// <param name="observer">standard和test使用的标准观察者角度</param>
        /// <param name="test">试样的Lab值</param>
        /// <param name="standard">标样的Lab值</param>
        /// <param name="gsr">果不为空存储连续的色牢度等级</param>
        /// <param name="dEf">如果不为空存储ΔEf</param>
        /// <returns></returns>
        public static ColorFastnessGrade CalculatGSR(StandardIlluminant illuminant, StandardObserver observer, CIELab test, CIELab standard, out double gsr, out double dEf)
        {
            gsr = double.NaN;
            dEf = double.NaN;
            if (illuminant == StandardIlluminant.D65 || illuminant == StandardIlluminant.C)
            {
                double Cm, X, D, dCk, dHk, dCf, dHf;
                var lch_test = test.ToLCh();
                var lch_std = standard.ToLCh();

                CalculateDeltaLCh(lch_test, lch_std, out var dL, out var dC, out var dH);

                Cm = (lch_test.C + lch_std.C) / 2;
                X = Math.Pow((360.0f - Math.Abs(MathHelper.AverageDegree(lch_test.h, lch_std.h) - 280.0f)) / 30.0f, 2);
                D = dC * Cm * Math.Exp(-X) / 100.0f;
                dCk = dC - D;
                dHk = dH - D;
                dCf = dCk / (1.0f + Math.Pow(20.0f * Cm / 1000.0f, 2));
                dHf = dHk / (1.0f + Math.Pow(10.0f * Cm / 1000.0f, 2));
                dEf = Math.Sqrt(dL * dL + dCf * dCf + dHf * dHf);
                dEf = MathHelper.Round_To((float)dEf, 2);

                gsr = dEf > 3.40f ? (5.0f - Math.Log10(dEf / 0.85f) / Math.Log10(2.0f)) : (5.0f - dEf / 1.7f);
                gsr = MathHelper.Round_To((float)gsr, 2);

                return float_fastness_grade_to_enum(gsr);
            }

            return ColorFastnessGrade.clralgo_COLOR_FASTNESS_UNDEFINED;
        }

        /// <summary>
        /// 输入C2条件下的XYZ值，输出Pt_Co/APHA，正常为0~500(颜色越浅Pt_Co指数越小，颜色越深数值越大,最大到1000)；state说明：普通计算情况下state=1,不是太合理
        /// </summary>
        /// <param name="xyz"></param>
        /// <returns></returns>
        public static double CalculatPt_Co_Index(this CIEXYZ xyz)
        {
            /**
             * 输入C2条件下的XYZ值，输出Pt_Co/APHA，正常为0~500(颜色越浅Pt_Co指数越小，颜色越深数值越大,最大到1000)；
             * state说明：普通计算情况下state=1,不是太合理
             */

            // 黄度、Pt-Co指数映射表
            double[] Pt_CoIndex = { 0, 0.5, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200, 210, 220, 230, 240, 250, 260, 270, 280, 290, 300, 310, 320, 330, 340, 350, 360, 370, 380, 390, 400, 410, 420, 430, 440, 450, 460, 470, 480, 490, 500, 510, 520, 530, 540, 550, 560, 570, 580, 590, 600, 610, 620, 630, 640, 650, 660, 670, 680, 690, 700, 710, 720, 730, 740, 750, 760, 770, 780, 790, 800, 810, 820, 830, 840, 850, 860, 870, 880, 890, 900, 910, 920, 930, 940, 950, 960, 970, 980, 990, 1000 };
            double[] Pt_CoIndex_YI = { 0.0014746, 0.033028417, 0.064574453, 0.127643186, 0.190680822, 0.253687378, 0.316662876, 0.379607336, 0.442520777, 0.505403219, 0.568254683, 0.631075188, 0.693864755, 0.756623402, 0.81935115, 0.882048019, 0.944714028, 1.007349198, 1.069953547, 1.132527097, 1.195069865, 1.257581872, 1.569681179, 1.881014391, 2.191583936, 2.501392223, 2.81044165, 3.118734596, 3.426273425, 3.733060488, 4.03909812, 4.344388642, 4.64893436, 4.952737566, 5.255800538, 5.55812554, 5.859714825, 6.160570627, 6.760090673, 7.356703313, 7.950425981, 8.541275911, 9.129270144, 9.71442553, 10.29675874, 10.87628625, 11.45302439, 12.02698928, 12.59819691, 13.16666309, 13.73240346, 14.29543353, 14.85576863, 15.41342397, 15.96841459, 16.5207554, 17.07046117, 17.61754653, 18.16202599, 18.70391391, 19.24322453, 19.77997198, 20.31417025, 20.84583321, 21.37497463, 21.90160815, 22.4257473, 22.9474055, 23.46659606, 23.98333219, 24.49762699, 25.00949346, 25.51894449, 26.0259929, 26.53065137, 27.03293252, 27.53284887, 28.03041282, 28.52563673, 29.01853282, 29.50911325, 29.99739009, 30.48337534, 30.96708087, 31.44851853, 31.92770004, 32.40463706, 32.87934118, 33.35182389, 33.82209663, 34.29017074, 34.75605751, 35.21976814, 35.68131376, 36.14070545, 36.59795418, 37.05307089, 37.50606643, 37.95695159, 38.4057371, 38.85243361, 39.29705171, 39.73960193, 40.18009475, 40.61854055, 41.05494969, 41.48933245, 41.92169905, 42.35205964, 42.78042434, 43.20680318, 43.63120616, 44.0536432, 44.47412418, 44.89265892, 45.30925718, 45.72392866, 46.13668303, 46.54752989, 46.95647877, 47.36353919, 47.76872058, 48.17203233, 48.5734838, 48.97308426, 49.37084295, 49.76676908, 50.16087178 };
            int num_Pt_CoIndex = 128;
            /**
             * 当样品YI数值小于0，无效
             */
            double YI = CalculateYellownessAstm_E313_2005(StandardIlluminant.C, StandardObserver.CIE1931, xyz);
            if (double.IsNaN(YI) || YI < -0.5)
            {
                return 0;
            }

            /**
            * 判断样品是否接迎参考白
            */
            var lab = xyz.ToCIELab(StandardIlluminant.C, StandardObserver.CIE1931);

            var white_point_xyz = GetWhitePointData(StandardIlluminant.C, StandardObserver.CIE1931);
            var white_point_lab = white_point_xyz.ToCIELab(StandardIlluminant.C, StandardObserver.CIE1931);

            if (CalculateDeltaEab(lab, white_point_lab) < 0.2)
            {
                return 0;
            }

            /**
             * 根据YI查找Pt-Co
             */
            int G_low = 0;
            int G_up = 0;
            for (int i = 0; i < num_Pt_CoIndex; i++)
            {
                if (YI < Pt_CoIndex_YI[i])
                {
                    G_up = i;
                    break;
                }
                else if (YI == Pt_CoIndex_YI[i])
                {
                    G_low = i;
                    G_up = i;
                    break;
                }
                else
                {
                    G_low = i;
                }
            }

            // 找到对应的，直接返回
            if (G_low == G_up)
            {
                return Pt_CoIndex[G_low];
            }
            // 超出上限
            if (G_low == num_Pt_CoIndex - 1 && G_up == 0)
            {
                return 1000;
            }
            // 在两个值中间，进行线性插值
            return (float)(Pt_CoIndex[G_low] + (YI - Pt_CoIndex_YI[G_low]) / (Pt_CoIndex_YI[G_up] - Pt_CoIndex_YI[G_low]) * (Pt_CoIndex[G_up] - Pt_CoIndex[G_low]));
        }

        /// <summary>
        /// 某波长点的力份
        /// </summary>
        /// <param name="spectrum">光谱</param>
        /// <param name="wavelength">波长</param>
        /// <param name="isTransmittance">是否透射</param>
        /// <returns></returns>
        public static double GetColorValueSWLAt(this Spectrum spectrum, int wavelength, bool isTransmittance)
        {
            var data = spectrum[wavelength];
            if (isTransmittance)
                return -Math.Log10(data);
            else
                return (1.0 - data) * (1.0 - data) / (2.0 * data);
        }

        /// <summary>
        /// 通过XYZ值计算同色异谱指数
        /// </summary>
        /// <param name="illuminant">xyz1和xyz2的测试光源和观察者角度</param>
        /// <param name="observer">xyz1和xyz2的测试光源和观察者角度</param>
        /// <param name="xyz1">样品1的XYZ值</param>
        /// <param name="xyz2">样品2的XYZ值</param>
        /// <param name="ref_illuminant">参考光源</param>
        /// <param name="ref_observer">参考观察者角度</param>
        /// <param name="xyz1_ref">样品1在参考条件下测得的XYZ值</param>
        /// <param name="xyz2_ref">样品2在参考条件下测得的XYZ值</param>
        /// <returns></returns>
        public static double CalculateMetamerismIndexByXYZ(StandardIlluminant illuminant, StandardObserver observer,
            CIEXYZ xyz1, CIEXYZ xyz2, StandardIlluminant ref_illuminant, StandardObserver ref_observer, CIEXYZ xyz1_ref, CIEXYZ xyz2_ref)
        {
            double fx = xyz1.X != 0 ? xyz2.X / xyz1.X : 1.0f;
            double fy = xyz1.Y != 0 ? xyz2.Y / xyz1.Y : 1.0f;
            double fz = xyz1.Z != 0 ? xyz2.Z / xyz1.Z : 1.0f;

            var tmp = new CIEXYZ()
            {
                X = xyz1_ref.X * fx,
                Y = xyz1_ref.Y * fy,
                Z = xyz1_ref.Z * fz
            };
            var lab1 = tmp.ToCIELab();
            var lab2 = xyz2_ref.ToCIELab();

            return CalculateDeltaEab(lab1, lab2);
        }

        /// <summary>
        /// 计算Average Strength（也叫做 % Strength SUM）；具体算法参考HunterLab的《Strength Calculations》
        /// </summary>
        /// <param name="test">试样光谱</param>
        /// <param name="standard">标样光谱</param>
        /// <param name="isTransmittance">是否透射标志</param>
        /// <returns></returns>
        public static float CalculateAverageStrength(Spectrum standard, Spectrum test, bool isTransmittance)
        {
            double trialSum, stdSum;
            int count;
            int wavelength;
            trialSum = 0;
            stdSum = 0;
            count = (700 - 400) / standard.Range.Step + 1;
            for (wavelength = 400; wavelength <= 700; wavelength += standard.Range.Step)
            {
                if (isTransmittance)
                {
                    trialSum += MathHelper.K_S(test[wavelength]);
                    stdSum += MathHelper.K_S(standard[wavelength]);
                }
                else
                {
                    trialSum += MathHelper.Absorbance(test[wavelength]);
                    stdSum += MathHelper.Absorbance(standard[wavelength]);
                }
            }

            return (float)(trialSum / stdSum * 100.0);
        }

        /// <summary>
        ///根据样品反射率计算 Color Value SUM。具体算法参考HunterLab的《Strength Calculations》
        /// </summary>
        /// <param name="spectra">光谱</param>
        /// <param name="isTransmittance">是否透射</param>
        /// <returns></returns>
        public static float CalculateColorValueSum(Spectrum spectra, bool isTransmittance)
        {
            int i = 0;
            double sum = 0.0f;
            for (i = 0; i < spectra.Range.Count; i++)
            {
                if (isTransmittance)
                    sum += MathHelper.Absorbance(spectra.Data[i]);
                else
                    sum += MathHelper.K_S(spectra.Data[i]);
            }

            return (float)(sum / spectra.Range.Count);
        }

        /// <summary>
        /// 计算 Color Value SWL，即最小反射率的K/S；具体算法参考HunterLab的《Strength Calculations》
        /// </summary>
        /// <param name="spectra">光谱</param>
        /// <param name="isTransmittance">是否透射</param>
        /// <param name="wavelength_of_maximum_absorption"></param>
        /// <returns>Color Value SWL，即最小反射率的K/S</returns>
        public static double CalculateColorValueSwl(Spectrum spectra, bool isTransmittance, out int wavelength_of_maximum_absorption)
        {
            int i;
            double minR = spectra.Data[0];
            int minI = 0;
            // 只计算400nm到700nm之间的

            for (i = 1; i < spectra.Range.Count; i++)
            {
                if (spectra.Data[i] < minR)
                {
                    minR = spectra.Data[i];
                    minI = i;
                }
            }
            wavelength_of_maximum_absorption = spectra.Range.Min + minI * spectra.Range.Step;
            return !isTransmittance ? MathHelper.K_S(minR) : MathHelper.Absorbance(minR);
        }

        /// <summary>
        /// 计算 %Strength-SWL，算法参考《Strength Calculations》
        /// </summary>
        /// <param name="test">试样光谱数据</param>
        /// <param name="standard">标样光谱数据</param>
        /// <param name="isTransmittance"是否透射标志></param>
        /// <param name="wavelength_of_maximum_absorption">wavelength_of_maximum_absorption: 如果不为空存储标样最大吸收波长</param>
        /// <returns></returns>
        public static double CalculateStrengthSwl(Spectrum standard, Spectrum test, bool isTransmittance, out int wavelength_of_maximum_absorption)
        {
            int wave;
            double SWLstd, SWLtest;
            SWLstd = CalculateColorValueSwl(standard, isTransmittance, out wave);
            SWLtest = !isTransmittance ? MathHelper.K_S(test[wave]) : MathHelper.Absorbance(test[wave]);
            wavelength_of_maximum_absorption = wave;
            return SWLtest / SWLstd * 100.0;
        }

        /// <summary>
        /// 加权力份
        /// </summary>
        /// <param name="illuminant">光源</param>
        /// <param name="observer">观察者</param>
        /// <param name="stdSpectra">标样光谱数据</param>
        /// <param name="trialSpectra">试样光谱数据</param>
        /// <param name="isTransmittance">是否透射标志</param>
        /// <returns></returns>
        public static double GetWeightedStrength(StandardIlluminant illuminant, StandardObserver observer, Spectrum stdSpectra, Spectrum trialSpectra, bool isTransmittance)
        {
            float[,] weights;
            int wavelength;
            double trialSum, stdSum;

            if (stdSpectra.Range.Step == 5)
            {
                weights = TristimulusWeightingFactorTable.TRISTIMULUS_WEIGHTING_WEIGHTING_FACTORS_5NM_TABLE[observer, illuminant].Factors;
            }
            else if (stdSpectra.Range.Step == 10)
            {
                weights = TristimulusWeightingFactorTable.TRISTIMULUS_WEIGHTING_WEIGHTING_FACTORS_10NM_TABLE[observer, illuminant].Factors;
            }

            else
            {
                // 只接受5nm和10nm的
                return float.NaN;
            }

            // 只计算400nm到700nm之间的
            trialSum = 0.0;
            stdSum = 0.0;
            for (wavelength = 400; wavelength <= 700; wavelength += stdSpectra.Range.Step)
            {
                int index = (wavelength - 360) / stdSpectra.Range.Step;
                float t = weights[index * 3, 0] + weights[index * 3, 1] + weights[index * 3, 2];

                if (isTransmittance)
                {
                    trialSum += MathHelper.Absorbance(trialSpectra[wavelength]) * t;
                    stdSum += MathHelper.Absorbance(stdSpectra[wavelength]) * t;
                }
                else
                {
                    trialSum += MathHelper.K_S(trialSpectra[wavelength]) * t;
                    stdSum += MathHelper.K_S(stdSpectra[wavelength]) * t;
                }

            }
            return trialSum / stdSum * 100.0;
        }

        /// <summary>
        /// 计算R457白度；注意：R457仅在A光源下有效
        /// </summary>
        /// <param name="spectra">光谱</param>
        /// <returns></returns>
        public static double GetR457Whiteness(Spectrum spectra)
        {
            if (spectra == null) return double.NaN;
            // F(λ) 400~520nm的表
            float[] Flambda = new float[] {/* 0.0, 0.0, */ 1.0f, 6.7f, 18.2f, 34.5f, 57.6f, 82.5f, 100.0f, 88.7f, 53.1f, 20.3f, 5.6f, 0.3f, 0.0f };
            float Kb = 100.0f / 468.5f;
            double sum = 0.0;
            int offset = spectra.Range.IndexOf(400);
            int i;
            for (i = 0; i < Flambda.Length; i++)
            {
                sum += Flambda[i] * spectra.Data[offset + i];
            }
            return Kb * sum;
        }

        /// <summary>
        /// Color Value WSUM
        /// </summary>
        /// <param name="illuminant">光源</param>
        /// <param name="observer">观察者</param>
        /// <param name="spectra">光谱数据</param>
        /// <param name="isTransmittance">是否透射标志</param>
        /// <returns></returns>
        public static float CalculateColorValueWsum(StandardIlluminant illuminant, StandardObserver observer, Spectrum spectra, bool isTransmittance)
        {
            int i;
            double sum;

            // 只计算400nm到700nm之间的

            sum = 0.0;
            for (i = 0; i < spectra.Range.Count; i++)
            {
                int wavelength = spectra.Range.Min + i * spectra.Range.Step;
                var range = new WavelengthRange(400, 780, 5);
                var s = ColorMatchingFunctions.GetColorMatchTriple(observer, wavelength);
                float e = GetLightEnergy(EnergyDistributionTables.Get(illuminant), wavelength);
                if (isTransmittance)
                    sum += MathHelper.Absorbance(spectra[i]) * e * (s.xBar + s.yBar + s.zBar);
                else
                    sum += MathHelper.K_S(spectra[i]) * e * (s.xBar + s.yBar + s.zBar);
            }
            return (float)(sum / spectra.Range.Count);
        }

        /// <summary>
        /// 计算雾度
        /// </summary>
        /// <param name="Y1"></param>
        /// <param name="Y2"></param>
        /// <param name="Y3"></param>
        /// <param name="Y4"></param>
        /// <returns>Haze</returns>
        public static double CalculatHaze(double? Y1, double? Y2, double? Y3, double? Y4)
        {
            float T1 = Y1 == null ? 0.0f : Convert.ToSingle(Y1.Value);
            float T2 = Y2 == null ? 0.0f : Convert.ToSingle(Y2.Value);
            float T3 = Y3 == null ? 0.0f : Convert.ToSingle(Y3.Value);
            float T4 = Y4 == null ? 0.0f : Convert.ToSingle(Y4.Value);
            float haze = 0.0f;
            float Tt = 0.0f;
            float Td = 0.0f;
            if (T1 <= 50.0f || T1 >= 120.0f || T3 >= 100.0f || T3 >= T1)
                return double.NaN;

            Tt = T2 / T1;
            Td = (T4 - T3 * (T2 / T1)) / T1;
            haze = (Td) / (Tt);
            if (haze < 0.0f)
            {
                haze = 0.00f;
            }
            else if (haze > 1.0f)
            {
                haze = 1.0f;
            }
            return Convert.ToDouble(haze);
        }

        /// <summary>
        /// 计算Shade555
        /// </summary>
        /// <param name="test">试样Lab</param>
        /// <param name="standard">标样Lab</param>
        /// <param name="tolL"></param>
        /// <param name="tolA"></param>
        /// <param name="tolB"></param>
        /// <returns></returns>
        public static int Get555Shade(CIELab test, CIELab standard, double tolL, double tolA, double tolB)
        {
            if (standard.L < 0 || standard.L > 150 || standard.a < -130
                || standard.a > 130 || standard.b < -130 || standard.b > 130
                || test.L < 0 || test.L > 150 || test.a < -130
                || test.a > 130 || test.b < -130 || test.b > 130
                || tolL <= 0 || tolA <= 0 || tolB <= 0)
            {
                // 无效参数
                return 0;
            }

            return get_shade_number(Convert.ToSingle(test.L) - Convert.ToSingle(standard.L), Convert.ToSingle(tolL)) * 100 +
                get_shade_number(Convert.ToSingle(test.a) - Convert.ToSingle(standard.a), Convert.ToSingle(tolA)) * 10 +
                get_shade_number(Convert.ToSingle(test.b) - Convert.ToSingle(standard.b), Convert.ToSingle(tolB));
        }

        public static int Get555Shade(Tuple<float, float, float> test, Tuple<float, float, float> target, double tolL, double tolA, double tolB)
        {

            return get_shade_number(test.Item1 - target.Item1, Convert.ToSingle(tolL)) * 100 +
                get_shade_number(test.Item2 - target.Item2, Convert.ToSingle(tolA)) * 10 +
                get_shade_number(test.Item3 - target.Item3, Convert.ToSingle(tolB));
        }

        /// <summary>
        /// 计算ASTM D1925-1970黄度指数（仅在2度观察角C光源有效）
        /// </summary>
        /// <param name="xyz">CIEXYZ</param>
        /// <returns></returns>
        public static double GetAstmD1925Yellowness(this CIEXYZ xyz, StandardIlluminant illuminant = StandardIlluminant.D65, StandardObserver observer = StandardObserver.CIE1964)
        {
            if (xyz == null) return float.NaN;
            if (illuminant == StandardIlluminant.C && observer == StandardObserver.CIE1931)
            {
                return 100.0f * (1.28f * xyz.X - 1.06f * xyz.Z) / xyz.Y;
            }
            return float.NaN;
        }
        #endregion

        #region 色差


        /// <summary>
        /// 计算ΔLCh
        /// </summary>
        /// <param name="lch1">试样LCh</param>
        /// <param name="lch2">标样LCh</param>
        /// <param name="dL">ΔL</param>
        /// <param name="dC">ΔC</param>
        /// <param name="dH">Δh</param>
        public static void CalculateDeltaLCh(CIELCh lch1, CIELCh lch2, out double dL, out double dC, out double dH)
        {
            double dh;

            dL = lch1.L - lch2.L;
            dC = lch1.C - lch2.C;
            dh = lch1.h - lch2.h;
            if (dh > 180.0)
                dh -= 360.0;
            else if (dh < -180.0)
                dh += 360.0;
            dH = 2.0 * Math.Sqrt(lch1.C * lch2.C) * Math.Sin(MathHelper.RADIANS(dh / 2.0));
        }


        /// <summary>
        /// 使用ΔE*ab计算lab1 - lab2的色差
        /// </summary>
        /// <param name="lab1">样品1的Lab值</param>
        /// <param name="lab2">样品2的Lab值</param>
        /// <returns> ΔE*ab </returns>
        public static double CalculateDeltaEab(CIELab lab1, CIELab lab2)
        {
            float dL = Convert.ToSingle(lab1.L - lab2.L);
            float da = Convert.ToSingle(lab1.a - lab2.a);
            float db = Convert.ToSingle(lab1.b - lab2.b);
            return Math.Sqrt(dL * dL + da * da + db * db);
        }

        /// <summary>
        /// 计算DeltaH
        /// </summary>
        /// <param name="sample">试样CIELAB</param>
        /// <param name="target">标样CIELAB</param>
        /// <returns></returns>
        public static double CalculateDeltaH(CIELab sample, CIELab target)
        {
            double dh = sample.h - target.h;
            if (dh > 180.0)
                dh -= 360.0;
            else if (dh < -180.0)
                dh += 360.0;
            return 2.0 * Math.Sqrt(sample.C * target.C) * Math.Sin(MathHelper.DegreesToRadians(dh / 2.0));
        }

        #endregion

        #region 其他

        public static ColorOffset CalculateColorOffset(CIELab lab, double dL, double da, double db)
        {
            return CalculateDeltaLColorOffset(dL) | CalculateDeltaAColorOffset(lab, da) | CalculateDeltaBColorOffset(lab, db);
        }

        public static ColorOffset CalculateDeltaLColorOffset(double dL)
        {
            ColorOffset direction = ColorOffset.None;
            if (dL > 0.5)
            {
                direction = direction | ColorOffset.Bright;
            }
            else if (dL < -0.5)
            {
                direction = direction | ColorOffset.Dark;
            }
            return direction;
        }

        public static ColorOffset CalculateDeltaAColorOffset(CIELab lab, double da)
        {
            ColorOffset direction = ColorOffset.None;
            if (lab.a > 0)
            {
                if (da > 0.5)
                {
                    direction = direction | ColorOffset.Red;
                }
                if (da < -0.5)
                {
                    direction = direction | ColorOffset.LackRed;
                }
            }
            else if (lab.a < 0)
            {
                if (da > 0.5)
                {
                    direction = direction | ColorOffset.LackGreen;
                }
                if (da < -0.5)
                {
                    direction = direction | ColorOffset.Green;
                }
            }
            else
            {
                if (da > 0.5)
                {
                    direction = direction | ColorOffset.LackGreen;
                }
                if (da < -0.5)
                {
                    direction = direction | ColorOffset.LackRed;
                }
            }
            return direction;
        }

        public static ColorOffset CalculateDeltaBColorOffset(CIELab lab, double db)
        {
            ColorOffset direction = ColorOffset.None;

            if (lab.b > 0)
            {
                if (db > 0.5)
                {
                    direction = direction | ColorOffset.Yellow;
                }
                if (db < -0.5)
                {
                    direction = direction | ColorOffset.LackYellow;
                }
            }
            else if (lab.b < 0)
            {
                if (db > 0.5)
                {
                    direction = direction | ColorOffset.LackBlue;
                }
                if (db < -0.5)
                {
                    direction = direction | ColorOffset.Blue;
                }
            }
            else
            {
                if (db > 0.5)
                {
                    direction = direction | ColorOffset.LackBlue;
                }
                if (db < -0.5)
                {
                    direction = direction | ColorOffset.LackYellow;
                }
            }

            return direction;
        }

        public static Spectrum Calculate_Reflect_400_700_To_Reflect_360_780(Spectrum source)
        {
            var range = new WavelengthRange(360, 780, 10);
            float[] array_sin_cof = new float[] { (float)(Math.PI / 16), (float)(Math.PI * 17 / 16), (float)(Math.PI / 8), (float)(Math.PI * 15 / 16), (float)(Math.PI * 7 / 8), 0, (float)(Math.PI * 17 / 16), (float)(Math.PI * 15 / 8) };
            float[] array_cof = new float[8];
            float[] array_poly = new float[2] { -1 / 30000.0f, 0.005f + 1 / 3000.0f };
            float base_700_780 = 0.0f;
            float[] array_tmp_diff = new float[8];
            int index_cnt = 0;

            double[] reflect;

            Interpolate(source.Range, source.Data, range, out reflect);
            // 计算360 370
            if (reflect[3] < 0)
            {
                reflect[3] = 0.0001f;
                reflect[2] = 0.0001f;
            }
            else if (reflect[2] < 0)
                reflect[2] = 0.0001f;
            reflect[1] = reflect[2] * 0.98;
            reflect[0] = reflect[1] * 0.98;

            var xyz = source.ToCIEXYZ(StandardIlluminant.D65, StandardObserver.CIE1964);

            base_700_780 = (float)((array_poly[0] * xyz.Y) + array_poly[1]);
            for (int i = 0; i < (array_sin_cof.Length / sizeof(float)); i++)
            {
                array_cof[i] = (float)Math.Sin(array_sin_cof[i]);
                array_tmp_diff[i] = base_700_780 * array_cof[i];
            }

            for (int i = 35; i < 43; i++)
            {
                reflect[i] = reflect[34] * (array_tmp_diff[index_cnt++] + 1);
            }
            return new Spectrum(range, reflect);
        }
        /// <summary>
        /// 使用三参样条曲线插值算法插值
        /// </summary>
        /// <param name="in_x_range">输入的点X方向上的坐标，固定间隔</param>
        /// <param name="y">输入的点Y方向上的坐标</param>
        /// <param name="out_x_range">输出点的X方向坐标的范围</param>
        /// <param name="out_y_data">返回插值结果</param>
        public static void Interpolate(WavelengthRange in_x_range, double[] y, WavelengthRange out_x_range, out double[] out_y_data)
        {
            out_y_data = new double[out_x_range.Count];
            int i;
            if (in_x_range.Min == out_x_range.Min && in_x_range.Step == out_x_range.Step && in_x_range.Count == out_x_range.Count)
            {
                for (i = 0; i < out_x_range.Count; i++)
                {
                    out_y_data[i] = y[i];
                }
            }
            else
            {

                double[] x = new double[out_x_range.Count];

                for (i = 0; i < in_x_range.Count; i++)
                {
                    out_y_data[i] = (in_x_range.Min + i * in_x_range.Step);
                }

                var curve = CubicSplineCurveHelper.CubicSplineCurve_Simulate_f(in_x_range.Count, x, y, 2, 0, 0);
                //assert(curve != NULL);
                if (curve == null) return;

                for (i = 0; i < out_x_range.Count; i++)
                {
                    out_y_data[i] = (float)curve.CubicSplineCurve_GetItem((float)(out_x_range.Min + i * out_x_range.Step));
                }
            }
        }


        public static float IntegralQuantity(Spectrum reflect)
        {
            float data = 0;
            float[] integral_percent = new float[] { 0, 0, 0, 0, 0.0004f, 0.0012f, 0.0040f, 0.0116f, 0.023f, 0.038f, 0.06f, 0.091f, 0.139f, 0.208f, 0.323f, 0.503f, 0.71f, 0.862f, 0.954f, 0.955f, 0.995f, 0.952f, 0.87f, 0.757f, 0.631f, 0.503f, 0.381f, 0.265f, 0.175f, 0.107f, 0.061f, 0.032f, 0.017f, 0.0082f, 0.0041f, 0, 0, 0, 0, 0, 0, 0, 0, };
            int end_wavelength = reflect.Range.Min + reflect.Range.Step * reflect.Range.Count;
            for (int i = 0; i < integral_percent.Length; i++)
            {
                int wavelength = 360 + i * 10;
                if (wavelength < reflect.Range.Min || wavelength > end_wavelength)
                {
                    continue;
                }
                data += (float)reflect[wavelength] * integral_percent[i];
            }
            return data;
        }

        /// <summary>
        /// 获取灯能量
        /// </summary>
        /// <param name="table"></param>
        /// <param name="wavelength"></param>
        /// <returns></returns>
        public static float GetLightEnergy(EnergyDistributionTable table, int wavelength)
        {
            if (wavelength >= table.Minimum && wavelength <= table.Maximum)
            {
                return table.data[(wavelength - table.Minimum) / table.Interval];
            }
            return 0.0f;
        }
        #endregion

        #region 颜色

        /// <summary>
        /// 光谱数据转CIEXYZ
        /// </summary>
        /// <param name="spectralData">光谱数据</param>
        /// <param name="illuminant">光源</param>
        /// <param name="observer">观察者</param>
        /// <returns>CIEXYZ</returns>
        public static CIEXYZ ToCIEXYZ(this Spectrum spectralData, StandardIlluminant illuminant, StandardObserver observer)
        {
            if (spectralData == null) return new CIEXYZ(float.NaN, float.NaN, float.NaN);
            var datas = TristimulusWeightingFactorTable.GetTristimulusValues(illuminant, observer, spectralData.Range.Step);
            var buffer = spectralData.Data;
            var outrange = new WavelengthRange(spectralData.Range.Min, spectralData.Range.Max, spectralData.Range.Step);
            double X = 0, Y = 0, Z = 0;
            for (int i = 0; i < outrange.Count; i++)
            {
                X += datas.Item1[i] * buffer[i];
                Y += datas.Item2[i] * buffer[i];
                Z += datas.Item3[i] * buffer[i];
            }
            var xyz = new CIEXYZ(X, Y, Z);
            return xyz;
        }

        /// <summary>
        /// CIEXYZ转CIELAB
        /// </summary>
        /// <param name="xyz"></param>
        /// <returns></returns>
        public static CIELab ToCIELab(this CIEXYZ xyz,
            StandardIlluminant illuminant = StandardIlluminant.D65,
            StandardObserver observer = StandardObserver.CIE1964)
        {
            if (xyz == null) return null;
            CIELab lab = new CIELab();
            if (xyz.X + xyz.Y + xyz.Z == 0.0f)
            {
                lab.L = lab.a = lab.b = 0.0f;
            }
            else
            {
                var xyz0 = WhitePoints.Get(illuminant, observer);

                double f_Y = MathHelper.f(xyz.Y / xyz0.Y);
                double f_X = MathHelper.f(xyz.X / xyz0.X);
                double f_Z = MathHelper.f(xyz.Z / xyz0.Z);

                lab.L = 116.0 * f_Y - 16.0;
                lab.a = 500.0 * (f_X - f_Y);
                lab.b = 200.0 * (f_Y - f_Z);
            }
            return lab;
        }

        /// <summary>
        /// 将Lab转换为XYZ颜色空间
        /// </summary>
        /// <param name="lab"></param>
        /// <param name="illuminant"></param>
        /// <param name="observer"></param>
        /// <returns></returns>
        public static CIEXYZ ToCIEXYZ(this CIELab lab,
            StandardIlluminant illuminant = StandardIlluminant.D65,
            StandardObserver observer = StandardObserver.CIE1964)
        {
            var xyz = new CIEXYZ();
            if (lab.L == 0.0f && lab.a == 0.0f && lab.b == 0.0f)
            {
                xyz.X = xyz.Y = xyz.Z = 0.0f;
            }
            else
            {
                var xyz0 = WhitePoints.Get(illuminant, observer);

                float f_Y_Yn = ((float)lab.L + 16.0f) / 116.0f;  // f(Y/Yn)
                float Y_Yn = MathHelper.F_Inv(f_Y_Yn); // Y/Yn
                float X_Xn = MathHelper.F_Inv((float)lab.a / 500.0f + f_Y_Yn); // X/Xn
                float Z_Zn = MathHelper.F_Inv(f_Y_Yn - (float)lab.b / 200.0f); // Z/Zn
                xyz.X = xyz0.X * X_Xn;
                xyz.Y = xyz0.Y * Y_Yn;
                xyz.Z = xyz0.Z * Z_Zn;
            }
            return xyz;
        }

        /// <summary>
        /// CIELAB转sRGB
        /// </summary>
        /// <param name="lab"></param>
        /// <returns></returns>
        public static sRGB TosRGB(this CIELab lab, StandardIlluminant illuminant = StandardIlluminant.D65, StandardObserver observer = StandardObserver.CIE1964)
        {
            var rgb = new sRGB();
            if (lab == null)
                return rgb;
            // 线性变换矩阵
            float[][] mat = new float[][] {
                               new float[] { 3.13405134140307f, -1.61702771078607f, -0.49065220978742f},
                               new float[] { -0.97876272987415f, 1.91614222843468f, 0.03344962851486f},
                               new float[] { 0.07194257711719f, -0.22897117960925f, 1.40521830532745f}
                            };


            CIEXYZ xyz = lab.ToCIEXYZ(illuminant, observer);

            var r = SRGB_Convert(mat[0], xyz);
            var g = SRGB_Convert(mat[1], xyz);
            var b = SRGB_Convert(mat[2], xyz);

            return new sRGB(MathHelper.Round_To(r * 255, 0), MathHelper.Round_To(g * 255, 0), MathHelper.Round_To(b * 255, 0));
        }

        /// <summary>
        /// CIEXYZ转βxy
        /// </summary>
        /// <param name="xyz">CIEXYZ</param>
        /// <returns>βxy</returns>
        public static BETAxy ToBETAxy(this CIEXYZ xyz)
        {
            var betaxy = new BETAxy();
            var temp = xyz.X + xyz.Y + xyz.Z;
            if (temp != 0)
            {
                betaxy.BETA = xyz.Y / 100.0;
                betaxy.x = xyz.X / temp;
                betaxy.y = xyz.Y / temp;
            }
            return betaxy;
        }

        /// <summary>
        /// CIELab转DINLab99
        /// </summary>
        /// <param name="lab">CIELab</param>
        /// <returns>DIN Lab99</returns>

        public static DIN99Lab ToDINLab99(this CIELab lab)
        {
            if (lab == null) return null;
            return new DIN99Lab(lab.L, lab.a, lab.b);
        }

        /// <summary>
        /// CIEXYZ转HunterLab
        /// </summary>
        /// <param name="xyz"></param>
        /// <returns></returns>
        public static HunterLab ToHunterLab(this CIEXYZ xyz, StandardIlluminant illuminant, StandardObserver observer)
        {
            var lab = new HunterLab();
            if (xyz.X + xyz.Y + xyz.Z == 0.0f)
            {
                lab.L = lab.a = lab.b = 0.0f;
            }
            else
            {
                double Ka, Kb, sqrt_Y_Yn;
                var xyz0 = GetWhitePointData(illuminant, observer);
                get_KaKb(illuminant, observer, out Ka, out Kb);

                sqrt_Y_Yn = (float)Math.Sqrt(xyz.Y / xyz0.Y);
                lab.L = 100.0f * sqrt_Y_Yn;
                lab.a = Ka * ((xyz.X / xyz0.X) / sqrt_Y_Yn - sqrt_Y_Yn);
                lab.b = Kb * (sqrt_Y_Yn - (xyz.Z / xyz0.Z) / sqrt_Y_Yn);
            }
            return lab;
        }

        /// <summary>
        /// CIEXYZ转CIELUV1976
        /// </summary>
        /// <param name="xyz"></param>
        /// <returns></returns>
        public static CIELUV1976 ToCIELUV1976(this CIEXYZ xyz, StandardIlluminant illuminant = StandardIlluminant.D65, StandardObserver observer = StandardObserver.CIE1964)
        {
            var luv = new CIELUV1976();
            if (xyz.X + xyz.Y + xyz.Z == 0.0f)
            {
                luv.L = luv.u = luv.v = 0.0f;
            }
            else
            {
                var xyz0 = GetWhitePointData(illuminant, observer);

                luv.L = 116.0f * f(Convert.ToSingle(xyz.Y) / Convert.ToSingle(xyz0.Y)) - 16.0f;
                luv.u = 13.0f * luv.L * (uq(xyz) - uq(xyz0));
                luv.v = 13.0f * luv.L * (vq(xyz) - vq(xyz0));
            }
            return luv;
        }

        /// <summary>
        /// CIEYxy转CIEXYZ
        /// </summary>
        /// <param name="xyz">CIEXYZ</param>
        /// <returns>Yxy</returns>
        public static CIEYxy ToCIEYxy(this CIEXYZ xyz)
        {
            var yxy = new CIEYxy();
            double tmp;
            tmp = xyz.X + xyz.Y + xyz.Z;
            if (tmp != 0.0f)
            {
                yxy.Y = xyz.Y;
                yxy.x = xyz.X / tmp;
                yxy.y = xyz.Y / tmp;
            }
            else
            {
                yxy.Y = yxy.x = yxy.y = 0.0f;
            }
            return yxy;
        }

        #endregion


        #region Private

        /// <summary>
        /// 根据光源和观察者获取白点
        /// </summary>
        /// <param name="illuminant"></param>
        /// <param name="observer"></param>
        /// <returns></returns>
        private static CIEXYZ GetWhitePointData(StandardIlluminant illuminant, StandardObserver observer)
        {
            return TristimulusWeightingFactorTable.TRISTIMULUS_WEIGHTING_WEIGHTING_FACTORS_10NM_TABLE[observer, illuminant].WhitePortPosition;
        }

        /// <summary>
        /// float 转FastnessGrade
        /// </summary>
        /// <param name="fgrade"></param>
        /// <returns></returns>
        private static ColorFastnessGrade float_fastness_grade_to_enum(double fgrade)
        {
            int ival;
            double fval;

            if (fgrade < 1.25)
                return ColorFastnessGrade.clralgo_GRADE_1;
            if (fgrade >= 4.75)
                return ColorFastnessGrade.clralgo_GRADE_5;

            ival = (int)fgrade;
            fval = fgrade - ival;
            if (fval >= 0.25 && fval < 0.75)
                return (ColorFastnessGrade)(ival * 2);
            if (fval >= 0.75)
                ival += 1;
            return (ColorFastnessGrade)(ival * 2 - 1);
        }

        private static float f(float a)
        {
            if (a > (float)Math.Pow((24.0 / 116.0), 3))
                return (float)Math.Pow(a, (1.0 / 3.0));
            else
                return (841.0f / 108.0f * a + 16.0f / 116.0f);
        }

        private static byte round_to(double x, int precision)
        {
            float result = (int)x;
            float mult = (float)Math.Pow(10.0, precision);
            float temp = Convert.ToSingle(x - result) * mult;
            float t = temp - (int)temp;

            if (t > 0.5f)
                temp += 0.5f;
            else if (t < -0.5f)
                temp -= 0.5f;
            return (byte)(result + ((int)temp) / mult);
        }

        // 计算u'的公式
        private static double uq(CIEXYZ xyz)
        {
            return 4.0f * xyz.X / (xyz.X + 15.0f * xyz.Y + 3 * xyz.Z);
        }

        // 计算v'的公式
        private static double vq(CIEXYZ xyz)
        {
            return 9.0f * xyz.Y / (xyz.X + 15.0f * xyz.Y + 3 * xyz.Z);
        }

        // 获取HunterLab和CIEXYZ互相转换的系数
        private static void get_KaKb(StandardIlluminant illuminant, StandardObserver observer, out double Ka, out double Kb)
        {

            if (observer == StandardObserver.CIE1931)
            {
                switch (illuminant)
                {
                    case StandardIlluminant.A:
                        Ka = 185.20f;
                        Kb = 38.40f;
                        break;
                    case StandardIlluminant.C:
                        Ka = 175.00f;
                        Kb = 70.00f;
                        break;
                    case StandardIlluminant.D50:
                        Ka = 173.51f;
                        Kb = 58.48f;
                        break;
                    case StandardIlluminant.D65:
                        Ka = 172.30f;
                        Kb = 67.20f;
                        break;
                    case StandardIlluminant.D75:
                        Ka = 172.22f;
                        Kb = 71.30f;
                        break;
                    case StandardIlluminant.F2:
                        Ka = 175.00f;
                        Kb = 52.90f;
                        break;
                    case StandardIlluminant.F11:
                        Ka = 178.00f;
                        Kb = 52.30f;
                        break;
                    case StandardIlluminant.F12:
                        Ka = 183.70f;
                        Kb = 37.50f;
                        break;
                    default:
                        {
                            CIEXYZ xyz = GetWhitePointData(illuminant, observer);
                            Ka = 175.0f / 198.04f * (xyz.X + xyz.Y);
                            Kb = 70.0f / 218.11f * (xyz.Y + xyz.Z);
                        }
                        break;
                }
            }
            else
            {
                switch (illuminant)
                {
                    case StandardIlluminant.A:
                        Ka = 186.30f;
                        Kb = 38.20f;
                        break;
                    case StandardIlluminant.C:
                        Ka = 174.30f;
                        Kb = 69.40f;
                        break;
                    case StandardIlluminant.D50:
                        Ka = 173.82f;
                        Kb = 58.13f;
                        break;
                    case StandardIlluminant.D65:
                        Ka = 172.10f;
                        Kb = 66.70f;
                        break;
                    case StandardIlluminant.D75:
                        Ka = 171.76f;
                        Kb = 70.76f;
                        break;
                    case StandardIlluminant.F2:
                        Ka = 178.60f;
                        Kb = 53.60f;
                        break;
                    case StandardIlluminant.F11:
                        Ka = 180.10f;
                        Kb = 52.70f;
                        break;
                    case StandardIlluminant.F12:
                        Ka = 186.30f;
                        Kb = 38.20f;
                        break;
                    default:
                        {
                            var xyz = GetWhitePointData(illuminant, observer);
                            Ka = 175.0f / 198.04f * (xyz.X + xyz.Y);
                            Kb = 70.0f / 218.11f * (xyz.Y + xyz.Z);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 获取555色调分类的等级
        /// </summary>
        /// <param name="d">试样的L, a, b减标样的差值之一</param>
        /// <param name="tol">容差，必须大于0</param>
        /// <returns></returns>
        private static int get_shade_number(float d, float tol)
        {
            int gradeL;
            if (tol <= 0)
            {
                return 0;
            }

            if (d >= -tol && d < tol)
                gradeL = 5;
            else if (d >= -2 * tol && d < -tol)
                gradeL = 4;
            else if (d >= -3 * tol && d < -2 * tol)
                gradeL = 3;
            else if (d >= -4 * tol && d < -3 * tol)
                gradeL = 2;
            else if (d < -4 * tol)
                gradeL = 1;
            else if (d > tol && d <= 2 * tol)
                gradeL = 6;
            else if (d > 2 * tol && d <= 3 * tol)
                gradeL = 7;
            else if (d > 3 * tol && d <= 4 * tol)
                gradeL = 8;
            else
                gradeL = 9;

            return gradeL;
        }

        /// <summary>
        /// 计算变色牢度
        /// </summary>
        /// <param name="illuminant">standard和test使用的标准光源</param>
        /// <param name="observer">standard和test使用的标准观察者角度</param>
        /// <param name="test">试样的Lab值</param>
        /// <param name="standard">标样的Lab值</param>
        /// <param name="_gsr">如果不为空存储连续的色牢度等级</param>
        /// <param name="_dEf">如果不为空存储ΔEf</param>
        /// <returns>变色牢度；如果指定条件无效返回clralgo_COLOR_FASTNESS_UNDEFINED</returns>
        private static ColorFastnessGrade CalculateRsr(
        StandardIlluminant illuminant,
        StandardObserver observer,
        CIELab test,
        CIELab standard,
        out double _gsr, out double _dEf)
        {
            _gsr = float.NaN;
            _dEf = float.NaN;
            if (illuminant == StandardIlluminant.D65 || illuminant == StandardIlluminant.C)
            {
                double dL, dC, dH, Cm, X, D, dCk, dHk, dCf, dHf;
                var lch_test = test.ToLCh();
                var lch_std = standard.ToLCh();
                CalculatDeltaLCH(lch_test, lch_std, out dL, out dC, out dH);

                Cm = Convert.ToSingle(lch_test.C + lch_std.C) / 2;
                X = (float)Math.Pow((360.0f - Math.Abs(MathHelper.AverageDegree(Convert.ToSingle(lch_test.h), Convert.ToSingle(lch_std.h)) - 280.0f)) / 30.0f, 2);
                D = dC * Cm * (float)Math.Exp(-X) / 100.0f;
                dCk = dC - D;
                dHk = dH - D;
                dCf = dCk / (1.0f + (float)Math.Pow(20.0f * Cm / 1000.0f, 2));
                dHf = dHk / (1.0f + (float)Math.Pow(10.0f * Cm / 1000.0f, 2));
                _dEf = (float)Math.Sqrt(dL * dL + dCf * dCf + dHf * dHf);
                _dEf = (float)round_to(_dEf, 2);

                _gsr = _dEf > 3.40f ? (float)(5.0f - Math.Log10(_dEf / 0.85f) / Math.Log10(2.0f)) : (5.0f - _dEf / 1.7f);
                _gsr = (float)round_to(_gsr, 2);

                return float_fastness_grade_to_enum(_gsr);
            }

            return ColorFastnessGrade.clralgo_COLOR_FASTNESS_UNDEFINED;
        }

        /// <summary>
        /// 计算lch1 - lch2的ΔL*、ΔC*、ΔH*
        /// </summary>
        /// <param name="lch1">样品1的LCh值</param>
        /// <param name="lch2">样品2的LCh值</param>
        /// <param name="dL">ΔL*</param>
        /// <param name="dC">ΔC*</param>
        /// <param name="dH">ΔH*</param>
        private static void CalculatDeltaLCH(CIELCh lch1, CIELCh lch2, out double dL, out double dC, out double dH)
        {
            double dh;

            dL = lch1.L - lch2.L;
            dC = lch1.C - lch2.C;
            dh = lch1.h - lch2.h;
            if (dh > 180.0f)
                dh -= 360.0f;
            else if (dh < -180.0f)
                dh += 360.0f;
            dH = 2.0 * Math.Sqrt(lch1.C * lch2.C) * Math.Sin(MathHelper.RADIANS(Convert.ToSingle(dh) / 2.0f));
        }

        /// <summary>
        /// 计算沾色牢度
        /// </summary>
        /// <param name="illuminant">standard和test使用的标准光源</param>
        /// <param name="observer">standard和test使用的标准观察者角度</param>
        /// <param name="test">试样的Lab值</param>
        /// <param name="standard">标样的Lab值</param>
        /// <param name="_ssr">如果不为空存储连续的色牢度等级</param>
        /// <param name="_dEgs">如果不为空存储ΔEgs</param>
        /// <returns>沾色牢度；如果指定条件无效返回clralgo_COLOR_FASTNESS_UNDEFINED</returns>
        private static ColorFastnessGrade CalculateSsr(
        StandardIlluminant illuminant,
        StandardObserver observer,
        CIELab test,
        CIELab standard,
        out double _ssr, out double _dEgs)
        {
            _ssr = double.NaN;
            _dEgs = double.NaN;
            if ((observer == StandardObserver.CIE1931 && illuminant == StandardIlluminant.C)
                    || (observer == StandardObserver.CIE1964 && illuminant == StandardIlluminant.D65))
            {
                double dL, dE;

                dL = test.L - standard.L;
                dE = CalculateDeltaEab(test, standard);
                _dEgs = dE - 0.4f * (float)Math.Sqrt(dE * dE - dL * dL);
                _dEgs = (float)round_to(_dEgs, 2);
                _ssr = _dEgs >= 4.26f ? (6.1f - 1.45f * (float)Math.Log(_dEgs)) : (5.0f - 0.23f * _dEgs);
                _ssr = (float)round_to(_ssr, 2);

                return float_fastness_grade_to_enum(_ssr);
            }

            return ColorFastnessGrade.clralgo_COLOR_FASTNESS_UNDEFINED;
        }

        // 获取X、Y、Z与A、G、B之间的变换系数
        private static bool GetAGB(StandardIlluminant illuminant,
                     StandardObserver observer,
                    out float fXA, out float fXB, out float fZB)
        {
            fXA = float.NaN;
            fXB = float.NaN;
            fZB = float.NaN;
            float[][] cie1937 = new float[][] {
                new float[] { 1.0447f, 0.0539f, 0.3558f },  // A
                new float[]{ 0.8061f, 0.1504f, 0.9209f },	 // D55
                new float[]{ 0.7701f, 0.1804f, 1.0889f },	 // D65
                new float[]{ 0.7446f, 0.2047f, 1.2256f },	 // D75
                new float[]{ 0.7832f, 0.1975f, 1.1822f }    // C
            };
            float[][] cie1964 = new float[][] {
                new float[] { 1.0571f, 0.0544f, 0.3520f },  // A
                new float[] { 0.8078f, 0.1502f, 0.9098f },  // D55
                new float[] { 0.7683f, 0.1798f, 1.0733f },  // D65
                new float[]{ 0.7405f, 0.2038f, 1.2073f },  // D75
                new float[]{ 0.7772f, 0.1957f, 1.1614f }   // C
            };

            int index = 0;
            switch (illuminant)
            {
                case StandardIlluminant.A: index = 0; break;
                case StandardIlluminant.D55: index = 1; break;
                case StandardIlluminant.D65: index = 2; break;
                case StandardIlluminant.D75: index = 3; break;
                case StandardIlluminant.C: index = 4; break;
                default: return false;
            }

            if (observer == StandardObserver.CIE1931)
            {
                fXA = cie1937[index][0];
                fXB = cie1937[index][1];
                fZB = cie1937[index][2];
            }
            else
            {
                fXA = cie1964[index][0];
                fXB = cie1964[index][1];
                fZB = cie1964[index][2];
            }
            return true;
        }

        private static float SRGB_Convert(float[] CL, CIEXYZ xyz)
        {
            float tmp = CL[0] * (float)xyz.X + CL[1] * (float)xyz.Y + CL[2] * (float)xyz.Z;
            tmp = tmp / 100.0f;

            if (tmp * 12.92f <= 0.04045f)
                tmp = tmp * 12.92f;
            else
                tmp = 1.055f * (float)Math.Pow(tmp, 1.0 / 2.4) - 0.055f;
            if (tmp < 0.0) tmp = 0.0f;
            if (tmp > 1.0) tmp = 1.0f;
            return tmp;
        }
        #endregion

    }
}
