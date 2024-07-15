namespace GameFramework.Services.Providers
{
    public interface IPlayerProgressProvider
    {
        T GetSaveData<T>(string filename) where T : class, new();

        void PutSaveData(string filename, object saveData);
    }
}
