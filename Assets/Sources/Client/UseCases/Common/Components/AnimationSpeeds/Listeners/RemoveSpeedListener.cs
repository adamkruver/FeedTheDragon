using System;
using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.Common.Components.Speeds.Listeners
{
    public class RemoveSpeedListener : ComponentUseCaseBase<AnimationSpeedComponent>
    {
        public RemoveSpeedListener(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public void Handle(int id, Action callback) =>
            GetComponent(id).Changed -= callback;
    }
}