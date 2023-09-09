using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components.Visibilities.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components
{
    public class VisibilityViewModelComponentFactory
    {
        private readonly IEntityRepository _entityRepository;

        public VisibilityViewModelComponentFactory(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }
        
        public IViewModelComponent Create(int id)
        {
            GetVisibilityQuery getVisibilityQuery = new GetVisibilityQuery(_entityRepository);

            return new VisibilityViewModelComponent(id, getVisibilityQuery);
        }
    }
}