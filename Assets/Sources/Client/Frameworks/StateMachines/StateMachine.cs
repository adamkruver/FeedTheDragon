using System;
using System.Collections.Generic;
using Sources.Client.Frameworks.StateMachines.Payloads;
using Sources.Client.Frameworks.StateMachines.States;

namespace Sources.Client.Frameworks.StateMachines
{
    public abstract class StateMachine<TState, TPayload> : IStateMachine<TPayload>
        where TState : IState
        where TPayload : IPayload
    {
        private readonly Dictionary<Type, Func<TPayload, TState>> _stateBuilders;

        protected TState CurrentState;

        protected StateMachine(Dictionary<Type, Func<TPayload, TState>> stateBuilders)
        {
            _stateBuilders = stateBuilders;
        }

        public void ChangeState<T>(T payload) where T : TPayload
        {
            OnBeforeChange(payload);
            Enter(payload);
        }

        protected virtual void Enter<T>(T payload) where T : TPayload
        {
            Type payloadType = payload.GetType();

            TState state = _stateBuilders[payloadType].Invoke(payload);

            CurrentState?.Exit();
            CurrentState = state;
            state.Enter();
        }

        protected virtual void OnBeforeChange<T>(T payload) where T : TPayload
        {
        }

        protected virtual void Exit()
        {
            CurrentState?.Exit();
        }
    }
}