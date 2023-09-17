using Sources.Client.Controllers.Scenes.StateMachines.States;
using Sources.Client.Domain.Scenes.Payloads;
using Sources.Client.Frameworks.StateMachines;

namespace Sources.Client.Controllers.Scenes.Initial
{
    public class InitialScene : ISceneState
    {
        private readonly IStateMachine<IScenePayload> _stateMachine;

        public InitialScene(IStateMachine<IScenePayload> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Update(float deltaTime)
        {
        }

        public void LateUpdate(float deltaTime)
        {
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
        }

        public void Enter()
        {
            _stateMachine.ChangeState(new GameplayPayload());
        }

        public void Exit()
        {
        }
    }
}