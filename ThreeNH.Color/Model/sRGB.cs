using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreeNH.Color.Model
{
    public class sRGB
    {
        public sRGB()
        {

        }
        private int _rgb;

        public sRGB(int rgb)
        {
            _rgb = rgb;
        }

        public sRGB(byte R, byte G, byte B)
        {
            this.R = R;
            this.G = G;
            this.B = B;
            _rgb = ((R & 0xFF) << 16) | ((G & 0xFF) << 8) | (B & 0xFF);
        }

        public sRGB(byte a, byte r, byte g, byte b)
        {
            _rgb = (int)((a << 24) | ((r & 0xFF) << 16) | ((g & 0xFF) << 8) | (b & 0xFF));
        }

        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }

        public int ToInteger()
        {
            return _rgb;
        }

        public byte A { get; set; }
    }
}
