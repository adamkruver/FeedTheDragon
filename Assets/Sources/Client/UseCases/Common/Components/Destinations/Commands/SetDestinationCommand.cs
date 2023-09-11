using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using UnityEngine;

namespace Sources.Client.UseCases.Common.Components.Destinations.Commands
{
    public class SetDestinationCommand : ComponentUseCaseBase<DestinationComponent>
    {
        public SetDestinationCommand(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public void Handle(int id, Vector3 destination) =>
            GetComponent(id).Set(destination);
    }
}