using GameFramework.Services.Providers;

namespace GameFramework.Services
{
    public class PlayerProgressService : BaseGameService
    {
        private readonly IPlayerProgressProvider _playerProgressService;

        public PlayerProgressService(IGame game) : base(game)
        {
            _playerProgressService = new PCPlayerProgressProvider();
        }

        public T GetSaveData<T>(string filename) where T: class, new()
        {
            return _playerProgressService.GetSaveData<T>(filename);
        }

        public void PutSaveData(string filename, object saveData)
        {
            _playerProgressService.PutSaveData(filename, saveData);
        }
    }
}
