using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.Common.Components.AnimationSpeeds.Commands
{
    public class SetAnimationSpeedCommand : ComponentUseCaseBase<AnimationSpeedComponent>
    {
        public SetAnimationSpeedCommand(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public void Handle(int id, float value) =>
            GetComponent(id).Set(value);
    }
}