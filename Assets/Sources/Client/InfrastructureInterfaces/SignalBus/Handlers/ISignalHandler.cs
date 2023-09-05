using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.Client.InfrastructureInterfaces.SignalBus.Handlers
{
    public interface ISignalHandler
    {
        void Handle<T>(T signal) where T : ISignal;
    }
}