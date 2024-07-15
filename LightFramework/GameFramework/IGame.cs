using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameFramework.FSM;
using System.Collections.Generic;
using GameFramework.Input;
using GameFramework.Audio;
using GameFramework.Bubbling.Events;

namespace GameFramework
{
    public interface IGame
    {
        GraphicsDevice GraphicsDevice { get; }
        SpriteBatch SpriteBatch { get; }
        ContentManager Content { get; }
        GameComponentCollection Components { get; }

        IBubbleMessageQueue MessageQueue { get; }
        IMusicService Audio { get; }
        IStateMachine StateMachine { get; }
        IStateProvider StateProvider { get; }
        GameServiceContainer Services { get; }
        Color BackgroundColor { get; set; }

        float Scale { get; set; }
        bool IsActive { get; }

        MouseInputState GetMouseState();

        T GetComponent<T>();

        IList<T> GetComponents<T>();

        void Exit();
    }
}
