using System;

namespace GameFramework.Bubbling.Events
{
    public class PlayEffectEvent : BubbleEvent
    {
        public string FxName { get; private set; }

        public PlayEffectEvent(object sender, PlayEffectEventArgs e) : base(sender, e)
        {
            FxName = e.FxName;
        }
    }

    public class PlayEffectEventArgs : EventArgs
    {
        public string FxName { get; }

        public PlayEffectEventArgs(string fxName)
        {
            FxName = fxName;
        }
    }
}
