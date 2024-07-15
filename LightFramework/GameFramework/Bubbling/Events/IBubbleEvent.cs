using System;

namespace GameFramework.Bubbling.Events
{
    public interface IBubbleEvent
    {
        object Sender { get; }
        EventArgs EventArgs { get; }
    }
}
