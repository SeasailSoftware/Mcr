using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeNH.Color;
using ThreeNH.Color.Algorithm;
using ThreeNH.Color.Algorithm.Tristimulus;
using ThreeNH.Color.Enums;
using ThreeNH.Data.Implement.SampleProperty;

namespace ThreeNH.Data.Implement
{
    public static class Extensions
    {

        public static ITolerance CreateDefaultTolerance(this DeltaEMethod deltaEMethod)
        {
            var tolerance = new Tolerance();
            switch (deltaEMethod)
            {
                case DeltaEMethod.CIEDE1976:
                    tolerance.Items.Add(nameof(SampleProperties.ColorDiffFormula_DE1976), new ToleranceItem(0, 1));
                    break;
                case DeltaEMethod.CIEDE1994:
                    tolerance.Items.Add(nameof(SampleProperties.ColorDiffFormula_DE1994), new ToleranceItem(0, 1));
                    tolerance.Factors.Add(SamplePropertySettingKeys.CIEDE94FactorL, 1.0);
                    tolerance.Factors.Add(SamplePropertySettingKeys.CIEDE94FactorC, 1.0);
                    tolerance.Factors.Add(SamplePropertySettingKeys.CIEDE94FactorH, 1.0);
                    break;
                case DeltaEMethod.CIEDE2000:
                    tolerance.Items.Add(nameof(SampleProperties.ColorDiffFormula_DE2000), new ToleranceItem(0, 1));
                    tolerance.Factors.Add(SamplePropertySettingKeys.CIEDE00FactorL, 1.0);
                    tolerance.Factors.Add(SamplePropertySettingKeys.CIEDE00FactorC, 1.0);
                    tolerance.Factors.Add(SamplePropertySettingKeys.CIEDE00FactorH, 1.0);
                    break;
                case DeltaEMethod.DECMC:
                    tolerance.Items.Add(nameof(SampleProperties.ColorDiffFormula_DECMC), new ToleranceItem(0, 1));
                    tolerance.Factors.Add(SamplePropertySettingKeys.CMCDEFactorL, 1.0);
                    tolerance.Factors.Add(SamplePropertySettingKeys.CMCDEFactorC, 1.0);
                    break;
                case DeltaEMethod.DEHUNTER:
                    tolerance.Items.Add(nameof(SampleProperties.ColorDiffFormula_DEHunter), new ToleranceItem(0, 1));
                    break;
                case DeltaEMethod.DEDIN99:
                    tolerance.Items.Add(nameof(SampleProperties.ColorDiffFormula_DEDIN99), new ToleranceItem(0, 1));
                    break;
                case DeltaEMethod.DEUV:
                    tolerance.Items.Add(nameof(SampleProperties.ColorDiffFormula_DEDIN99), new ToleranceItem(0, 1));
                    break;
            }
            return tolerance;
        }

        public static IEnumerable<SamplePropertyInfo> GetColorSamplePropertyInfoes(this ColorSpace colorSpace)
        {
            var list = new List<SamplePropertyInfo>();
            switch (colorSpace)
            {
                case ColorSpace.CIE_XYZ:
                    list.Add(SampleProperties.Color_CIEXYZ_X());
                    list.Add(SampleProperties.Color_CIEXYZ_Y());
                    list.Add(SampleProperties.Color_CIEXYZ_Z());
                    break;
                case ColorSpace.CIE_LAB:
                    list.Add(SampleProperties.Color_CIELAB_L());
                    list.Add(SampleProperties.Color_CIELAB_a());
                    list.Add(SampleProperties.Color_CIELAB_b());
                    break;
                case ColorSpace.CIE_LUV:
                    list.Add(SampleProperties.Color_Luv1976_L());
                    list.Add(SampleProperties.Color_Luv1976_u());
                    list.Add(SampleProperties.Color_Luv1976_v());
                    break;
                case ColorSpace.CIE_LCH:
                    list.Add(SampleProperties.Color_CIELAB_L());
                    list.Add(SampleProperties.Color_CIELCH_C());
                    list.Add(SampleProperties.Color_CIELCH_h());
                    break;
                case ColorSpace.CIE_Yxy:
                    list.Add(SampleProperties.Color_CIEXYZ_Y());
                    list.Add(SampleProperties.Color_CIEYxy_x());
                    list.Add(SampleProperties.Color_CIEYxy_y());
                    break;
                case ColorSpace.BETA_xy:
                    list.Add(SampleProperties.Color_BETAxy_BETA());
                    list.Add(SampleProperties.Color_BETAxy_x());
                    list.Add(SampleProperties.Color_BETAxy_y());
                    break;
                case ColorSpace.HUNTER_LAB:
                    list.Add(SampleProperties.Color_HunterLab_L());
                    list.Add(SampleProperties.Color_HunterLab_a());
                    list.Add(SampleProperties.Color_HunterLab_b());
                    break;
                case ColorSpace.sRGB:
                    list.Add(SampleProperties.Color_RGB_R());
                    list.Add(SampleProperties.Color_RGB_G());
                    list.Add(SampleProperties.Color_RGB_B());
                    break;
                case ColorSpace.DIN99_LAB:
                    list.Add(SampleProperties.Color_DINLab99_L());
                    list.Add(SampleProperties.Color_DINLab99_a());
                    list.Add(SampleProperties.Color_DINLab99_b());
                    break;
                case ColorSpace.Munsell:
                    list.Add(SampleProperties.Color_Munsell_H());
                    list.Add(SampleProperties.Color_Munsell_V());
                    list.Add(SampleProperties.Color_Munsell_C());
                    break;
            }
            return list;
        }

        public static IEnumerable<SamplePropertyInfo> GetColorDifferanceSamplePropertyInfoes(this ColorSpace colorSpace)
        {
            var list = new List<SamplePropertyInfo>();
            switch (colorSpace)
            {
                case ColorSpace.CIE_XYZ:
                    list.Add(SampleProperties.ColorDiff_CIEXYZ_X());
                    list.Add(SampleProperties.ColorDiff_CIEXYZ_Y());
                    list.Add(SampleProperties.ColorDiff_CIEXYZ_Z());
                    break;
                case ColorSpace.CIE_LAB:
                    list.Add(SampleProperties.ColorDiff_CIELAB_L());
                    list.Add(SampleProperties.ColorDiff_CIELAB_a());
                    list.Add(SampleProperties.ColorDiff_CIELAB_b());
                    break;
                case ColorSpace.CIE_LUV:
                    list.Add(SampleProperties.ColorDiff_CIELuv_L());
                    list.Add(SampleProperties.ColorDiff_CIELuv_u());
                    list.Add(SampleProperties.ColorDiff_CIELuv_v());
                    break;
                case ColorSpace.CIE_LCH:
                    list.Add(SampleProperties.ColorDiff_CIELCH_L());
                    list.Add(SampleProperties.ColorDiff_CIELCH_C());
                    list.Add(SampleProperties.ColorDiff_CIELCH_H());
                    break;
                case ColorSpace.CIE_Yxy:
                    list.Add(SampleProperties.ColorDiff_CIEYxy_Y());
                    list.Add(SampleProperties.ColorDiff_CIEYxy_x());
                    list.Add(SampleProperties.ColorDiff_CIEYxy_y());
                    break;
                case ColorSpace.BETA_xy:
                    list.Add(SampleProperties.ColorDiff_BETAxy_BETA());
                    list.Add(SampleProperties.ColorDiff_BETAxy_x());
                    list.Add(SampleProperties.ColorDiff_BETAxy_y());
                    break;
                case ColorSpace.HUNTER_LAB:
                    list.Add(SampleProperties.ColorDiff_HunterLab_L());
                    list.Add(SampleProperties.ColorDiff_HunterLab_a());
                    list.Add(SampleProperties.ColorDiff_HunterLab_b());
                    break;
                case ColorSpace.sRGB:
                    list.Add(SampleProperties.ColorDiff_RGB_R());
                    list.Add(SampleProperties.ColorDiff_RGB_G());
                    list.Add(SampleProperties.ColorDiff_RGB_B());
                    break;
                case ColorSpace.DIN99_LAB:
                    list.Add(SampleProperties.ColorDiff_DINLab99_L());
                    list.Add(SampleProperties.ColorDiff_DINLab99_a());
                    list.Add(SampleProperties.ColorDiff_DINLab99_b());
                    break;
                    //case ColorSpace.Munsell:
                    //    list.Add(SampleProperties.ColorDiff_Munsell_H());
                    //    list.Add(SampleProperties.ColorDiff_Munsell_V());
                    //    list.Add(SampleProperties.ColorDiff_Munsell_C());
                    //    break;
            }
            return list;
        }



        public static SamplePropertyInfo GetDeltaEProperty(this DeltaEMethod deltaEMethod)
        {
            switch (deltaEMethod)
            {
                case DeltaEMethod.CIEDE1976:
                    return SampleProperties.ColorDiffFormula_DE1976();
                case DeltaEMethod.CIEDE1994:
                    return SampleProperties.ColorDiffFormula_DE1994();
                case DeltaEMethod.CIEDE2000:
                    return SampleProperties.ColorDiffFormula_DE2000();
                case DeltaEMethod.DEHUNTER:
                    return SampleProperties.ColorDiffFormula_DEHunter();
                case DeltaEMethod.DECMC:
                    return SampleProperties.ColorDiffFormula_DECMC();
                case DeltaEMethod.DEDIN99:
                    return SampleProperties.ColorDiffFormula_DEDIN99();
                default:
                    throw new ArgumentNullException(nameof(deltaEMethod));
            }
        }

        public static IEnumerable<SamplePropertyInfo> GetSampleProperties(this ColorIndex colorIndex)
        {
            var list = new List<SamplePropertyInfo>();
            switch (colorIndex)
            {
                case ColorIndex.Yellowness:
                    list.Add(SampleProperties.ColorIndex_ASTM1925_1970_YI());
                    list.Add(SampleProperties.ColorIndex_AstmE313_YI());
                    break;
                case ColorIndex.Whiteness:
                    list.Add(SampleProperties.ColorIndex_AstmE313_WI());
                    list.Add(SampleProperties.ColorIndex_CIEISO_WI());
                    list.Add(SampleProperties.ColorIndex_Hunter1942_WI());
                    list.Add(SampleProperties.ColorIndex_Hunter1960_WI());
                    list.Add(SampleProperties.ColorIndex_R457_WI());
                    break;
            }
            return list;
        }

    }
}
