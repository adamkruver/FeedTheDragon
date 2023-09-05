using Sources.InfrastructureInterfaces.SignalBus.Signals;
using UnityEngine;

namespace Sources.Infrastructure.Signals
{
    public class CharacterMoveSignal : ISignal
    {
        public CharacterMoveSignal(Vector3 moveDelta)
        {
            MoveDelta = moveDelta;
        }

        public Vector3 MoveDelta { get; }
    }
}