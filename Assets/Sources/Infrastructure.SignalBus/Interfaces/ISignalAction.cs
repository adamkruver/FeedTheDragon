using Sources.Infrastructure.SignalBus.Interfaces.Signals;

namespace Sources.Infrastructure.SignalBus.Interfaces
{
    public interface ISignalAction
    {
    }

    public interface ISignalAction<in T> : ISignalAction where T : ISignal
    {
        void Handle(T signal);
    }
}