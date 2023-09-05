using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;
using UnityEngine;

namespace Sources.Client.Controllers.Characters.SIgnals
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