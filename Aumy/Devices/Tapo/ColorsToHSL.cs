using System;

namespace Aumy.Devices.Tapo
{
    public class ColorsToHSL
    {
        public static HSLColor FromRGBToHSL (Byte R, Byte G, Byte B)
        {
            var _R = R / 255f;
            var _G = G / 255f;
            var _B = B / 255f;

            var _Min = Math.Min(Math.Min(_R, _G), _B);
            var _Max = Math.Max(Math.Max(_R, _G), _B);
            var _Delta = _Max - _Min;

            float H = 0;
            float S = 0;
            var L = (_Max + _Min) / 2.0f;

            if (_Delta == 0) return new HSLColor { Hue = H, Saturation = S, Lightness = L };
            if (L < 0.5f)
                S = _Delta / (_Max + _Min);
            else
                S = _Delta / (2.0f - _Max - _Min);


            if (_R == _Max)
                H = (_G - _B) / _Delta;
            else if (_G == _Max)
                H = 2f + (_B - _R) / _Delta;
            else if (_B == _Max)
                H = 4f + (_R - _G) / _Delta;

            H *= 60f;
            if (H < 0) H += 360;

            return new HSLColor { Hue = H, Saturation = S, Lightness = L };
        }
    }

    public class HSLColor
    {
        public float Hue { get; set; }
        public float Saturation { get; set; }
        public float Lightness { get; set; }
    }
}
