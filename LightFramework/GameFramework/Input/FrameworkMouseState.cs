using Microsoft.Xna.Framework.Input;

namespace GameFramework.Input
{
    internal class FrameworkMouseState : IMouseState
    {
        private readonly ButtonState[] _previousState;
        private readonly IGame _game;

        internal FrameworkMouseState(IGame game)
        {
            _previousState = new ButtonState[3]
            {
                ButtonState.Released,
                ButtonState.Released,
                ButtonState.Released
            };
            _game = game;
        }

        public MouseInputState GetState()
        {
            MouseState state = Mouse.GetState();

            bool leftPressed = _previousState[0] == ButtonState.Pressed && state.LeftButton == ButtonState.Released;
            bool rightPressed = _previousState[1] == ButtonState.Pressed && state.RightButton == ButtonState.Released;
            bool middlePressed = _previousState[2] == ButtonState.Pressed && state.MiddleButton == ButtonState.Released;

            _previousState[0] = state.LeftButton;
            _previousState[1] = state.RightButton;
            _previousState[2] = state.MiddleButton;

            return new MouseInputState
            {
                X = state.X,
                Y = state.Y,
                LeftButton = state.LeftButton,
                MiddleButton = state.MiddleButton,
                RightButton = state.RightButton,
                LeftClick = leftPressed,
                RightClick = rightPressed,
                MiddleClick = middlePressed,
                Position = state.Position.ToVector2() / _game.Scale
            };
        }
    }
}
