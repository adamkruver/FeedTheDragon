using System;
using System.Collections.Generic;
using Sources.Client.InfrastructureInterfaces.SignalBus.Controllers;
using Sources.Client.InfrastructureInterfaces.SignalBus.Handlers;
using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.Client.Infrastructure.SignalBus
{
    public class SignalHandler : ISignalHandler
    {
        private readonly List<ISignalController> _signalControllers = new List<ISignalController>();

        public void Handle<T>(T signal) where T : ISignal
        {
            foreach (ISignalController signalController in _signalControllers)
            {
                signalController.Handle(signal);
            }
        }

        public void Register(ISignalController signalController)
        {
            if (_signalControllers.Contains(signalController))
                throw new AggregateException();

            _signalControllers.Add(signalController);
        }
    }
}