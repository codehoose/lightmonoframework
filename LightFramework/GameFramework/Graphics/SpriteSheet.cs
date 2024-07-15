using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFramework.Graphics
{
    public class SpriteSheet
    {
        private Texture2D _texture;
        private int _cellHeight;
        private int _cellWidth;
        private int _columns;
        private int _rows;

        public SpriteSheet(Texture2D texture, int cellHeight, int cellWidth)
        {
            _texture = texture;
            _cellHeight = cellHeight;
            _cellWidth = cellWidth;
            _columns = _texture.Width / _cellWidth;
            _rows = _texture.Height / _cellHeight;
        }

        public void Draw(SpriteBatch spriteBatch, int frame, Vector2 position, Color color)
        {
            int x = frame % _columns;
            int y = frame / _columns;
            Rectangle rect = new Rectangle(x * _cellWidth, y * _cellHeight, _cellWidth, _cellHeight);
            spriteBatch.Draw(_texture, position, rect, color);
        }
    }
}
