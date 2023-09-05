using Sources.InfrastructureInterfaces.SignalBus;
using Sources.InfrastructureInterfaces.SignalBus.Handlers;
using Sources.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.Infrastructure.SignalBus
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