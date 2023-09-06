using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using UnityEngine;

namespace Sources.Client.UseCases.Common.Components.Positions.Queries
{
    public class GetPositionQuery : ComponentUseCaseBase<PositionComponent>
    {
        public GetPositionQuery(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public Vector3 Handle(int id) =>
            GetComponent(id).Value;
    }
}