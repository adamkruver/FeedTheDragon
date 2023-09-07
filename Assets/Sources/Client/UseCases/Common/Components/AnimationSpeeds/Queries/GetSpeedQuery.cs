using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.Common.Components.AnimationSpeeds.Queries
{
    public class GetSpeedQuery : ComponentUseCaseBase<AnimationSpeedComponent>
    {
        public GetSpeedQuery(IEntityRepository entityRepository) : base(entityRepository)
        {
        }
        
        public float Handle(int id) =>
            GetComponent(id).Value;
    }
}