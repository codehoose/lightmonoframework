namespace GameFramework.FSM
{
    public interface IState
    {
        void OnEnter();
        void OnExit();
    }
}
