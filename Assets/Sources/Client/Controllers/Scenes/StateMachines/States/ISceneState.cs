using Sources.Client.Frameworks.StateMachines;
using Sources.Client.Frameworks.StateMachines.States;

namespace Sources.Client.Controllers.Scenes.StateMachines.States
{
    public interface ISceneState : IState, IUpdatable, IFixedUpdatable, ILateUpdatable
    {
    }
}