using System;
using UnityEngine;

namespace Sources.Client.Presentation.Views.AudioPlayers
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class AudioPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;
        private bool _isEnded;
        
        public event Action Ended;

        public bool IsPlaying => _isEnded == false;
        
        private void Awake() => 
            _audioSource = GetComponent<AudioSource>();

        private void Update()
        {
            if (_isEnded == false)
                if (_audioSource.isPlaying == false)
                    Stop();
        }

        public void Play(AudioClip audioClip)
        {
            _audioSource.Stop();
            _audioSource.clip = audioClip;
            _audioSource.Play();
            _isEnded = false;
        }

        public void Stop()
        {
            _audioSource.Stop();
            _isEnded = true;
            Ended?.Invoke();
        }
    }
}