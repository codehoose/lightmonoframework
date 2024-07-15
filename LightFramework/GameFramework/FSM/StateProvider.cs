using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFramework.FSM
{
    public class StateProvider : IStateProvider
    {
        private bool _disposed;
        private readonly IGame _game;
        private List<IState> _states;

        public StateProvider(IGame game)
        {
            _game = game;
            _states = new List<IState>();
        }

        public T Get<T>() where T : IState
        {
            var state = _states.FirstOrDefault(s => s is T);
            if (state != null) return (T)state;

            var newState = (T)Activator.CreateInstance(typeof(T), _game);
            _states.Add(newState);
            return newState;
        }

        public virtual void Dispose()
        {
            if (_disposed) return;
            _states.Clear();
            _disposed = true;
        }
    }
}
