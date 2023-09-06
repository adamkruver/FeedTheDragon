using System;
using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.Common.Components.Visibilities.Listeners
{
    public class RemoveVisibilityListener : ComponentUseCaseBase<VisibilityComponent>
    {
        public RemoveVisibilityListener(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public void Handle(int id, Action callback) =>
            GetComponent(id).Changed -= callback;
    }
}