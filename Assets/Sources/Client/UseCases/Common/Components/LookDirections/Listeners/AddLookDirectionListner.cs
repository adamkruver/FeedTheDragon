using System;
using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.Common.Components.LookDirections.Listeners
{
    public class AddLookDirectionListner : ComponentUseCaseBase<LookDirectionComponent>
    {
        public AddLookDirectionListner(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public void Handle(int id, Action callback) =>
            GetComponent(id).Changed += callback;
    }
}