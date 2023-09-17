using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Handlers;
using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.Client.Infrastructure.SignalBuses
{
    public class SignalBus : ISignalBus
    {
        private readonly ISignalHandler _signalHandler;

        public SignalBus(ISignalHandler signalHandler)
        {
            _signalHandler = signalHandler;
        }
        
        public void Handle<T>(T signal) where T : ISignal
        {
            //Network
            
            _signalHandler.Handle(signal);
        }
    }
}