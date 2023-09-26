using Sources.Client.Frameworks.StateMachines;
using Sources.Client.Frameworks.StateMachines.States;

namespace Sources.Client.Controllers.Gameplays
{
    public interface IGameplayState : IState, IUpdatable, IFixedUpdatable, ILateUpdatable
    {
    }
}