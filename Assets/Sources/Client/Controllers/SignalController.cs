﻿using System;
using System.Collections.Generic;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.InfrastructureInterfaces.SignalBus.Controllers;
using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.Client.Controllers
{
    public class SignalController : ISignalController
    {
        private readonly List<ISignalAction> _signalActions = new List<ISignalAction>();

        public SignalController(IEnumerable<ISignalAction> signalActions)
        {
            Register(signalActions);
        }

        public void Handle<T>(T signal) where T : ISignal
        {
            foreach (ISignalAction<T> signalAction in GetSignalActions<T>())
                signalAction.Handle(signal);
        }

        private void Register(IEnumerable<ISignalAction> signalActions)
        {
            foreach (ISignalAction signalAction in signalActions)
                Register(signalAction);
        }

        private void Register(ISignalAction signalAction)
        {
            if (_signalActions.Contains(signalAction))
                throw new AggregateException();

            _signalActions.Add(signalAction);
        }

        private IEnumerable<ISignalAction<T>> GetSignalActions<T>() where T : ISignal
        {
            List<ISignalAction<T>> actions = new List<ISignalAction<T>>();

            foreach (ISignalAction signalAction in _signalActions)
                if (signalAction is ISignalAction<T> concreteAction)
                    actions.Add(concreteAction);

            return actions;
        }
    }
}