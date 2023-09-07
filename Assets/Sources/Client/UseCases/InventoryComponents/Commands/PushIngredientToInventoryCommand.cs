using Sources.Client.Domain.Components;
using Sources.Client.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components;

namespace Sources.Client.UseCases.Characters.InventoryComponents.Commands
{
    public class PushIngredientToInventoryCommand : ComponentUseCaseBase<InventoryComponent>
    {
        public PushIngredientToInventoryCommand(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public bool Handle(int id, Ingredient ingredient) =>
            GetComponent(id).TryPush(ingredient);
    }
}