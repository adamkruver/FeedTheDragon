using System;
using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.Common.Components.Positions.Listeners
{
    public class RemovePositionListener : ComponentUseCaseBase<PositionComponent>
    {
        public RemovePositionListener(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public void Handle(int id, Action action) =>
            GetComponent(id).Changed -= action;
    }
}