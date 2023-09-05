using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic
{
    public interface ISignalAction<in T> : ISignalAction where T : ISignal
    {
        void Handle(T signal);
    }
}