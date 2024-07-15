using System;

namespace GameFramework.Bubbling.Events
{
    public interface IBubbleMessageQueue
    {
        void RegisterInterest(IBubbleMessageRecipient recipient, Type type);

        void BubbleEvent(IBubbleEvent bubbleEvent);
    }
}