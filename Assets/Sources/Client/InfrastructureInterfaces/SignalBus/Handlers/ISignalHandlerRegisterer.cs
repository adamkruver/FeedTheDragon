using Sources.Client.InfrastructureInterfaces.SignalBus.Controllers;

namespace Sources.Client.InfrastructureInterfaces.SignalBus.Handlers
{
    public interface ISignalHandlerRegisterer
    {
        public void Register(ISignalController signalController);
        public void Unregister(ISignalController signalController);
    }
}