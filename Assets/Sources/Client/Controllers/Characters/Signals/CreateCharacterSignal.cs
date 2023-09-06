using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;
using UnityEngine;

namespace Sources.Client.Controllers.Characters.Signals
{
    public class CreateCharacterSignal : ISignal
    {
        public CreateCharacterSignal(Vector3 spawnPosition)
        {
            SpawnPosition = spawnPosition;
        }

        public Vector3 SpawnPosition { get; }
    }
}