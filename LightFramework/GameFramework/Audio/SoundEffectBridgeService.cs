using GameFramework.Bubbling.Events;
using GameFramework.Services;
using System;
using System.Collections.Generic;

namespace GameFramework.Audio
{
    public class SoundEffectBridgeService : BaseGameService, IBubbleMessageRecipient
    {
        public readonly Dictionary<Type, string> _map = new Dictionary<Type, string>();

        public SoundEffectBridgeService(IGame game) : base(game)
        {
            Game.MessageQueue.RegisterInterest(this, typeof(ClickBubbleEvent));
            Game.MessageQueue.RegisterInterest(this, typeof(ItemSelectedBubbleEvent));
        }

        public void EventFired(IBubbleEvent bubbleEvent)
        {
            if (_map.TryGetValue(bubbleEvent.GetType(), out var fxName))
            {
                Game.MessageQueue.BubbleEvent(new PlayEffectEvent(bubbleEvent.Sender, new PlayEffectEventArgs(fxName)));
            }
        }

        public void Register<TType>(string fxName) where TType : IBubbleEvent
        {
            if (_map.ContainsKey(typeof(TType)))
            {
                _map[typeof(TType)] = fxName;
            }
            else
            {
                _map.Add(typeof(TType), fxName);
            }
        }
    }
}
