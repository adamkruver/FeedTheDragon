namespace Sources.Client.Frameworks.StateMachines.States
{
    public interface IState : IUpdatable, IFixedUpdatable, ILateUpdatable
    {
        void Enter();
        void Exit();
    }
}