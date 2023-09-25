using System.Diagnostics.CodeAnalysis;
using ThreeNH.Color.Algorithm;
using ThreeNH.Color.Enums;
using ThreeNH.Color.Model;
using ThreeNH.Data.Implement.SampleProperty;

namespace ThreeNH.Data.Implement
{

    /// <summary>
    /// 颜色数据
    /// 注意：如果使用光谱数据，不要在使用期间更新引用反射率数据，
    /// 否则会导致XYZ和Lab的值与光谱表示的颜色不一致
    /// </summary>
    public class ColorData : IColorData
    {
        public CIEXYZ Xyz { get; set; }
        public CIELab Lab { get; set; }
        public ISpectralData SpectralData { get; set; } = null;
        public sRGB PseudoColor { get; set; }
        public bool TransmissionFlag { get; set; }

        public StandardIlluminant Illuminant { get; set; }

        public StandardObserver Observer { get; set; }

        public ColorData()
        {

        }
        public ColorData(CIEXYZ xyz,
            StandardIlluminant illuminant = StandardIlluminant.D65,
            StandardObserver observer = StandardObserver.CIE1964,
            bool transmissionFlag = false)
        {
            TransmissionFlag = transmissionFlag;
            Xyz = xyz;
            Lab = Xyz.ToCIELab(illuminant, observer);
            PseudoColor = Lab.TosRGB();
        }

        public ColorData(CIELab lab, StandardIlluminant illuminant = StandardIlluminant.D65,
            StandardObserver observer = StandardObserver.CIE1964,
            bool transmissionFlag = false)
        {
            TransmissionFlag = transmissionFlag;
            Lab = lab;
            Xyz = Lab.ToCIEXYZ(illuminant, observer);
            PseudoColor = Lab.TosRGB();
        }

        public ColorData(ISpectralData spectrum, StandardIlluminant illuminant = StandardIlluminant.D65,
            StandardObserver observer = StandardObserver.CIE1964,
            bool transmissionFlag = false)
        {
            TransmissionFlag = transmissionFlag;
            Xyz = spectrum.ToSpectrum().ToCIEXYZ(illuminant, observer);
            Lab = Xyz.ToCIELab();
            PseudoColor = Lab.TosRGB(illuminant, observer);
            SpectralData = spectrum;
        }

        public ColorData( IColorData source, bool deepCopy = true)
        {
            if (deepCopy)
            {
                DeepCopyFrom(source);
            }
            else
            {
                ShallowCopyFrom(source);
            }
        }

        public void DeepCopyFrom( IColorData source)
        {
            if (source == this) return;

            TransmissionFlag = source.TransmissionFlag;
            Xyz = new CIEXYZ(source.Xyz.X, source.Xyz.Y, source.Xyz.Z);
            Lab = new CIELab(source.Lab.L, source.Lab.a, source.Lab.b);
            PseudoColor = new sRGB(source.PseudoColor.R, source.PseudoColor.G, source.PseudoColor.B);
            if (source.SpectralData != null)
            {
                SpectralData = new SpectralData(source.SpectralData);
            }
        }

        public void ShallowCopyFrom( IColorData source)
        {
            if (source == this) return;

            TransmissionFlag = source.TransmissionFlag;
            Xyz = source.Xyz;
            Lab = source.Lab;
            PseudoColor = source.PseudoColor;
            SpectralData = source.SpectralData;
        }

        public object Clone()
        {
            return new ColorData(this);
        }

    }

}
