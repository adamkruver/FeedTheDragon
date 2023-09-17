using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sources.Client.Controllers.Scenes.StateMachines.States;
using Sources.Client.Domain.Scenes.Payloads;
using Sources.Client.Frameworks.StateMachines;
using Sources.Client.InfrastructureInterfaces.StateMachines.Generic;
using UnityEngine.SceneManagement;

namespace Sources.Client.Infrastructure.StateMachines
{
    public class SceneStateMachine : StateMachine<ISceneState, IScenePayload>, ISceneStateMachine<IScenePayload>
    {
        public SceneStateMachine(Dictionary<Type, Func<IStateMachine<IScenePayload>, IScenePayload, ISceneState>> stateBuilders)
            : base(stateBuilders)
        {
        }
        
        public void Update(float deltaTime) =>
            CurrentState?.Update(deltaTime);

        public void LateUpdate(float deltaTime) =>
            CurrentState?.LateUpdate(deltaTime);

        public void FixedUpdate(float fixedDeltaTime) =>
            CurrentState?.FixedUpdate(fixedDeltaTime);

        public void Run<T>(T payload) where T : class, IScenePayload =>
            ChangeState(payload);

        public void Stop() =>
            Exit();

        protected override void OnBeforeChange<T>(T payload)
        {
            LoadScene(payload);
        }

        protected override void Enter<T>(T payload)
        {
        }

        private async void LoadScene<T>(T payload) where T : IScenePayload
        {
            await SceneManager.LoadSceneAsync(payload.SceneName);

            base.Enter(payload);
        }
    }
}