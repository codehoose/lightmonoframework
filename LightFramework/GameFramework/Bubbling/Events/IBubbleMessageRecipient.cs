namespace GameFramework.Bubbling.Events
{
    public interface IBubbleMessageRecipient
    {
        void EventFired(IBubbleEvent bubbleEvent);
    }
}