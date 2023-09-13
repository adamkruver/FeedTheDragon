using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components.Speeds.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components
{
    public class AnimationSpeedViewModelComponentFactory
    {
        private readonly IEntityRepository _entityRepository;

        public AnimationSpeedViewModelComponentFactory(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public IViewModelComponent Create(int id)
        {
            GetSpeedQuery getSpeedQuery = new GetSpeedQuery(_entityRepository);

            return new AnimationSpeedViewModelComponent(id, getSpeedQuery);
        }
    }
}