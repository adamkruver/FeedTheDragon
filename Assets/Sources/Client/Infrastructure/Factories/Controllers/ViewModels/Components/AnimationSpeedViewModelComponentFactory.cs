using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components.AnimationSpeeds.Listeners;
using Sources.Client.UseCases.Common.Components.AnimationSpeeds.Queries;

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
            AddSpeedListener addSpeedListener = new AddSpeedListener(_entityRepository);
            RemoveSpeedListener removeSpeedListener = new RemoveSpeedListener(_entityRepository);
            GetSpeedQuery getSpeedQuery = new GetSpeedQuery(_entityRepository);

            return new AnimationSpeedViewModelComponent
            (
                id,
                addSpeedListener,
                removeSpeedListener,
                getSpeedQuery
            );
        }
    }
}