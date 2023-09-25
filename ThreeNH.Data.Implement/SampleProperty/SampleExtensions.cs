using System;
using System.Collections.Generic;
using ThreeNH.Color.Algorithm;
using ThreeNH.Color.Chromaticity;
using ThreeNH.Color.Enums;
using ThreeNH.Color.Model;

namespace ThreeNH.Data.Implement.SampleProperty
{
    public static class SampleExtensions
    {
        private static int _minWavelength = 360;
        private static int _maxWavelength = 780;

        public static Spectrum ToSpectrum(this ISpectralData spectralData)
        {
            var waveLength = new WavelengthRange(spectralData.WavelengthBegin, spectralData.WavelengthEnd, spectralData.WavelengthInterval);
            return new Spectrum(waveLength, spectralData.Data);
        }


        internal static CIEDE1976 GetDe1976(this ISample sample, ISample target, IDictionary<string, object> settings)
        {
            SampleProperties.GetColorSetting(settings, out var channel, out var illuminant, out var observer);
            return GetDe1976(sample, target, channel, illuminant, observer);
        }

        public static CIEDE1976 GetDe1976(ISample sample, ISample target, string channel, StandardIlluminant illuminant, StandardObserver observer)
        {
            var labSample = sample.GetLab(channel, illuminant, observer);
            var labTarget = target.GetLab(channel, illuminant, observer);
            if (labSample != null && labTarget != null)
                return new CIEDE1976(labSample, labTarget);
            return null;
        }

        internal static DEDIN99 GetDeDIN99(this ISample sample, ISample target, IDictionary<string, object> settings)
        {
            SampleProperties.GetColorSetting(settings, out var channel, out var illuminant, out var observer);
            return GetDeDIN99(sample, target, channel, illuminant, observer);
        }

        public static DEDIN99 GetDeDIN99(this ISample sample, ISample target, string channel, StandardIlluminant illuminant, StandardObserver observer)
        {
            var labSample = sample.GetLab(channel, illuminant, observer);
            var labTarget = target.GetLab(channel, illuminant, observer);
            if (labSample != null && labTarget != null)
                return new DEDIN99(labSample, labTarget);
            return null;
        }

        internal static DeltaHunterLab GetDeHunter(this ISample sample, ISample target, IDictionary<string, object> settings)
        {
            SampleProperties.GetColorSetting(settings, out var channel, out var illuminant, out var observer);
            return GetDeHunter(sample, target, channel, illuminant, observer);
        }

        internal static DeltaLuv GetDeltaLuv(this ISample sample, ISample target, IDictionary<string, object> settings)
        {
            SampleProperties.GetColorSetting(settings, out var channel, out var illuminant, out var observer);
            var trial = sample.GetLuv1976(settings);
            var standard = target.GetLuv1976(settings);
            return new DeltaLuv(trial, standard);
        }



        public static DeltaHunterLab GetDeHunter(this ISample sample, ISample target, string channel, StandardIlluminant illuminant, StandardObserver observer)
        {
            var labSample = sample.GetLab(channel, illuminant, observer);
            var labTarget = target.GetLab(channel, illuminant, observer);
            if (labSample != null && labTarget != null)
            {
                var hunterSample = new HunterLab(labSample.L, labSample.a, labSample.b);
                var hunterTarget = new HunterLab(labTarget.L, labTarget.a, labTarget.b);
                return new DeltaHunterLab(hunterSample, hunterTarget);
            }
            return null;
        }

        internal static CIEDE1994 GetDe1994(this ISample sample, ISample target, IDictionary<string, object> settings)
        {
            SampleProperties.GetColorSetting(settings, out var channel, out var illuminant, out var observer);

            SampleProperties.GetDE94Factors(settings, out var KL, out var KC, out var KH);

            return GetDe1994(sample, target, channel, illuminant, observer, KL, KC, KH);
        }

        public static CIEDE1994 GetDe1994(this ISample sample, ISample target, string channel, StandardIlluminant illuminant, StandardObserver observer, double KL, double KC, double KH)
        {
            var labSample = sample.GetLab(channel, illuminant, observer);
            var labTarget = target.GetLab(channel, illuminant, observer);
            if (labSample != null && labTarget != null)
                return new CIEDE1994(labSample, labTarget, (float)KL, (float)KC, (float)KH);
            return null;
        }

        internal static CIEDE2000 GetDe2000(this ISample sample, ISample target, IDictionary<string, object> settings)
        {
            SampleProperties.GetColorSetting(settings, out var channel, out var illuminant, out var observer);
            SampleProperties.GetDE00Factors(settings, out var KL, out var KC, out var KH);
            return GetDe2000(sample, target, channel, illuminant, observer, KL, KC, KH);
        }

        public static CIEDE2000 GetDe2000(this ISample sample, ISample target, string channel, StandardIlluminant illuminant, StandardObserver observer, double KL, double KC, double KH)
        {
            var labSample = sample.GetLab(channel, illuminant, observer);
            var labTarget = target.GetLab(channel, illuminant, observer);
            if (labSample != null && labTarget != null)
                return new CIEDE2000(labSample, labTarget, (float)KL, (float)KC, (float)KH);
            return null;
        }

        internal static CMCDE GetCmcDe(this ISample sample, ISample target, IDictionary<string, object> settings)
        {
            SampleProperties.GetColorSetting(settings, out var channel, out var illuminant, out var observer);
            SampleProperties.GetCMCDEFactors(settings, out var KL, out var KC);
            return GetCmcDe(sample, target, channel, illuminant, observer, KL, KC);
        }

        public static CMCDE GetCmcDe(this ISample sample, ISample target, string channel, StandardIlluminant illuminant, StandardObserver observer, double KL, double KC)
        {
            var labSample = sample.GetLab(channel, illuminant, observer);
            var labTarget = target.GetLab(channel, illuminant, observer);
            if (labSample != null && labTarget != null)
                return new CMCDE(labSample, labTarget, (float)KL, (float)KC);
            return null;
        }

        public static CIEXYZ ToCIEXYZ(this ISpectralData spectralData, StandardIlluminant illuminant, StandardObserver observer)
        {
            if (spectralData == null) return new CIEXYZ(float.NaN, float.NaN, float.NaN);
            var spectrum = spectralData.ToSpectrum();
            return spectrum.ToCIEXYZ(illuminant, observer);
        }

        public static ISpectralData GetSpectralData(this ISample sample, string channel)//s7_M s6_L
        {
            return sample?.GetColorData(channel)?.SpectralData;
        }

        public static CIEXYZ GetXYZ(this ISample sample, string channel, StandardIlluminant illuminant, StandardObserver observer)
        {
            var clr = sample.GetColorData(channel);
            //if (clr != null && clr.Illuminant == illuminant && clr.Observer == observer)
            //{
            //    return clr.Xyz;
            //}
            var spectra = sample.GetSpectralData(channel);
            return spectra.ToCIEXYZ(illuminant, observer);
        }

        internal static sRGB GetRgb(this ISample sample, IDictionary<string, object> settings)
        {
            SampleProperties.GetColorSetting(settings, out var channel, out var illuminant, out var observer);
            return sample.GetRgb(channel, illuminant, observer,
                SampleProperties.GetRgbColorSystem(settings),
                SampleProperties.GetRgbGamma(settings));
        }

        public static sRGB GetRgb(this ISample sample, string channel, StandardIlluminant illuminant, StandardObserver observer, RgbColorSystem rgbColorSystem, double gamma = 1.0)
        {
            if (sample == null) return null;

            var xyz = sample.GetXYZ(channel, illuminant, observer);
            if (xyz != null && rgbColorSystem != null)
            {
                var tuple = XYZtoRGBConversion.XYZtoNormalizedRGB(rgbColorSystem, xyz.X, xyz.Y, xyz.Z, gamma);
                return new sRGB((byte)tuple.Item1, (byte)tuple.Item2, (byte)tuple.Item3);
            }

            return GetPseudoColor(sample, channel, illuminant, observer);
        }

        public static sRGB GetPseudoColor(this ISample sample, string channel, StandardIlluminant illuminant, StandardObserver observer)
        {
            var lab = sample.GetLab(channel, illuminant, observer);
            if (lab != null)
                return lab.TosRGB();
            return new sRGB();
        }



        public static CIELab GetLab(this ISample sample, string channel, StandardIlluminant illuminant, StandardObserver observer)//s4_L
        {
            var spectra = sample.GetSpectralData(channel);
            if (spectra != null)
                return spectra?.ToCIELAB(illuminant, observer);
            var clr = sample?.GetColorData(channel);
            if (clr != null)
            {
                return clr.Lab;
            }
            return null;
        }

        /// <summary>
        /// 反射率转CIELAB
        /// </summary>
        /// <param name="spectralData"></param>
        /// <param name="illuminant"></param>
        /// <param name="observer"></param>
        /// <returns></returns>
        public static CIELab ToCIELAB(this ISpectralData spectralData, StandardIlluminant illuminant, StandardObserver observer)
        {
            var xyz = spectralData.ToCIEXYZ(illuminant, observer);
            return xyz.ToCIELab();
        }

        internal static CIELab GetLab(this ISample sample, IDictionary<string, object> settings)//s2_L
        {
            SampleProperties.GetColorSetting(settings, out var channel, out var illuminant, out var observer);
            return sample.GetLab(channel, illuminant, observer);
        }

        /// <summary>
        /// 通过配置获取XYZ值
        /// 如果配置包含"ColorSetting"，或包含
        /// "Channel", "Illuminant", "Observer"三项
        /// 如果同时包含则优先使用三项直接设置，找不到才使用ColorSetting中的设置
        /// </summary>
        internal static CIEXYZ GetXYZ(this ISample sample, IDictionary<string, object> settings)
        {
            SampleProperties.GetColorSetting(settings, out var channel, out var illuminant, out var observer);
            return sample.GetXYZ(channel, illuminant, observer);
        }

        internal static HunterLab GetHunterLab(this ISample sample, IDictionary<string, object> settings)
        {
            SampleProperties.GetColorSetting(settings, out var channel, out var illuminant, out var observer);
            return sample.GetHunterLab(channel, illuminant, observer);
        }
        // Hunter Lab
        public static HunterLab GetHunterLab(this ISample sample, string channel, StandardIlluminant illuminant, StandardObserver observer)
        {
            var xyz = sample.GetXYZ(channel, illuminant, observer);
            return xyz.ToHunterLab(illuminant, observer);
        }

        internal static CIELUV1976 GetLuv1976(this ISample sample, IDictionary<string, object> settings)
        {
            SampleProperties.GetColorSetting(settings, out var channel, out var illuminant, out var observer);
            return sample.GetLuv1976(channel, illuminant, observer);
        }

        public static CIELUV1976 GetLuv1976(this ISample sample, string channel, StandardIlluminant illuminant, StandardObserver observer)
        {
            var xyz = sample.GetXYZ(channel, illuminant, observer);
            return xyz.ToCIELUV1976(illuminant, observer);
        }



        /// <summary>
        /// 某波长点的力份
        /// </summary>
        /// <param name="spectralData"></param>
        /// <param name="wavelength"></param>
        /// <param name="isTransmittance"></param>
        /// <returns></returns>
        public static double GetColorValueSWLAt(this ISpectralData spectralData, int wavelength, bool isTransmittance)
        {
            var spectrum = spectralData.ToSpectrum();
            return spectrum.GetColorValueSWLAt(wavelength, isTransmittance);
        }

        public static double GetGardnerIndex(this ISpectralData spectralData, StandardIlluminant illuminant, StandardObserver observer)
        {
            if (!spectralData.TransmissionFlag) return double.NaN;
            if (illuminant != StandardIlluminant.C && observer != StandardObserver.CIE1931) return double.NaN;
            var xyz = spectralData.ToCIEXYZ(illuminant, observer);
            return xyz.CalculateGardneIndex();
        }

        public static double GetPtCoIndex(this ISpectralData spectralData, StandardIlluminant illuminant, StandardObserver observer)
        {
            if (!spectralData.TransmissionFlag) return double.NaN;
            var xyz = spectralData.ToCIEXYZ(illuminant, observer);
            return xyz.CalculatPt_Co_Index();
        }

        public static short GetGloss8(ISpectralData ssciSpectra, ISpectralData ssceSpectra, ISpectralData wsciSpectra, ISpectralData wsceSpectra)
        {
            short data_8_gloss;
            float sample = 0, sample1 = 0, glass = 0, glass1 = 0, data = 0;
            var ssci = ssciSpectra.ToSpectrum();
            sample = ColorAlgorithm.IntegralQuantity(ssci);
            if (sample == 0) return 0;
            var ssce = ssceSpectra.ToSpectrum();
            sample1 = ColorAlgorithm.IntegralQuantity(ssce);
            if (sample1 == 0) return 0;
            sample -= ColorAlgorithm.IntegralQuantity(ssce);

            var wsci = wsciSpectra.ToSpectrum();
            var wsce = wsceSpectra.ToSpectrum();
            glass = ColorAlgorithm.IntegralQuantity(wsci);
            glass1 = ColorAlgorithm.IntegralQuantity(wsce);

            glass -= glass1;
            data = sample / glass * 113;
            data *= 10;
            data_8_gloss = (short)(data / 10 + ((short)data % 10 > 5 ? 1 : 0));
            return data_8_gloss;
        }

        public static double GetMetamerismIndiex(ISpectralData sample, ISpectralData target,
    StandardIlluminant illuminant, StandardObserver observer,
    StandardIlluminant refIlluminant, StandardObserver refObserver)
        {
            var xyz1 = sample.ToCIEXYZ(illuminant, observer);
            var xyz2 = target.ToCIEXYZ(illuminant, observer);
            var refxyz1 = sample.ToCIEXYZ(refIlluminant, refObserver);
            var refxyz2 = target.ToCIEXYZ(refIlluminant, refObserver);
            return ColorAlgorithm.CalculateMetamerismIndexByXYZ(illuminant, observer, xyz1, xyz2, refIlluminant, refObserver, refxyz1, refxyz2);
        }

        /// <summary>
        /// Strength SWL
        /// </summary>
        /// <param name="sample"></param>
        /// <param name="target"></param>
        /// <param name="isTransmittance"></param>
        /// <param name="wavelengthOfMaximumAbsorption"></param>
        /// <returns></returns>
        public static double GetStrengthSWL(ISpectralData target, ISpectralData sample, bool isTransmittance, out int wavelengthOfMaximumAbsorption)
        {
            var sampleSpectrum = sample.ToSpectrum();
            var targetSpectrum = target.ToSpectrum();
            return ColorAlgorithm.CalculateStrengthSwl(targetSpectrum, sampleSpectrum, isTransmittance, out wavelengthOfMaximumAbsorption);
        }

        /// <summary>
        /// Weighted Strength
        /// </summary>
        /// <param name="test"></param>
        /// <param name="standard"></param>
        /// <param name="isTransmittance"></param>
        /// <param name="illuminant"></param>
        /// <param name="observer"></param>
        /// <returns></returns>
        public static double GetWeightedStrength(ISpectralData standard, ISpectralData test, bool isTransmittance, StandardIlluminant illuminant, StandardObserver observer)
        {
            var testSpectrum = test.ToSpectrum();
            var standardSpectrum = standard.ToSpectrum();
            return ColorAlgorithm.GetWeightedStrength(illuminant, observer, standardSpectrum, testSpectrum, isTransmittance);
        }

        /// <summary>
        /// Average Strength
        /// </summary>
        /// <param name="test"></param>
        /// <param name="standard"></param>
        /// <param name="isTransmittance"></param>
        /// <returns></returns>
        public static double GetAverageStrength(ISpectralData standard, ISpectralData test, bool isTransmittance)
        {
            var testSpectrum = test.ToSpectrum();
            var standardSpectrum = standard.ToSpectrum();
            return ColorAlgorithm.CalculateAverageStrength(standardSpectrum, testSpectrum, isTransmittance);
        }

        /// <summary>
        /// Color Value Sum
        /// </summary>
        /// <param name="spectralData"></param>
        /// <param name="isTransmittance"></param>
        /// <returns></returns>
        public static double GetColorValueSum(this ISpectralData spectralData, bool isTransmittance)
        {
            if (spectralData == null) return double.NaN;
            var spectrum = spectralData.ToSpectrum();
            return ColorAlgorithm.CalculateColorValueSum(spectrum, isTransmittance);
        }

        /// <summary>
        /// Color Value SWL
        /// </summary>
        /// <param name="spectralData"></param>
        /// <param name="isTransmittance"></param>
        /// <param name="wavelengthOfMaximumAbsorption"></param>
        /// <returns></returns>
        public static double GetColorValueSWL(this ISpectralData spectralData, bool isTransmittance, out int wavelengthOfMaximumAbsorption)
        {
            if (spectralData == null)
            {
                wavelengthOfMaximumAbsorption = 0;
                return double.NaN;
            }
            var spectrum = spectralData.ToSpectrum();
            return ColorAlgorithm.CalculateColorValueSwl(spectrum, isTransmittance, out wavelengthOfMaximumAbsorption);
        }

        public static double GetR457Whiteness(this ISpectralData spectra)
        {
            var spectrum = spectra.ToSpectrum();
            return ColorAlgorithm.GetR457Whiteness(spectrum);
        }

        /// <summary>
        /// Color Value WSUM
        /// </summary>
        /// <param name="spectralData"></param>
        /// <param name="isTransmittance"></param>
        /// <param name="illuminant"></param>
        /// <param name="observer"></param>
        /// <returns></returns>
        public static double GetColorValueWSUM(this ISpectralData spectralData, bool isTransmittance, StandardIlluminant illuminant, StandardObserver observer)
        {
            var spectrum = spectralData.ToSpectrum();
            return ColorAlgorithm.CalculateColorValueWsum(illuminant, observer, spectrum, isTransmittance);
        }
    }
}
