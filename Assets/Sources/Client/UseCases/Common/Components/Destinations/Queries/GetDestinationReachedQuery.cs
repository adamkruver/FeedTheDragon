using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.UseCases.Common.Components.Destinations.Queries
{
    public class GetDestinationReachedQuery : ComponentUseCaseBase<DestinationComponent>
    {
        public GetDestinationReachedQuery(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public LiveData<bool> Handle(int id) =>
            GetComponent(id).IsReached;
    }
}