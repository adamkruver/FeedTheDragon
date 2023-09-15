using Sources.Client.Frameworks.StateMachines.Payloads;
using Sources.Client.InfrastructureInterfaces.StateMachines;
using UnityEngine;

namespace Sources.Client.App
{
    public class AppCore : MonoBehaviour
    {
        private ISceneStateMachine _sceneStateMachine;
        
        private void Awake() =>
            DontDestroyOnLoad(this);

        private void Start() =>
            _sceneStateMachine.Run(new GameplayPayload());

        private void Update() =>
            _sceneStateMachine.Update(Time.deltaTime);

        private void FixedUpdate() =>
            _sceneStateMachine.FixedUpdate(Time.fixedDeltaTime);

        private void LateUpdate() =>
            _sceneStateMachine.LateUpdate(Time.deltaTime);

        private void OnDestroy() =>
            _sceneStateMachine.Stop();

        public void Init(ISceneStateMachine sceneStateMachine) =>
            _sceneStateMachine = sceneStateMachine;
    }
}