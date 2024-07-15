using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFramework.FSM.Generic
{
    public class StateProvider<T> : IStateProvider
    {
        private bool _disposed;
        private readonly T _parent;
        private List<IState> _states;

        public StateProvider(T game)
        {
            _parent = game;
            _states = new List<IState>();
        }

        public TState Get<TState>() where TState : IState
        {
            var state = _states.FirstOrDefault(s => s is TState);
            if (state != null) return (TState)state;

            var newState = (TState)Activator.CreateInstance(typeof(TState), _parent);
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
