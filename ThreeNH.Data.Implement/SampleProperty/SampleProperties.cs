using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ThreeNH.Color.Algorithm;
using ThreeNH.Color.Chromaticity;
using ThreeNH.Color.Enums;
using ThreeNH.Extensions;

namespace ThreeNH.Data.Implement.SampleProperty
{
    public class SampleProperties
    {
        #region 通用属性

        // 名称
        public static SamplePropertyInfo Common_Name()
        {
            return new SamplePropertyInfo()
            {
                Name = nameof(Common_Name),
                ValueType = typeof(string),
                Evaluate = (settings, sample, target) => sample?.Name
            };
        }

        // 时间
        public static SamplePropertyInfo Common_DateTime()
        {
            return new SamplePropertyInfo()
            {
                Name = nameof(Common_DateTime),
                ValueType = typeof(DateTime),
                Evaluate = (settings, sample, target) => sample?.DateTime
            };
        }

        // 料号
        public static SamplePropertyInfo Common_Material()
        {
            return CreatePropertyInfo(typeof(string), (settings, sample, target) => sample?.Material);
        }

        // 备注
        public static SamplePropertyInfo Common_Remark()
        {
            return CreatePropertyInfo(typeof(string), (settings, sample, target) => sample?.Remark);
        }

        // 配方
        public static SamplePropertyInfo Common_ColorFormula()
        {
            return CreatePropertyInfo(typeof(string), (settings, sample, target) => sample?.ColorFormula);
        }

        #endregion

        #region 颜色属性

        // 测量条件
        public static SamplePropertyInfo Color_Channel()
        {
            return CreatePropertyInfo(typeof(string),
                (settings, sample, target) =>
                    (GetColorChannel(settings)));
        }

        // 光源/观察者
        public static SamplePropertyInfo Color_IlluminantObserver()
        {
            return CreatePropertyInfo(typeof(string), (settings, sample, target) =>
            {
                GetColorSetting(settings, out var channel, out var illuminant, out var observer);
                return string.Format("{0}/{1}", illuminant.ToString(),
                    observer == StandardObserver.CIE1931 ? "2\u00B0" : "10\u00B0");
            });
        }

        // 仿真色
        public static SamplePropertyInfo Color_PseudoColor()
        {
            return CreatePropertyInfo(typeof(uint), (settings, sample, target) =>
                sample?.GetRgb(settings)?.ToInteger());
        }

        // CIE L*a*b* - L*
        public static SamplePropertyInfo Color_CIELAB_L()//s1_L
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                return sample?.GetLab(settings)?.L;
            });
        }

        // CIE L*a*b* - a*
        public static SamplePropertyInfo Color_CIELAB_a()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
                sample?.GetLab(settings)?.a);
        }

        // CIE L*a*b* - b*
        public static SamplePropertyInfo Color_CIELAB_b()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
                sample?.GetLab(settings)?.b);
        }

        // CIELUV 1976 L*
        public static SamplePropertyInfo Color_Luv1976_L()
        {
            return CreatePropertyInfo(typeof(double), ((settings, sample, target) =>
                sample?.GetLuv1976(settings)?.L));
        }

        // CIELUV 1976 u*
        public static SamplePropertyInfo Color_Luv1976_u()
        {
            return CreatePropertyInfo(typeof(double), ((settings, sample, target) =>
                sample?.GetLuv1976(settings)?.u));
        }

        // CIELUV 1976 v*
        public static SamplePropertyInfo Color_Luv1976_v()
        {
            return CreatePropertyInfo(typeof(double), ((settings, sample, target) =>
                sample?.GetLuv1976(settings)?.v));
        }


        // CIE L*C*h° - C*
        public static SamplePropertyInfo Color_CIELCH_C()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
                sample?.GetLab(settings)?.C);
        }

        // CIE L*C*h° - h°
        public static SamplePropertyInfo Color_CIELCH_h()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
                sample?.GetLab(settings)?.h);
        }

        // 三刺激值X
        public static SamplePropertyInfo Color_CIEXYZ_X()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
                sample?.GetXYZ(settings)?.X);
        }

        // 三刺激值Y
        public static SamplePropertyInfo Color_CIEXYZ_Y()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
                sample?.GetXYZ(settings)?.Y);
        }

        // 三刺激值Z
        public static SamplePropertyInfo Color_CIEXYZ_Z()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
                sample?.GetXYZ(settings)?.Z);
        }

        // 色品坐标x
        public static SamplePropertyInfo Color_CIEYxy_x()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
                sample?.GetXYZ(settings)?.x);
        }

        // 色品坐标y
        public static SamplePropertyInfo Color_CIEYxy_y()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
                sample?.GetXYZ(settings)?.y);
        }


        public static SamplePropertyInfo Color_BETAxy_BETA()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
                sample?.GetXYZ(settings)?.ToBETAxy()?.BETA);
        }

        public static SamplePropertyInfo Color_BETAxy_x()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
                sample?.GetXYZ(settings)?.ToBETAxy().x);
        }

        public static SamplePropertyInfo Color_BETAxy_y()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
                sample?.GetXYZ(settings)?.ToBETAxy().y);
        }

        public static SamplePropertyInfo Color_HunterLab_L()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
                sample?.GetHunterLab(settings)?.L);
        }

        public static SamplePropertyInfo Color_HunterLab_a()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
                sample?.GetHunterLab(settings)?.a);
        }

        public static SamplePropertyInfo Color_HunterLab_b()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
                sample?.GetHunterLab(settings)?.b);
        }

        public static SamplePropertyInfo Color_DINLab99_L()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
                sample?.GetLab(settings).ToDINLab99()?.L);
        }

        public static SamplePropertyInfo Color_DINLab99_a()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
                sample?.GetLab(settings).ToDINLab99()?.a);
        }

        public static SamplePropertyInfo Color_DINLab99_b()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
                sample?.GetLab(settings).ToDINLab99()?.b);
        }

        public static SamplePropertyInfo Color_Munsell_H()
        {
            return CreatePropertyInfo(typeof(string), (settings, sample, target) =>
            {
                var yxy = sample?.GetXYZ(settings)?.ToCIEYxy();
                if (yxy == null) return double.NaN;
                var munsell = new Color.Model.Munsell(yxy);
                return $"{munsell.H.ToString("F2")} {munsell.HUnit}";
            });
        }
        public static SamplePropertyInfo Color_Munsell_V()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                var yxy = sample?.GetXYZ(settings)?.ToCIEYxy();
                if (yxy == null) return double.NaN;
                return new ThreeNH.Color.Model.Munsell(yxy).V;
            });
        }
        public static SamplePropertyInfo Color_Munsell_C()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                var yxy = sample?.GetXYZ(settings)?.ToCIEYxy();
                if (yxy == null) return double.NaN;
                return new ThreeNH.Color.Model.Munsell(yxy).C;
            });
        }

        // 仿真色 R
        public static SamplePropertyInfo Color_RGB_R()
        {
            return CreatePropertyInfo(typeof(int), ((settings, sample, target) =>
                (int?)sample?.GetRgb(settings)?.R));
        }

        // 仿真色 G
        public static SamplePropertyInfo Color_RGB_G()
        {
            return CreatePropertyInfo(typeof(int), ((settings, sample, target) =>
                (int?)sample?.GetRgb(settings)?.G));
        }

        // 仿真色 B
        public static SamplePropertyInfo Color_RGB_B()
        {
            return CreatePropertyInfo(typeof(int), ((settings, sample, target) =>
                (int?)sample?.GetRgb(settings)?.B));
        }

        #endregion

        #region 色差属性

        // Δa*
        public static SamplePropertyInfo ColorDiff_CIELAB_a()
        {
            return CreatePropertyInfo(typeof(double),
                (settings, sample, target) =>
                {
                    if (sample == null || target == null) return double.NaN;
                    return (sample?.GetLab(settings)?.a - target?.GetLab(settings)?.a).GetValueOrDefault();
                });
        }

        // Δb*
        public static SamplePropertyInfo ColorDiff_CIELAB_b()
        {
            return CreatePropertyInfo(typeof(double),
                (settings, sample, target) =>
                {
                    if (sample == null || target == null) return double.NaN;
                    return (sample?.GetLab(settings)?.b - target?.GetLab(settings)?.b).GetValueOrDefault();
                });
        }

        // ΔL*
        public static SamplePropertyInfo ColorDiff_CIELAB_L()
        {
            return CreatePropertyInfo(typeof(double),
                (settings, sample, target) =>
                {
                    if (sample == null || target == null) return double.NaN;
                    return ((sample?.GetLab(settings)?.L - target?.GetLab(settings)?.L).GetValueOrDefault());
                });
        }

        public static SamplePropertyInfo ColorDiff_CIELuv_L()
        {
            return CreatePropertyInfo(typeof(double),
                (settings, sample, target) =>
                {
                    if (sample == null || target == null) return double.NaN;
                    return ((sample?.GetLuv1976(settings)?.L - target?.GetLuv1976(settings)?.L).GetValueOrDefault());
                });
        }
        public static SamplePropertyInfo ColorDiff_CIELuv_u()
        {
            return CreatePropertyInfo(typeof(double),
                (settings, sample, target) =>
                {
                    if (sample == null || target == null) return double.NaN;
                    return ((sample?.GetLuv1976(settings)?.u - target?.GetLuv1976(settings)?.u).GetValueOrDefault());
                });
        }
        public static SamplePropertyInfo ColorDiff_CIELuv_v()
        {
            return CreatePropertyInfo(typeof(double),
                (settings, sample, target) =>
                {
                    if (sample == null || target == null) return double.NaN;
                    return ((sample?.GetLuv1976(settings)?.v - target?.GetLuv1976(settings)?.v).GetValueOrDefault());
                });
        }
        public static SamplePropertyInfo ColorDiff_CIELCH_L()
        {
            return CreatePropertyInfo(typeof(double),
                (settings, sample, target) =>
                {
                    if (sample == null || target == null) return double.NaN;
                    return (sample?.GetLab(settings)?.C - target?.GetLab(settings)?.C);
                });
        }

        // ΔC*
        public static SamplePropertyInfo ColorDiff_CIELCH_C()
        {
            return CreatePropertyInfo(typeof(double),
                (settings, sample, target) =>
                {
                    if (sample == null || target == null) return double.NaN;
                    return (sample?.GetLab(settings)?.C - target?.GetLab(settings)?.C);
                });
        }

        // ΔH*
        public static SamplePropertyInfo ColorDiff_CIELCH_H()
        {
            return CreatePropertyInfo(typeof(double),
                (settings, sample, target) =>
                {
                    if (sample == null || target == null) return double.NaN;
                    var labSample = sample.GetLab(settings);
                    var labTarget = target.GetLab(settings);
                    if (labSample != null && labTarget != null)
                    {
                        return ColorAlgorithm.CalculateDeltaH(labSample, labTarget);
                    }
                    return double.NaN;
                });
        }


        // ΔX
        public static SamplePropertyInfo ColorDiff_CIEXYZ_X()
        {
            return CreatePropertyInfo(typeof(double),
                (settings, sample, target) =>
                {
                    if (sample == null || target == null) return double.NaN;
                    return sample?.GetXYZ(settings)?.X - target?.GetXYZ(settings)?.X;
                });
        }

        // ΔY
        public static SamplePropertyInfo ColorDiff_CIEXYZ_Y()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetXYZ(settings)?.Y - target?.GetXYZ(settings)?.Y;
            });
        }

        // ΔZ
        public static SamplePropertyInfo ColorDiff_CIEXYZ_Z()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetXYZ(settings)?.Z - target?.GetXYZ(settings)?.Z;
            });
        }

        public static SamplePropertyInfo ColorDiff_CIEYxy_Y()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetXYZ(settings)?.Y - target?.GetXYZ(settings)?.Y;
            });
        }

        // Δx
        public static SamplePropertyInfo ColorDiff_CIEYxy_x()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetXYZ(settings)?.x - target?.GetXYZ(settings)?.x;
            });
        }

        // Δy
        public static SamplePropertyInfo ColorDiff_CIEYxy_y()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetXYZ(settings)?.y - target?.GetXYZ(settings)?.y;
            });
        }

        public static SamplePropertyInfo ColorDiff_BETAxy_BETA()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetXYZ(settings)?.ToBETAxy().BETA - target?.GetXYZ(settings)?.ToBETAxy().BETA;
            });
        }
        public static SamplePropertyInfo ColorDiff_BETAxy_x()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetXYZ(settings)?.ToBETAxy().x - target?.GetXYZ(settings)?.ToBETAxy().x;
            });
        }

        public static SamplePropertyInfo ColorDiff_BETAxy_y()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetXYZ(settings).ToBETAxy().y - target?.GetXYZ(settings)?.ToBETAxy().y;
            });
        }
        // Hunter Lab - a
        public static SamplePropertyInfo ColorDiff_HunterLab_a()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetHunterLab(settings)?.a - target?.GetHunterLab(settings)?.a;
            });
        }

        // Hunter Lab - b
        public static SamplePropertyInfo ColorDiff_HunterLab_b()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetHunterLab(settings)?.b - target?.GetHunterLab(settings)?.b;
            });
        }

        // Hunter Lab - L
        public static SamplePropertyInfo ColorDiff_HunterLab_L()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetHunterLab(settings)?.L - target?.GetHunterLab(settings)?.L;
            });
        }

        // CIELUV 1976 L*
        public static SamplePropertyInfo ColorDiff_Luv1976_L()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetLuv1976(settings)?.L - target?.GetLuv1976(settings)?.L;
            });
        }

        // CIELUV 1976 u*
        public static SamplePropertyInfo ColorDiff_Luv1976_u()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetLuv1976(settings)?.u - target?.GetLuv1976(settings)?.u;
            });
        }

        // CIELUV 1976 v*
        public static SamplePropertyInfo ColorDiff_Luv1976_v()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetLuv1976(settings)?.v - target?.GetLuv1976(settings)?.v;
            });
        }
        // 仿真色 B
        public static SamplePropertyInfo ColorDiff_RGB_B()
        {
            return CreatePropertyInfo(typeof(int), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetRgb(settings)?.B - target?.GetRgb(settings)?.B;
            });
        }

        // 仿真色 G
        public static SamplePropertyInfo ColorDiff_RGB_G()
        {
            return CreatePropertyInfo(typeof(int), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetRgb(settings)?.G - target?.GetRgb(settings)?.G;
            });
        }

        // 仿真色 R
        public static SamplePropertyInfo ColorDiff_RGB_R()
        {
            return CreatePropertyInfo(typeof(int), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetRgb(settings)?.R - target?.GetRgb(settings)?.R;
            });
        }

        public static SamplePropertyInfo ColorDiff_DINLab99_L()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetRgb(settings)?.R - target?.GetRgb(settings)?.R;
            });
        }

        public static SamplePropertyInfo ColorDiff_DINLab99_a()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetRgb(settings)?.R - target?.GetRgb(settings)?.R;
            });
        }

        public static SamplePropertyInfo ColorDiff_DINLab99_b()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                if (sample == null || target == null) return double.NaN;
                return sample?.GetRgb(settings)?.R - target?.GetRgb(settings)?.R;
            });
        }
        public static SamplePropertyInfo ColorDiff_Munsell_H()
        {
            return CreatePropertyInfo(typeof(string), (settings, sample, target) =>
            {
                return double.NaN;
            });
        }

        public static SamplePropertyInfo ColorDiff_Munsell_V()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                return double.NaN;
            });
        }

        public static SamplePropertyInfo ColorDiff_Munsell_C()
        {
            return CreatePropertyInfo(typeof(double), (settings, sample, target) =>
            {
                return double.NaN;
            });
        }

        #endregion 色差属性

        #region 色差公式

        // ΔE*
        public static SamplePropertyInfo ColorDiffFormula_DE1976()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) => sample?.GetDe1976(target, settings)?.dE));
        }

        // ΔE1994
        public static SamplePropertyInfo ColorDiffFormula_DE1994()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) => sample?.GetDe1994(target, settings)?.dE));
        }

        // ΔL94
        public static SamplePropertyInfo ColorDiffFormula_DL1994()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) => sample?.GetDe1994(target, settings)?.dL));
        }

        // ΔC94
        public static SamplePropertyInfo ColorDiffFormula_DC1994()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) => sample?.GetDe1994(target, settings)?.dC));
        }

        // ΔH94
        public static SamplePropertyInfo ColorDiffFormula_DH1994()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) => sample?.GetDe1994(target, settings)?.dH));
        }


        // ΔE2000
        public static SamplePropertyInfo ColorDiffFormula_DE2000()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) => sample?.GetDe2000(target, settings)?.dE));
        }


        // ΔL00
        public static SamplePropertyInfo ColorDiffFormula_DL2000()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) => sample?.GetDe2000(target, settings)?.dL));
        }
        public static SamplePropertyInfo ColorDiffFormula_DLq2000()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) => sample?.GetDe2000(target, settings)?.dL));
        }

        // ΔC00
        public static SamplePropertyInfo ColorDiffFormula_DC2000()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) => sample?.GetDe2000(target, settings)?.dC));
        }
        public static SamplePropertyInfo ColorDiffFormula_DCq2000()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) => sample?.GetDe2000(target, settings)?.dC));
        }

        // ΔH00
        public static SamplePropertyInfo ColorDiffFormula_DH2000()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) => sample?.GetDe2000(target, settings)?.dH));
        }

        public static SamplePropertyInfo ColorDiffFormula_DHq2000()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) => sample?.GetDe2000(target, settings)?.dH));
        }

        // CMC
        public static SamplePropertyInfo ColorDiffFormula_DECMC()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) => sample?.GetCmcDe(target, settings)?.dE));
        }

        // CMC-ΔL00
        public static SamplePropertyInfo ColorDiffFormula_DLcmc()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) => sample?.GetCmcDe(target, settings)?.dL));
        }

        // CMC-ΔC00
        public static SamplePropertyInfo ColorDiffFormula_DCcmc()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) => sample?.GetCmcDe(target, settings)?.dC));
        }

        // CMC-ΔH
        public static SamplePropertyInfo ColorDiffFormula_DHcmc()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) => sample?.GetCmcDe(target, settings)?.dH));
        }

        public static SamplePropertyInfo ColorDiffFormula_DEDIN99()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) => sample?.GetDeDIN99(target, settings)?.dE));
        }

        public static SamplePropertyInfo ColorDiffFormula_DEHunter()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) => sample?.GetDeHunter(target, settings)?.dE));
        }

        public static SamplePropertyInfo ColorDiffFormula_DEUV()
        {
            return CreatePropertyInfo(typeof(double),
                (settings, sample, target) => sample?.GetDeltaLuv(target, settings)?.dE);
        }

        #endregion


        #region 颜色指数

        // 555分类
        public static SamplePropertyInfo ColorIndex_555Shade()
        {
            return CreatePropertyInfo(typeof(int?),
                ((settings, sample, target) =>
                {
                    var shadeSetting = settings.ContainsKey(SamplePropertySettingKeys.Shade555Setting) ?
                        (IShade555Setting)settings[SamplePropertySettingKeys.Shade555Setting] : null;
                    if (shadeSetting == null) return null;
                    switch (shadeSetting.ShadeToleranceType)
                    {
                        case ShadeToleranceType.DeltaLab:
                            var trialLab = sample?.GetLab(settings);
                            var targetLab = target?.GetLab(settings);
                            if (trialLab != null && targetLab != null)
                            {
                                return ColorAlgorithm.Get555Shade(new Tuple<float, float, float>(Convert.ToSingle(trialLab.L), Convert.ToSingle(trialLab.a), Convert.ToSingle(trialLab.b)),
                                   new Tuple<float, float, float>(Convert.ToSingle(targetLab.L), Convert.ToSingle(targetLab.a), Convert.ToSingle(targetLab.b)),
                                   shadeSetting.Factors[SamplePropertySettingKeys.ShadeToleranceL], shadeSetting.Factors[SamplePropertySettingKeys.ShadeToleranceA], shadeSetting.Factors[SamplePropertySettingKeys.ShadeToleranceB]);
                            }
                            break;
                        case ShadeToleranceType.DeltaLCH:
                            var trialLch = sample?.GetLab(settings).ToLCh();
                            var targetLch = target?.GetLab(settings).ToLCh();
                            if (trialLch != null && targetLch != null)
                            {
                                return ColorAlgorithm.Get555Shade(new Tuple<float, float, float>(Convert.ToSingle(trialLch.L), Convert.ToSingle(trialLch.C), Convert.ToSingle(trialLch.h)),
                                   new Tuple<float, float, float>(Convert.ToSingle(targetLch.L), Convert.ToSingle(targetLch.C), Convert.ToSingle(targetLch.h)),
                                   shadeSetting.Factors[SamplePropertySettingKeys.ShadeToleranceL], shadeSetting.Factors[SamplePropertySettingKeys.ShadeToleranceC], shadeSetting.Factors[SamplePropertySettingKeys.ShadeToleranceH]);
                            }
                            break;
                        case ShadeToleranceType.DeltaDIN99Lab:
                            var trialDin99 = sample?.GetLab(settings).ToDINLab99();
                            var targetDin99 = target?.GetLab(settings).ToDINLab99();
                            if (trialDin99 != null && targetDin99 != null)
                            {
                                return ColorAlgorithm.Get555Shade(new Tuple<float, float, float>(Convert.ToSingle(trialDin99.L), Convert.ToSingle(trialDin99.a), Convert.ToSingle(trialDin99.b)),
                                   new Tuple<float, float, float>(Convert.ToSingle(targetDin99.L), Convert.ToSingle(targetDin99.a), Convert.ToSingle(targetDin99.b)),
                                   shadeSetting.Factors[SamplePropertySettingKeys.ShadeToleranceLDIN99], shadeSetting.Factors[SamplePropertySettingKeys.ShadeToleranceADIN99], shadeSetting.Factors[SamplePropertySettingKeys.ShadeToleranceBDIN99]);
                            }
                            break;
                        case ShadeToleranceType.DeltaHunterLab:
                            var trialHunter = sample?.GetHunterLab(settings);
                            var targetHunter = target?.GetHunterLab(settings);
                            if (trialHunter != null && targetHunter != null)
                            {
                                return ColorAlgorithm.Get555Shade(new Tuple<float, float, float>(Convert.ToSingle(trialHunter.L), Convert.ToSingle(trialHunter.a), Convert.ToSingle(trialHunter.b)),
                                   new Tuple<float, float, float>(Convert.ToSingle(targetHunter.L), Convert.ToSingle(targetHunter.a), Convert.ToSingle(targetHunter.b)),
                                   shadeSetting.Factors[SamplePropertySettingKeys.ShadeToleranceLHunter], shadeSetting.Factors[SamplePropertySettingKeys.ShadeToleranceAHunter], shadeSetting.Factors[SamplePropertySettingKeys.ShadeToleranceBHunter]);
                            }
                            break;
                    }
                    //var trialLab = sample?.GetLab(settings);
                    //var targetLab = target?.GetLab(settings);
                    //if (trialLab != null && targetLab != null)
                    //{
                    //    double tolL = settings.ContainsKey(SamplePropertySettingKeys.ShadeToleranceL)
                    //        ? (double)settings[SamplePropertySettingKeys.ShadeToleranceL]
                    //        : 1.0;
                    //    double tolA = settings.ContainsKey(SamplePropertySettingKeys.ShadeToleranceA)
                    //        ? (double)settings[SamplePropertySettingKeys.ShadeToleranceA]
                    //        : 1.0;
                    //    double tolB = settings.ContainsKey(SamplePropertySettingKeys.ShadeToleranceB)
                    //        ? (double)settings[SamplePropertySettingKeys.ShadeToleranceB]
                    //        : 1.0;

                    //    return ColorAlgorithm.Get555Shade(trialLab,targetLab, (float)tolL, (float)tolA, (float)tolB);
                    //}

                    return null;
                }));
        }

        // ASTM D1925-1970 黄度
        public static SamplePropertyInfo ColorIndex_ASTM1925_1970_YI()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    GetColorSetting(settings, out var channel, out var illuminant, out var observer);
                    var spectralData = sample.GetSpectralData(channel);
                    var xyz = spectralData.ToCIEXYZ(illuminant, observer);
                    return xyz?.GetAstmD1925Yellowness(illuminant, observer);
                }));
        }

        // ASTM-E313白度的Tint指数
        public static SamplePropertyInfo ColorIndex_AstmE313_Tint()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    GetColorSetting(settings, out var channel, out var illuminant, out var observer);
                    var xyz = sample.GetXYZ(settings);
                    return xyz?.GetAstmE313Tint(illuminant, observer);
                }));
        }

        // ASTM-E313白度
        public static SamplePropertyInfo ColorIndex_AstmE313_WI()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    GetColorSetting(settings, out var channel, out var illuminant, out var observer);
                    var xyz = sample.GetXYZ(settings);
                    return xyz?.GetAstmE313Whiteness(illuminant, observer);
                }));
        }

        // ASTM E313-2005 黄度
        public static SamplePropertyInfo ColorIndex_AstmE313_YI()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    GetColorSetting(settings, out var channel, out var illuminant, out var observer);
                    var xyz = sample.GetXYZ(settings);
                    return xyz?.GetAstmE313Yellowness(illuminant, observer);
                }));
        }

        // Average Strength
        public static SamplePropertyInfo ColorIndex_AverageStrength()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    var channel = GetColorChannel(settings);
                    var trialClr = sample?.GetColorData(channel);
                    var targetClr = target?.GetColorData(channel);
                    if (trialClr?.SpectralData != null && targetClr?.SpectralData != null &&
                        trialClr.TransmissionFlag == targetClr.TransmissionFlag)
                    {
                        return SampleExtensions.GetAverageStrength(targetClr.SpectralData, trialClr.SpectralData, targetClr.TransmissionFlag);
                    }

                    return null;
                }));
        }

        // CIE ISO Tint指数
        public static SamplePropertyInfo ColorIndex_CIEISO_Tint()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    GetColorSetting(settings, out var channel, out var illuminant, out var observer);
                    var xyz = sample.GetXYZ(settings);
                    return xyz?.GetCIEISOTint(illuminant, observer);
                }));
        }

        // CIE ISO 白度
        public static SamplePropertyInfo ColorIndex_CIEISO_WI()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    GetColorSetting(settings, out var channel, out var illuminant, out var observer);
                    var xyz = sample.GetXYZ(settings);
                    return xyz?.GetAstmE313Whiteness(illuminant, observer);
                }));
        }



        // Color Value Sum
        public static SamplePropertyInfo ColorIndex_ColorValueSum()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    var clr = sample?.GetColorData(GetColorChannel(settings));
                    return clr?.SpectralData.GetColorValueSum(clr.TransmissionFlag);
                }));
        }

        // Color Value SWL
        public static SamplePropertyInfo ColorIndex_ColorValueSWL()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    var clr = sample?.GetColorData(GetColorChannel(settings));
                    return clr?.SpectralData.GetColorValueSWL(clr.TransmissionFlag, out int tmp);
                }));
        }

        // Color Value WSUM
        public static SamplePropertyInfo ColorIndex_ColorValueWSUM()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    var clr = sample?.GetColorData(GetColorChannel(settings));
                    GetColorSetting(settings, out var channel, out var illuminant, out var observer);
                    return clr?.SpectralData.GetColorValueWSUM(clr.TransmissionFlag, illuminant, observer);
                }));
        }

        // Hunter 白度（1942）
        // D65/10和C/2光源下有效
        public static SamplePropertyInfo ColorIndex_Hunter1942_WI()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    var lab = sample.GetHunterLab(settings);
                    GetColorSetting(settings, out var channel, out var illuminant, out var observer);
                    return lab?.GetHunter1942Whiteness(illuminant, observer);
                }));
        }

        // Hunter 白度（1960）
        // D65/10和C/2光源下有效
        public static SamplePropertyInfo ColorIndex_Hunter1960_WI()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    var lab = sample.GetHunterLab(settings);
                    GetColorSetting(settings, out var channel, out var illuminant, out var observer);
                    return lab?.GetHunter1960Whiteness(illuminant, observer);
                }));
        }

        // 同色异谱指数
        public static SamplePropertyInfo ColorIndex_MI(StandardIlluminant illuminant1, StandardObserver observer1, StandardIlluminant illuminant2, StandardObserver observer2)
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    var trialSpectra = sample?.GetSpectralData(GetColorChannel(settings));
                    var targetSpectra = target?.GetSpectralData(GetColorChannel(settings));
                    if (trialSpectra != null && targetSpectra != null)
                    {
                        return SampleExtensions.GetMetamerismIndiex(trialSpectra, targetSpectra, illuminant1, observer1, illuminant2, observer2);
                    }
                    return null;
                }));
        }

        // 遮盖度
        public static SamplePropertyInfo ColorIndex_Opacity()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    var trialXyz = sample?.GetXYZ(settings); // 试样做白底
                    var targetXyz = target?.GetXYZ(settings); // 标样做黑底
                    if (trialXyz != null && targetXyz != null)
                    {
                        return trialXyz.GetOpacity(targetXyz);
                    }

                    return null;
                }));
        }

        // R457白度
        public static SamplePropertyInfo ColorIndex_R457_WI()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    var spectra = sample.GetSpectralData(GetColorChannel(settings));
                    return spectra?.GetR457Whiteness();
                }));
        }


        // 沾色牢度级数
        public static SamplePropertyInfo ColorIndex_ReportedSSR()
        {
            return CreatePropertyInfo(typeof(string),
                ((settings, sample, target) =>
                {
                    var sampleLab = sample?.GetLab(settings);
                    var targetLab = target?.GetLab(settings);
                    if (sampleLab == null || targetLab == null) return double.NaN;
                    GetColorSetting(settings, out var channel, out var illuminant, out var observer);
                    return sampleLab.GetReportedSSR(targetLab, illuminant, observer).Description();
                }));
        }

        // 沾色牢度的dEgs
        public static SamplePropertyInfo ColorIndex_StainingFastness_dEgs()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    var sampleLab = sample?.GetLab(settings);
                    var targetLab = target?.GetLab(settings);
                    if (sampleLab == null || targetLab == null)
                        return double.NaN;
                    GetColorSetting(settings, out var channel, out var illuminant, out var observer);
                    return sampleLab.GetStainingFastness_dEgs(targetLab, illuminant, observer);
                }
                ));
        }

        // Strength SWL
        public static SamplePropertyInfo ColorIndex_StrengthSWL()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    var channel = GetColorChannel(settings);
                    var trialClr = sample?.GetColorData(channel);
                    var targetClr = target?.GetColorData(channel);
                    if (trialClr?.SpectralData != null && targetClr?.SpectralData != null &&
                        trialClr.TransmissionFlag == targetClr.TransmissionFlag)
                    {
                        return SampleExtensions.GetStrengthSWL(targetClr.SpectralData, trialClr.SpectralData, targetClr.TransmissionFlag, out var tmp);
                    }

                    return null;
                }));
        }

        // 坦伯白度
        // 在A、D55、D65、C、D75光源下有效
        public static SamplePropertyInfo ColorIndex_Tapple_WI()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    GetColorSetting(settings, out var channel, out var illuminant, out var observer);
                    var xyz = sample.GetXYZ(settings);
                    return xyz?.GetTapplWhiteness(illuminant, observer);
                }));
        }

        // 陶贝白度
        // 在A、D55、D65、C、D75光源下有效
        public static SamplePropertyInfo ColorIndex_Taubel_WI()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    var xyz = sample.GetXYZ(settings);
                    return xyz?.GetTaubeWhiteness();
                }));
        }

        // 变色牢度级数
        public static SamplePropertyInfo ColorIndex_ReportedGSR()
        {
            return CreatePropertyInfo(typeof(string),
                ((settings, sample, target) =>
                {
                    GetColorSetting(settings, out var channel, out var illuminant, out var observer);
                    var trialLab = sample?.GetSpectralData(channel)?.ToCIELAB(illuminant, observer);
                    var targetLab = target?.GetSpectralData(channel)?.ToCIELAB(illuminant, observer);
                    if (trialLab == null || targetLab == null) return double.NaN;
                    var grade = ColorAlgorithm.CalculatGSR(illuminant, observer, trialLab, targetLab, out var gsr, out var def);
                    return grade.Description();
                }));
        }

        // 变色牢度的ΔEf
        public static SamplePropertyInfo ColorIndex_ColorFastness_dEf()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    GetColorSetting(settings, out var channel, out var illuminant, out var observer);
                    var trialLab = sample?.GetSpectralData(channel)?.ToCIELAB(illuminant, observer);
                    var targetLab = target?.GetSpectralData(channel)?.ToCIELAB(illuminant, observer);
                    if (trialLab == null || targetLab == null) return double.NaN;
                    var grade = ColorAlgorithm.CalculatGSR(illuminant, observer, trialLab, targetLab, out var gsr, out var def);
                    return def;
                }));
        }
        // 最大吸收（或最小反射/透射）波长
        public static SamplePropertyInfo ColorIndex_WavelengthOfMaximumAbsorption()
        {
            return CreatePropertyInfo(typeof(int),
                ((settings, sample, target) =>
                {
                    var clr = sample?.GetColorData(GetColorChannel(settings));
                    if (clr?.SpectralData != null)
                    {
                        clr.SpectralData.GetColorValueSWL(clr.TransmissionFlag,
                            out int wavelengthOfMaximumAbsorption);
                        return wavelengthOfMaximumAbsorption;
                    }
                    return null;
                }));
        }

        // 指定波长处的K/S值或吸收率
        // 绝对力份
        public static SamplePropertyInfo SpectralStrength_At(int wavelength)
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    var clr = sample?.GetColorData(GetColorChannel(settings));
                    if (clr?.SpectralData != null)
                    {
                        return clr.SpectralData.GetColorValueSWLAt(wavelength, clr.TransmissionFlag);
                    }

                    return null;
                }),
                nameof(SpectralStrength_At) + "_" + wavelength);
        }
        // Weighted Strength
        public static SamplePropertyInfo ColorIndex_WeightedStrength()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    var channel = GetColorChannel(settings);
                    var trialClr = sample?.GetColorData(channel);
                    var targetClr = target?.GetColorData(channel);
                    if (trialClr?.SpectralData != null && targetClr?.SpectralData != null &&
                        trialClr.TransmissionFlag == targetClr.TransmissionFlag)
                    {
                        GetColorSetting(settings, out channel, out var illuminant, out var observer);
                        return SampleExtensions.GetWeightedStrength(targetClr.SpectralData, trialClr.SpectralData, targetClr.TransmissionFlag, illuminant, observer);
                    }

                    return null;
                }));
        }

        public static SamplePropertyInfo ColorIndex_GardnerIndex()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    GetColorSetting(settings, out var channel, out var illuminant, out var observer);
                    var spectral = sample?.GetSpectralData(channel);
                    if (spectral == null) return double.NaN;
                    return spectral.GetGardnerIndex(illuminant, observer);
                }));
        }

        public static SamplePropertyInfo ColorIndex_PtCoIndex()
        {
            return CreatePropertyInfo(typeof(double),
                ((settings, sample, target) =>
                {
                    GetColorSetting(settings, out var channel, out var illuminant, out var observer);
                    var spectral = sample?.GetSpectralData(channel);
                    if (spectral == null) return double.NaN;
                    return spectral.GetPtCoIndex(illuminant, observer);
                }));
        }

        /// <summary>
        /// 8度光泽度
        /// </summary>
        /// <returns></returns>
        public static SamplePropertyInfo ColorIndex_Gloss8(ISpectralData wsci, ISpectralData wsce)
        {
            return CreatePropertyInfo(typeof(int),
                ((settings, sample, target) =>
                {
                    var ssci = sample?.GetSpectralData("SCI");
                    var ssce = sample?.GetSpectralData("SCE");
                    if (ssci == null || ssce == null) return null;
                    return (int)SampleExtensions.GetGloss8(ssci, ssce, wsci, wsce);
                }));
        }
        #endregion 颜色指数


        #region 辅助方法

        /// <summary>
        /// 获取属性信息
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <returns>返回值</returns>
        public static SamplePropertyInfo GetPropertyInfo(string propertyName)
        {

            var type = typeof(SampleProperties);

            if (!propertyName.StartsWith("Spectral", StringComparison.OrdinalIgnoreCase))
            {
                var method = type.GetMethod(propertyName, BindingFlags.Static | BindingFlags.Public);
                if (method != null)
                {
                    return (SamplePropertyInfo)method.Invoke(type, new object[0]);//190924lilin 用当前的方法给委托赋值
                }
            }
            else
            {
                var match = Regex.Match(propertyName, @"^(Spectral\w+_At)_(\d+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var method = type.GetMethod(match.Groups[1].Value, BindingFlags.Static | BindingFlags.Public);
                    if (method != null)
                    {
                        return (SamplePropertyInfo)method.Invoke(type,
                            new object[] { Int32.Parse(match.Groups[2].Value) });
                    }
                }
            }
            return null;
            //throw new ArgumentException($"Unknown property '{propertyName}'", nameof(propertyName));
        }
        static SamplePropertyInfo CreatePropertyInfo(Type valueType, EvaluateSampleProperty evaluate, [CallerMemberName] string propertyName = null)
        {
            return new SamplePropertyInfo()
            {
                Name = propertyName,
                ValueType = valueType,
                Evaluate = evaluate
            };
        }


        internal static string GetColorChannel(IDictionary<string, object> settings)
        {
            if (settings.ContainsKey(SamplePropertySettingKeys.Channel))
                return (string)settings[SamplePropertySettingKeys.Channel];
            if (settings.ContainsKey(SamplePropertySettingKeys.ColorSetting))
                return ((IColorSetting)settings[SamplePropertySettingKeys.ColorSetting]).Channel;
            throw new ArgumentException("The 'Channel' is not specified", nameof(settings));
        }

        /// <summary>
        /// 获取颜色设置
        /// </summary>
        internal static void GetColorSetting(IDictionary<string, object> settings, out string channel,
            out StandardIlluminant illuminant,
            out StandardObserver observer)//s3_L
        {
            IColorSetting colorSetting = settings.ContainsKey(SamplePropertySettingKeys.ColorSetting)
                ? (IColorSetting)(settings[SamplePropertySettingKeys.ColorSetting])
                : null;
            //if (colorSetting != null)
            //{
            //    channel = "SCI";
            //    illuminant = StandardIlluminant.D65;
            //    observer = StandardObserver.CIE1964;
            //    return;
            //}
            channel = settings.ContainsKey(SamplePropertySettingKeys.Channel)
                ? (settings[SamplePropertySettingKeys.Channel] as string) ?? ""
                : colorSetting?.Channel ?? "SCI";

            illuminant = settings.ContainsKey(SamplePropertySettingKeys.Illuminant)
                ? (StandardIlluminant)settings[SamplePropertySettingKeys.Illuminant]
                : colorSetting?.Illuminant ?? StandardIlluminant.D65;

            observer = settings.ContainsKey(SamplePropertySettingKeys.Observer)
                ? (StandardObserver)settings[SamplePropertySettingKeys.Observer]
                : colorSetting?.Observer ?? StandardObserver.CIE1964;
        }

        public static RgbColorSystem GetRgbColorSystem(IDictionary<string, object> settings)
        {
            if (settings.ContainsKey(SamplePropertySettingKeys.RgbColorSystem))
            {
                var value = settings[SamplePropertySettingKeys.RgbColorSystem];
                // 如果是字符串表示是标准的色空间名
                if (value is string rgbColorSystemName)
                {
                    var filed = typeof(RgbColorSystems).GetField(rgbColorSystemName);
                    if (filed == null)
                    {
                        //throw new ArgumentException($"Unknown rgb color system '{rgbColorSystemName}'");
                        return null;
                    }

                    return (RgbColorSystem)filed.GetValue(null);
                }

                if (value is RgbColorSystem rgbColorSystem)
                {
                    return rgbColorSystem;
                }

            }

            return null;
        }

        public static double GetRgbGamma(IDictionary<string, object> settings)
        {
            if (settings.ContainsKey(SamplePropertySettingKeys.RgbGamma))
            {
                return Convert.ToDouble(settings[SamplePropertySettingKeys.RgbGamma]);
            }

            return 1.0;
        }

        public static void GetDE94Factors(IDictionary<string, object> settings, out double KL, out double KC, out double KH)
        {
            ICIEDE1994Factors factors = settings.ContainsKey(SamplePropertySettingKeys.CIEDE94Factors)
                ? (ICIEDE1994Factors)settings[SamplePropertySettingKeys.CIEDE94Factors]
                : null;

            KL = settings.ContainsKey(SamplePropertySettingKeys.CIEDE94FactorL)
                ? Convert.ToDouble(settings[SamplePropertySettingKeys.CIEDE94FactorL])
                : factors?.L ?? 1.0;
            KC = settings.ContainsKey(SamplePropertySettingKeys.CIEDE94FactorC)
                ? Convert.ToDouble(settings[SamplePropertySettingKeys.CIEDE94FactorC])
                : factors?.C ?? 1.0;
            KH = settings.ContainsKey(SamplePropertySettingKeys.CIEDE94FactorH)
                ? Convert.ToDouble(settings[SamplePropertySettingKeys.CIEDE94FactorH])
                : factors?.H ?? 1.0;
        }

        public static void GetDE00Factors(IDictionary<string, object> settings, out double KL, out double KC, out double KH)
        {
            ICIEDE2000Factors factors = settings.ContainsKey(SamplePropertySettingKeys.CIEDE00Factors)
                ? (ICIEDE2000Factors)settings[SamplePropertySettingKeys.CIEDE00Factors]
                : null;

            KL = settings.ContainsKey(SamplePropertySettingKeys.CIEDE00FactorL)
                ? Convert.ToDouble(settings[SamplePropertySettingKeys.CIEDE00FactorL])
                : factors?.L ?? 1.0;
            KC = settings.ContainsKey(SamplePropertySettingKeys.CIEDE00FactorC)
                ? Convert.ToDouble(settings[SamplePropertySettingKeys.CIEDE00FactorC])
                : factors?.C ?? 1.0;
            KH = settings.ContainsKey(SamplePropertySettingKeys.CIEDE00FactorH)
                ? Convert.ToDouble(settings[SamplePropertySettingKeys.CIEDE00FactorH])
                : factors?.H ?? 1.0;
        }

        public static void GetCMCDEFactors(IDictionary<string, object> settings, out double KL, out double KC)
        {
            ICMCFactors factors = settings.ContainsKey(SamplePropertySettingKeys.CMCDEFactors)
                ? (ICMCFactors)settings[SamplePropertySettingKeys.CMCDEFactors]
                : null;

            KL = settings.ContainsKey(SamplePropertySettingKeys.CMCDEFactorL)
                ? Convert.ToDouble(settings[SamplePropertySettingKeys.CMCDEFactorL])
                : factors?.L ?? 2.0;
            KC = settings.ContainsKey(SamplePropertySettingKeys.CMCDEFactorC)
                ? Convert.ToDouble(settings[SamplePropertySettingKeys.CMCDEFactorC])
                : factors?.C ?? 1.0;
        }


        public static ColorOffset CalcutelateColorOffset(SamplePropertyInfo property, IDictionary<string, object> settings, IStandard standard, ISample sample)
        {
            var targetLab = standard?.GetLab(settings);
            var trialLab = sample?.GetLab(settings);
            if (targetLab != null && trialLab != null)
            {
                switch (property.Name)
                {
                    case nameof(ColorDiff_CIELAB_L):
                    case nameof(ColorDiff_HunterLab_L):
                    case nameof(ColorDiff_DINLab99_L):
                        return ColorAlgorithm.CalculateDeltaLColorOffset(trialLab.L - targetLab.L);
                    case nameof(ColorDiff_CIELAB_a):
                    case nameof(ColorDiff_HunterLab_a):
                    case nameof(ColorDiff_DINLab99_a):
                        return ColorAlgorithm.CalculateDeltaAColorOffset(trialLab, trialLab.a - targetLab.a);
                    case nameof(ColorDiff_CIELAB_b):
                    case nameof(ColorDiff_HunterLab_b):
                    case nameof(ColorDiff_DINLab99_b):
                        return ColorAlgorithm.CalculateDeltaBColorOffset(trialLab, trialLab.b - targetLab.b);
                }
            }

            return ColorOffset.None;
        }
        #endregion

    }
}
