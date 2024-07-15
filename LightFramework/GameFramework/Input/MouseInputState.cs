using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameFramework.Input
{
    public struct MouseInputState
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ButtonState LeftButton { get; set; }
        public ButtonState RightButton { get; set; }
        public ButtonState MiddleButton { get; set; }
        public bool LeftClick { get; set; }
        public bool RightClick { get; set; }
        public bool MiddleClick { get; set; }
        public Point Point => Position.ToPoint();
        public Vector2 Position { get; set; }
    }
}
