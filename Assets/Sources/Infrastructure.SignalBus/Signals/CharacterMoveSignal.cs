using Sources.Infrastructure.SignalBus.Interfaces.Signals;
using UnityEngine;

namespace Sources.Infrastructure.SignalBus.Signals
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