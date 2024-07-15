namespace GameFramework.FSM
{
    public interface IStateMachine
    {
        public IState CurrentState { get; }

        void ChangeState(IState state);

        void ChangeState<T>() where T : IState;
    }
}
