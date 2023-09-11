using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using UnityEngine;

namespace Sources.Client.UseCases.Common.Components.Destinations.Queries
{
    public class GetMoveImpulseQuery : ComponentUseCaseBase<DestinationComponent>
    {
        public GetMoveImpulseQuery(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public Vector3 Handle(int id) =>
            GetComponent(id).MoveDirection;
    }
}