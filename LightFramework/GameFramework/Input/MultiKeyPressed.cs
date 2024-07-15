using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFramework.Input
{
    public class MultiKeyPressed : IKeyPressed
    {
        private readonly List<IKeyPressed> _keys = new List<IKeyPressed>();

        public event EventHandler Activated;

        public MultiKeyPressed(params Keys[] keys)
        {
            _keys.AddRange(keys.Select(k => new KeyPressed(k, true)).ToList());
            _keys.ForEach(k => k.Activated += (o, e) => { Activated?.Invoke(this, EventArgs.Empty); });
        }


        public void Update(GameTime gameTime)
        {
            foreach(var key in _keys)
            {
                key.Update(gameTime);
            }
        }
    }
}
