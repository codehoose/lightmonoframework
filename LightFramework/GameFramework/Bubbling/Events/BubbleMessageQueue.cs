using System;
using System.Collections.Generic;

namespace GameFramework.Bubbling.Events
{
    internal class BubbleMessageQueue : IBubbleMessageQueue
    {
        private Dictionary<Type, List<IBubbleMessageRecipient>> _recipients = new Dictionary<Type, List<IBubbleMessageRecipient>>();

        public void RegisterInterest(IBubbleMessageRecipient recipient, Type type)
        {
            if (_recipients.TryGetValue(type, out var recipients))
            {
                if (!recipients.Contains(recipient)) recipients.Add(recipient);
            }
            else
            {
                _recipients.Add(type, new List<IBubbleMessageRecipient>());
                _recipients[type].Add(recipient);
            }
        }

        public void BubbleEvent(IBubbleEvent bubbleEvent)
        {
            Type type = bubbleEvent.GetType();
            if (_recipients.TryGetValue(type, out var recipients))
            {
                foreach (var recipient in recipients)
                {
                    recipient.EventFired(bubbleEvent);
                }
            }
        }
    }
}
