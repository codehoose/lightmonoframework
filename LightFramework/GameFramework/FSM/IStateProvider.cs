namespace GameFramework.FSM
{
    public interface IStateProvider
    {
        T Get<T>() where T : IState;

        void Dispose();
    }
}
