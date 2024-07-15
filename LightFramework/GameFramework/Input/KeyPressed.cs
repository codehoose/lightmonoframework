using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameFramework.Input
{
    public class KeyPressed : IKeyPressed
    {
        enum KeyPressState
        {
            None,
            Up,
            Down
        }

        private readonly Keys _key;
        private readonly bool _activateOnKeyDown;
        private KeyPressState _state;
        private int _count;

        public event EventHandler Activated;

        public KeyPressed(Keys key, bool activateOnKeyDown)
        {
            _key = key;
            _activateOnKeyDown = activateOnKeyDown;
            _state = KeyPressState.None;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            bool isKeydown = state.IsKeyDown(_key);
            bool isKeyup = state.IsKeyUp(_key);
            bool changed = (isKeydown && _state != KeyPressState.Down) || (isKeyup && _state != KeyPressState.Up);

            _count += gameTime.ElapsedGameTime.Milliseconds;
            if (_count < 250 && !changed)
                return;

            _count -= 250;

            if (isKeydown)
            {
                DoKeyDown();
            }
            else if (isKeyup)
            {
                DoKeyUp();
            }
        }

        private void DoKeyUp()
        {
            if (!_activateOnKeyDown)
                Activated(this, EventArgs.Empty);
            _state = KeyPressState.Up;
        }

        private void DoKeyDown()
        {
            if (_activateOnKeyDown)
            {
                Activated.Invoke(this, EventArgs.Empty);
            }
            _state = KeyPressState.Down;
        }
    }
}
