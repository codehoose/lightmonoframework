using Microsoft.Xna.Framework;
using System;

namespace GameFramework.Input
{
    public interface IKeyPressed
    {
        event EventHandler Activated;

        void Update(GameTime gameTime);
    }
}
