namespace GameFramework.Services
{
    public abstract class BaseGameService : IGameService
    {
        protected IGame Game { get; }

        public string Name => GetType().Name;

        public BaseGameService(IGame game)
        {
            Game = game;
        }
    }
}
