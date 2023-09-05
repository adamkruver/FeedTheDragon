using Sources.Infrastructure.SignalBus.Interfaces.Signals;

namespace Sources.Infrastructure.SignalBus.Interfaces
{
    public interface ISignalController
    {
        void Handle<T>(T signal) where T : ISignal;
    }
}