using UnityEngine;

namespace Sources.Client.InfrastructureInterfaces.Services.AudioPlayers
{
    public interface IAudioPlayerService
    {
        public void Add(AudioClip audioClip);
    }
}