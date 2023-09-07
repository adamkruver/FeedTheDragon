using System;
using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.Common.Components.AnimationSpeeds.Listeners
{
    public class AddSpeedListener : ComponentUseCaseBase<AnimationSpeedComponent>
    {
        public AddSpeedListener(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public void Handle(int id, Action callback) =>
            GetComponent(id).Changed += callback;
    }
}