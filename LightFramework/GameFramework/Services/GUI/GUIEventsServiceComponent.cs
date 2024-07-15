using GameFramework.Components.UI.GUIEvent;
using GameFramework.Input;
using Microsoft.Xna.Framework;
using System.Linq;

namespace GameFramework.Services.GUI
{
    internal class GUIEventsServiceComponent : GameComponent
    {
        private readonly IGame _game;

        public GUIEventsServiceComponent(IGame game) : base((Game)game)
        {
            _game = game;
        }

        public override void Update(GameTime gameTime)
        {
            var drawables = _game.Components
                                 .Where(c => c is GUIEventComponent)
                                 .Select(c => c as GUIEventComponent)
                                 .ToList();
            MouseInputState state = _game.GetMouseState();

            for (int i = drawables.Count - 1; i >= 0; i--)
            {
                var drawable = drawables[i];

                // Check if the component has gained or lost focus
                if (!drawable.HasFocus && drawable.Contains(state.Point))
                {
                    drawable.GiveFocus();
                }
                else if (drawable.HasFocus && !drawable.Contains(state.Point))
                {
                    drawable.LoseFocus();
                }

                if (state.LeftClick || state.RightClick || state.MiddleClick)
                {
                    drawable.Clicked(state.LeftClick, state.RightClick, state.MiddleClick);
                }
            }

            base.Update(gameTime);
        }
    }
}
