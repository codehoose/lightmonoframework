using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFramework.Graphics.Extensions
{
    internal static class TextureExtensions
    {
        public static Point GetSize(this Texture2D texture)
        {
            return new Point(texture.Width, texture.Height);
        }

        public static Rectangle GetBounds(this Texture2D texture)
        {
            return new Rectangle(0, 0, texture.Width, texture.Height);
        }
    }
}
