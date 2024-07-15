using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFramework.Components.Graphics
{
    public class StaticImageComponent : BaseImageComponent
    {
        private Texture2D _texture;
        private string _textureName;

        public string TextureName
        {
            get { return _textureName; }
            set
            {
                if (_textureName == value && _texture != null) return;

                _textureName = value;
                if (_texture != null)
                {
                    _texture.Dispose();
                    _texture = null;
                }
                _texture = Game.Content.Load<Texture2D>(_textureName);
                Size = _texture.Bounds.Size;
                Color = Color.White;
            }
        }

        public StaticImageComponent(IGame game, Components.BaseComponent parent = null, string textureName = null) : base(game, parent)
        {
            if (!string.IsNullOrEmpty(textureName))
            {
                TextureName = textureName;
            }
        }

        public StaticImageComponent(IGame game, Components.BaseComponent parent = null) : base(game, parent)
        {

        }

        public StaticImageComponent(IGame game, Components.BaseComponent parent = null, Color color = new Color(), Point size = new Point()) : base(game, parent)
        {
            if (size.X > 0)
            {
                MakeSolidColor(color, size);
            }
        }

        public void MakeSolidColor(Color color, Point size)
        {
            _textureName = "_singleColour";
            _texture = new Texture2D(Game.GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.White });
            Color = color;
            Size = size;
        }

        protected override void DrawImage(GameTime gameTime)
        {
            var pos = GetWorldPosition(PivotPoint).ToPoint();
            var dest = new Rectangle(pos, Size);
            Game.SpriteBatch.Draw(_texture, dest, Color);
        }
    }
}
