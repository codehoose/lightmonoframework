namespace GameFramework.FSM
{
    public abstract class BaseState : IState
    {
        private readonly IGame _game;

        public IGame Game => _game;

        public BaseState(IGame game)
        {
            _game = game;
        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
        {

        }
    }
}
