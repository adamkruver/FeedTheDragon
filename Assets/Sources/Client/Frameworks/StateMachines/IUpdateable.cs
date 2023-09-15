namespace Sources.Client.Frameworks.StateMachines
{
    public interface IUpdateable
    {
        void Update(float deltaTime);
        void LateUpdate(float deltaTime);
        void FixedUpdate(float fixedDeltaTime);
    }
}