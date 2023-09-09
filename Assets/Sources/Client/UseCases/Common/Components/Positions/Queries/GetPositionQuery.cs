using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using UnityEngine;
using Utils.LiveData;

namespace Sources.Client.UseCases.Common.Components.Positions.Queries
{
    public class GetPositionQuery : ComponentUseCaseBase<PositionComponent>
    {
        public GetPositionQuery(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public LiveData<Vector3> Handle(int id) =>
            GetComponent(id).Position;
    }
}