using Sources.Infrastructure.SignalBus.Interfaces;
using Sources.Infrastructure.SignalBus.Interfaces.Signals;

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