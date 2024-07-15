using GameFramework.Components.UI.GUIEvent;
using GameFramework.General.Linq;
using System;

namespace GameFramework.Components.UI
{
    public class MouseHitComponent : GUIEventComponent
    {
        public event EventHandler LeftClick;
        public event EventHandler RightClick;
        public event EventHandler MiddleClick;

        public event EventHandler MouseEntered;
        public event EventHandler MouseExited;

        public MouseHitComponent(IGame game, BaseComponent parent = null) : base(game, parent) { }

        public override void GiveFocus()
        {
            if (!Game.IsActive) return;
            HasFocus = true;
            MouseEntered?.Invoke(this, new EventArgs());
        }

        public override void LoseFocus()
        {
            if (!Game.IsActive) return;
            HasFocus = false;
            MouseExited?.Invoke(this, new EventArgs());
        }

        public override MouseEventResult Clicked(bool left, bool right, bool middle)
        {
            if (!Game.IsActive)
            {
                return MouseEventResult.Continue;
            }

            if (HasFocus)
            {
                if (left)
                {
                    LeftClick?.Invoke(this, new EventArgs());
                }
                else if (right)
                {
                    RightClick?.Invoke(this, new EventArgs());
                }
                else if (middle)
                {
                    MiddleClick?.Invoke(this, new EventArgs());
                }

                return MouseEventResult.Stop;
            }

            return MouseEventResult.Continue;
        }

        protected override void Dispose(bool disposing)
        {
            LeftClick?.GetInvocationList()
                      .ForEach(e => LeftClick -= (EventHandler)e);

            RightClick?.GetInvocationList()
                       .ForEach(e => RightClick -= (EventHandler)e);

            MiddleClick?.GetInvocationList()
                        .ForEach(e => MiddleClick -= (EventHandler)e);

            base.Dispose(disposing);
        }
    }
}
