using Microsoft.Xna.Framework;

namespace GameFramework.Components.UI.GUIEvent
{
    /// <summary>
    /// Base class for GUI Event components.
    /// </summary>
    public abstract class GUIEventComponent : BaseComponent
    {
        public bool HasFocus { get; protected set; }

        public GUIEventComponent(IGame game, BaseComponent parent = null) : base(game, parent)
        {
        }

        public bool Contains(Point point)
        {
            var rect = new Rectangle(GetWorldPosition().ToPoint(), Size);
            return rect.Contains(point);
        }

        public abstract void GiveFocus();
        public abstract void LoseFocus();
        public abstract MouseEventResult Clicked(bool left, bool right, bool middle);
    }

    /// <summary>
    /// Determine whether processing should continue or stop through the
    /// visual stack
    /// </summary>
    public enum MouseEventResult
    {
        /// <summary>
        /// Continue processing down through the drawable components
        /// </summary>
        Continue,

        /// <summary>
        /// Stop processing down through the visual stack
        /// </summary>
        Stop
    }
}
