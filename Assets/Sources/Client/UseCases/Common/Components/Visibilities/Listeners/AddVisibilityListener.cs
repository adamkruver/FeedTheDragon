using System;
using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.Common.Components.Visibilities.Listeners
{
    public class AddVisibilityListener : ComponentUseCaseBase<VisibilityComponent>
    {
        public AddVisibilityListener(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public void Handle(int id, Action callback) =>
            GetComponent(id).Changed += callback;
    }
}