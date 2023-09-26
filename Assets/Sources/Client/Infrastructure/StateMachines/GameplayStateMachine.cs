using System;
using System.Collections.Generic;
using Sources.Client.Controllers.Gameplays;
using Sources.Client.Domain.Gameplays;
using Sources.Client.Domain.Gameplays.Payloads;
using Sources.Client.Frameworks.StateMachines;

namespace Sources.Client.Infrastructure.StateMachines
{
    public class GameplayStateMachine : StateMachineBase<IGameplayState, IGameplayPayload>, IUpdatable, ILateUpdatable, IFixedUpdatable
    {
        public GameplayStateMachine
            (IReadOnlyDictionary<Type, Func<IStateMachine<IGameplayPayload>, IGameplayPayload, IGameplayState>> stateBuilders) 
            : base(stateBuilders)
        {
        }

        public void Run() =>
            ChangeState(new MainGameplayPayload());

        public void Update(float deltaTime) =>
            CurrentState?.Update(deltaTime);

        public void LateUpdate(float deltaTime) =>
            CurrentState?.LateUpdate(deltaTime);

        public void FixedUpdate(float fixedDeltaTime) =>
            CurrentState?.FixedUpdate(fixedDeltaTime);

        public void Stop() =>
            Exit();
    }
}