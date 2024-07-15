using GameFramework.Components.UI.GUIEvent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFramework.Components.UI
{
    /// <summary>
    /// Dark overlay that gets displayed when a GUI overlay is shown.
    /// </summary>
    public class DarkOverlayComponent : GUIEventComponent
    {
        private readonly Texture2D _texture;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game instance</param>
        /// <param name="parent">Parent</param>
        public DarkOverlayComponent(IGame game, BaseComponent parent = null) : base(game, parent)
        {
            _texture = new Texture2D(game.GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.Black * 0.5f });
        }

        public override void GiveFocus() => HasFocus = true;

        public override void LoseFocus() => HasFocus = false;

        /// <summary>
        /// Handle mouse click events. 
        /// </summary>
        /// <param name="mouseState">The mouse state</param>
        /// <returns>Returns 'Stop' if the component is visible.</returns>
        public override MouseEventResult Clicked(bool left, bool right, bool middle)
        {
            return Visible ? MouseEventResult.Stop : MouseEventResult.Continue;
        }

        /// <summary>
        /// Draw the dark overlay
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Draw(GameTime gameTime)
        {
            if (!Visible) return;

            int width = Game.GraphicsDevice.Viewport.Width;
            int height = Game.GraphicsDevice.Viewport.Height;

            Game.SpriteBatch
                .Draw(_texture, new Rectangle(0, 0, width, height), Color.White);

            base.Draw(gameTime);
        }
    }
}
