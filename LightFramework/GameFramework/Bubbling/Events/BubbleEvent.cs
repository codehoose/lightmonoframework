using System;

namespace GameFramework.Bubbling.Events
{
    public class BubbleEvent : IBubbleEvent
    {
        public object Sender { get; }
        public EventArgs EventArgs { get; }

        public BubbleEvent(object sender, EventArgs e)
        {
            Sender = sender;
            EventArgs = e;
        }
    }
}