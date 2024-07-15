namespace GameFramework.FSM
{
    public class StateMachine : IStateMachine
    {
        private readonly IGame _game;
        private IState _currentState;

        public IState CurrentState => _currentState;

        public StateMachine(IGame game)
        {
            _game = game;
        }

        public void ChangeState<T>() where T : IState
        {
            var state = _game.StateProvider.Get<T>();
            ChangeState(state);
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
