using GameFramework.Bubbling.Events;
using GameFramework.Services;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace GameFramework.Audio
{
    public class MusicService : BaseGameService, IMusicService, IBubbleMessageRecipient
    {
        private readonly Dictionary<string, Song> _songs = new Dictionary<string, Song>();
        private readonly Dictionary<string, SoundEffect> _fx = new Dictionary<string, SoundEffect>();

        public MusicService(IGame game) : base(game)
        {
            Game.MessageQueue.RegisterInterest(this, typeof(PlayEffectEvent));
        }

        public void EventFired(IBubbleEvent bubbleEvent)
        {
            PlayEffectEvent e = bubbleEvent as PlayEffectEvent;
            PlayFX(e?.FxName);
        }

        public void LoadSong(string songName, bool play = false)
        {
            string song = songName.ToLower();
            if (_songs.ContainsKey(song)) return;
            _songs.Add(song, Game.Content.Load<Song>(songName));
            if (play)
            {
                MediaPlayer.Play(_songs[song]);
            }
        }

        public void PlayFX(string fxName)
        {
            if (string.IsNullOrEmpty(fxName)) return;

            string fx = fxName.ToLower();
            if (_fx.TryGetValue(fxName, out SoundEffect soundEffect))
            {
                soundEffect.Play();
            }
            else
            {
                var newFx = Game.Content.Load<SoundEffect>(fxName);
                newFx.Play();
                _fx.Add(fx, newFx);
            }
        }

        public void PlaySong(string songName)
        {
            if (_songs.TryGetValue(songName.ToLower(), out Song song))
            {
                MediaPlayer.Play(song);
            }
        }
    }
}
