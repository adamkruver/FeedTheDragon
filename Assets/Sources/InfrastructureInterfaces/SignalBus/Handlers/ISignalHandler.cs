using Sources.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.InfrastructureInterfaces.SignalBus.Handlers
{
    public interface ISignalHandler
    {
        void Handle<T>(T signal) where T : ISignal;
    }
}