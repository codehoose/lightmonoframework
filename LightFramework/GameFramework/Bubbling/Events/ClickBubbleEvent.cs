using System;

namespace GameFramework.Bubbling.Events
{
    public class ClickBubbleEvent : BubbleEvent
    {
        public ClickBubbleEvent(object sender, EventArgs e): base(sender, e) { }
    }
}
