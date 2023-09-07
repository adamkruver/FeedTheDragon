using System;
using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components;

namespace Sources.Client.UseCases.InventoryComponents.Listeners
{
    public class AddInventoryListener : ComponentUseCaseBase<InventoryComponent>
    {
        public AddInventoryListener(IEntityRepository entityRepository) : base(entityRepository)
        {
        }
        
        public void Handle(int id, Action callback)
        {
            GetComponent(id).Changed += callback;
        }
    }
}