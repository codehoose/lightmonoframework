using System;
using System.Globalization;

namespace GameFramework.Graphics.Palette
{
    internal static class StringFormatExtensions
    {
        public static Tuple<int, int, int, int> ToRGBA(this string s)
        {
            int alpha = int.Parse(s.Substring(0, 2), NumberStyles.HexNumber);
            int red = int.Parse(s.Substring(2, 2), NumberStyles.HexNumber);
            int green = int.Parse(s.Substring(4, 2), NumberStyles.HexNumber);
            int blue = int.Parse(s.Substring(6, 2), NumberStyles.HexNumber);

            return new Tuple<int, int, int, int>(red, green, blue, alpha);
        }
    }
}
