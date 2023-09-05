using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.Client.InfrastructureInterfaces.SignalBus.Controllers
{
    public interface ISignalController
    {
        void Handle<T>(T signal) where T : ISignal;
    }
}