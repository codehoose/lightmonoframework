using GameFramework.Graphics.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameFramework.Components.Graphics
{
    public class TiledBackgroundComponent : BaseImageComponent
    {
        private Vector2 _drift = Vector2.Zero;
        private double _msCount;
        private float _driftSpeed = 0f;
        private readonly Texture2D _texture;

        public float DriftSpeedSec
        {
            get => _driftSpeed;
            set
            {
                _driftSpeed = Math.Max(0, value);
            }
        }

        public TiledBackgroundComponent(IGame game, BaseComponent parent = null, string textureName = "") : base(game, parent)
        {
            _texture = Game.Content.Load<Texture2D>(textureName);
        }

        protected override void DrawImage(GameTime gameTime)
        {
            Point screenSize = Game.GraphicsDevice.Viewport.Bounds.Size;
            int cols = (screenSize.X / _texture.Width) + 1;
            int rows = (screenSize.Y/_texture.Height) + 1;

            _msCount += DriftSpeedSec > 0 ? gameTime.ElapsedGameTime.TotalMilliseconds / DriftSpeedSec : 0;
            if (_msCount > 1000)
            {
                _msCount -= 1000;
            }

            float time = (float)(_msCount / 1000f);
            _drift = Vector2.Lerp(_texture.GetSize().ToVector2(), Vector2.Zero, time);

            for (int y = -1; y < rows; y++)
            {
                for (int x = -1; x < cols; x++)
                {
                    var pos = new Vector2(x * _texture.Width, y * _texture.Height).ToPoint();
                    var dest = new Rectangle(pos, _texture.GetSize());
                    var src = new Rectangle(_drift.ToPoint(), _texture.GetSize());

                    Game.SpriteBatch
                        .Draw(_texture, dest, src, Color, 0f, Vector2.Zero, SpriteEffects.None, 0);
                }
            }
        }
    }
}
