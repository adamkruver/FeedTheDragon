using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;
using UnityEngine;

namespace Sources.Client.Controllers.Enemies.Spiders.Signals
{
    public class CreateSpiderSignal : ISignal
    {
        public CreateSpiderSignal(Vector3 position)
        {
            Position = position;
        }

        public Vector3 Position { get; }
    }
}