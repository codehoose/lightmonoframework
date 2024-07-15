namespace GameFramework.FSM.Generic
{
    public abstract class BaseState<T> : IState
    {
        protected T Parent { get; }

        public BaseState(T parent)
        {
            Parent = parent;
        }

        public virtual void OnEnter()
        {
            
        }

        public virtual void OnExit()
        {
            
        }
    }
}
