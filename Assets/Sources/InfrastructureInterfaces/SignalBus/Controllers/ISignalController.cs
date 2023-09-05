using Sources.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.InfrastructureInterfaces.SignalBus.Controllers
{
    public interface ISignalController
    {
        void Handle<T>(T signal) where T : ISignal;
    }
}