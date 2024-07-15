using GameFramework.Input.Extensions;
using Microsoft.Xna.Framework.Input;

namespace GameFramework.Input
{
    internal class KeyboardStateExtended
    {
        private bool _firstTime = true;
        public KeyboardState Previous { get; private set; }
        public KeyboardState Current { get; private set; }

        public char GetKey()
        {
            if (Current.TryConvertKeyboardInput(Previous, out char key))
            {
                return key;
            }

            return (char)0;
        }

        public void Update()
        {
            if (_firstTime)
            {
                _firstTime = false;
                Previous = Keyboard.GetState();
                Current = Keyboard.GetState();
            }

            Previous = Current;
            Current = Keyboard.GetState();
        }
    }
}
