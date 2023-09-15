namespace Sources.Client.Frameworks.StateMachines.States
{
    public interface IState : IUpdateable
    {
        void Enter();
        void Exit();
    }
}