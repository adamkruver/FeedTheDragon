using System.Collections.Generic;
using Sources.Client.InfrastructureInterfaces.Services.AudioPlayers;
using Sources.Client.Presentation.Views.AudioPlayers;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.AudioPlayers
{
    public class AudioPlayerService : IAudioPlayerService
    {
        private readonly AudioPlayer _audioPlayer;
        private readonly Queue<AudioClip> _audioClips = new Queue<AudioClip>();

        public AudioPlayerService(AudioPlayer audioPlayer) =>
            _audioPlayer = audioPlayer;

        public void Enable() =>
            _audioPlayer.Ended += OnPlayingEnded;

        public void Disable() =>
            _audioPlayer.Ended -= OnPlayingEnded;

        public void Add(AudioClip audioClip)
        {
            _audioClips.Enqueue(audioClip);

            if (_audioPlayer.IsPlaying == false)
                OnPlayingEnded();
        }

        private void OnPlayingEnded()
        {
            if (_audioClips.Count == 0)
                return;

            _audioPlayer.Play(_audioClips.Dequeue());
        }
    }
}