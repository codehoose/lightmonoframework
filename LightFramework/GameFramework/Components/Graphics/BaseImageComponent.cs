using Microsoft.Xna.Framework;

namespace GameFramework.Components.Graphics
{
    public abstract class BaseImageComponent : BaseComponent
    {
        private Color _color = Color.White;

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }

        public BaseImageComponent(IGame game, BaseComponent parent = null) : base(game, parent)
        {
        }


        public override void Draw(GameTime gameTime)
        {
            if (!Visible) return;
            DrawImage(gameTime);
        }

        protected abstract void DrawImage(GameTime gameTime);
    }
}
