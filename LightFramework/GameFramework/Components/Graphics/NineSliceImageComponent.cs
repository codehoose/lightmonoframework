using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameFramework.Components.Graphics
{
    public class NineSliceImageComponent : BaseImageComponent
    {
        private Texture2D _texture;
        private Rectangle[] _rectangles;
        private Point EndCapSize { get; set; }

        public NineSliceImageComponent(IGame game, Components.BaseComponent parent = null, string textureName = "", Point size = new Point()) : base(game, parent)
        {
            _texture = Game.Content.Load<Texture2D>(textureName);
            EndCapSize = new Point(32, 32);
            _rectangles = SetupRectangles(_texture.Bounds.Size);
            Size = size;
        }

        private Rectangle[] SetupRectangles(Point size)
        {
            // 9-slice rectangle sources are:
            // ABC
            // DEF
            // GHI
            List<Rectangle> rectangles = new List<Rectangle>
            {
                new Rectangle(0, 0, EndCapSize.X, EndCapSize.Y), // A
                new Rectangle(EndCapSize.X, 0, size.X - EndCapSize.X*2, EndCapSize.Y), // B
                new Rectangle(size.X - EndCapSize.X, 0, EndCapSize.X, EndCapSize.Y), // C
                new Rectangle(0, EndCapSize.Y,EndCapSize.X,EndCapSize.Y), // D
                new Rectangle(EndCapSize.X,EndCapSize.Y,size.X-EndCapSize.X*2, size.Y-EndCapSize.Y*2), // E
                new Rectangle(size.X-EndCapSize.X, EndCapSize.Y,EndCapSize.X,EndCapSize.Y), //F
                new Rectangle(0, size.Y - EndCapSize.Y, EndCapSize.X, EndCapSize.Y), // G
                new Rectangle(EndCapSize.X, size.Y-EndCapSize.Y, size.X - EndCapSize.X*2, EndCapSize.Y), // H
                new Rectangle(size.X - EndCapSize.X, size.Y - EndCapSize.Y, EndCapSize.X, EndCapSize.Y) // I
            };

            return rectangles.ToArray();
        }

        private Point GetPoint(int offX, int offY)
        {
            return GetWorldPosition().ToPoint() + new Point(offX, offY);
        }

        protected override void DrawImage(GameTime gameTime)
        {
            // ABC
            // DEF
            // GHI

            // Four Corners
            Game.SpriteBatch
                .Draw(_texture, new Rectangle(GetPoint(0, 0), EndCapSize), _rectangles[0], Color);
            Game.SpriteBatch
                .Draw(_texture, new Rectangle(GetPoint(Size.X - EndCapSize.X, 0), EndCapSize), _rectangles[2], Color);
            Game.SpriteBatch
                .Draw(_texture, new Rectangle(GetPoint(0, Size.Y - EndCapSize.Y), EndCapSize), _rectangles[6], Color);
            Game.SpriteBatch
                .Draw(_texture, new Rectangle(GetPoint(Size.X - EndCapSize.X, Size.Y - EndCapSize.Y), EndCapSize), _rectangles[8], Color);

            // Middle
            Game.SpriteBatch
                .Draw(_texture, new Rectangle(GetPoint(EndCapSize.X, EndCapSize.Y), Size - EndCapSize - EndCapSize), _rectangles[4], Color);

            // Sides
            Game.SpriteBatch
                .Draw(_texture, new Rectangle(GetPoint(0, EndCapSize.Y), new Point(EndCapSize.X, Size.Y - EndCapSize.Y * 2)), _rectangles[3], Color);
            Game.SpriteBatch
                .Draw(_texture, new Rectangle(GetPoint(Size.X - EndCapSize.X, EndCapSize.Y), new Point(EndCapSize.X, Size.Y - EndCapSize.Y * 2)), _rectangles[5], Color);

            // Top and Bottom
            Game.SpriteBatch
                .Draw(_texture, new Rectangle(GetPoint(EndCapSize.X, 0), new Point(Size.X - EndCapSize.X * 2, EndCapSize.Y)), _rectangles[1], Color);
            Game.SpriteBatch
                .Draw(_texture, new Rectangle(GetPoint(EndCapSize.X, Size.Y - EndCapSize.Y), new Point(Size.X - EndCapSize.X * 2, EndCapSize.Y)), _rectangles[7], Color);
        }
    }
}
