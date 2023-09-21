using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;
using UnityEngine;

namespace Sources.Client.Controllers.Enemies.Bears.Signals
{
    public class CreateBearSignal : ISignal
    {
        public CreateBearSignal(Vector3 position)
        {
            Position = position;
        }

        public Vector3 Position { get; }
    }
}