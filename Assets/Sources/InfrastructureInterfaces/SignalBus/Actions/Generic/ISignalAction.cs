using Sources.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.InfrastructureInterfaces.SignalBus.Actions.Generic
{
    public interface ISignalAction<in T> : ISignalAction where T : ISignal
    {
        void Handle(T signal);
    }
}