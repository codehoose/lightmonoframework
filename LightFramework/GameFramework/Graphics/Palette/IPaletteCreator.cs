using Microsoft.Xna.Framework;

namespace GameFramework.Graphics.Palette
{
    public interface IPaletteCreator
    {
        Color[] GetPalette(string filename = "");
        int BackgroundColor { get; set; }
    }
}