namespace GameFramework.FSM.Generic
{
    public class StateMachine<T> : IStateMachine
    {
        private readonly T _parent;
        private IStateProvider _provider;
        private IState _currentState;

        public IState CurrentState => _currentState;

        public T Parent => _parent;

        public StateMachine(T parent, IStateProvider provider)
        {
            _parent = parent;
            _provider = provider;
        }


        public void ChangeState<TState>() where TState : IState
        {
            var state = _provider.Get<TState>();
            ChangeState(state);
        }

        public bool IsCurrentState<TState>() where TState : IState
        {
            return _currentState != null && _currentState is TState;
        }

        public void ChangeState(IState state)
        {
            if (_currentState != null)
            {
                _currentState.OnExit();
            }

            _currentState = state;
            _currentState.OnEnter();
        }
    }
}
