using System;

namespace GameFramework.Bubbling.Events
{
    public class ItemSelectedBubbleEvent : BubbleEvent
    {
        public ItemSelectedBubbleEvent(object sender, EventArgs e) : base(sender, e)
        {
        }
    }
}
