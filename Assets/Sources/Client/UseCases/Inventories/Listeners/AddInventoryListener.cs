using System;
using Sources.Client.Domain.Components;
using Sources.Client.Domain.Inventories;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components;

namespace Sources.Client.UseCases.Inventories.Listeners
{
    public class AddInventoryListener : ComponentUseCaseBase<Inventory>
    {
        public AddInventoryListener(IEntityRepository entityRepository) : base(entityRepository)
        {
        }
        
        public void Handle(int id, Action callback) => 
            GetComponent(id).Changed += callback;
    }
}