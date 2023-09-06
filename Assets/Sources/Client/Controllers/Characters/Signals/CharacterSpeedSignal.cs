using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.Client.Controllers.Characters.Signals
{
    public class CharacterSpeedSignal : ISignal
    {
        public CharacterSpeedSignal(float speed)
        {
            Speed = speed;
        }

        public float Speed { get; }
    }
}