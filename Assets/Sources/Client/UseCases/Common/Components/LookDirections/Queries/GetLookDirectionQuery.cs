using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using UnityEngine;

namespace Sources.Client.UseCases.Common.Components.LookDirections.Queries
{
    public class GetLookDirectionQuery : ComponentUseCaseBase<LookDirectionComponent>
    {
        public GetLookDirectionQuery(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public Vector3 Handle(int id) =>
            GetComponent(id).Value;
    }
}