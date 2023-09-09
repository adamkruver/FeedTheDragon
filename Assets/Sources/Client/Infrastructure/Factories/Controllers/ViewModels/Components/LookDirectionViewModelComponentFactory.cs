using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components.LookDirection.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components
{
    public class LookDirectionViewModelComponentFactory
    {
        private readonly IEntityRepository _entityRepository;

        public LookDirectionViewModelComponentFactory(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public IViewModelComponent Create(int id)
        {
            GetLookDirectionQuery getLookDirectionQuery = new GetLookDirectionQuery(_entityRepository);

            return new LookDirectionViewModelComponent(id, getLookDirectionQuery);
        }
    }
}