namespace GameFramework.Audio
{
    public interface IMusicService
    {
        void LoadSong(string songName, bool play = false);
        void PlaySong(string songName);
    }
}