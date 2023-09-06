using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components.Positions.Listeners;
using Sources.Client.UseCases.Common.Components.Positions.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components
{
    public class PositionViewModelComponentFactory
    {
        private readonly IEntityRepository _entityRepository;

        public PositionViewModelComponentFactory(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public IViewModelComponent Create(int id)
        {
            AddPositionListener addPositionListener =
                new AddPositionListener(_entityRepository);

            RemovePositionListener removePositionListener =
                new RemovePositionListener(_entityRepository);

            GetPositionQuery getPositionQuery =
                new GetPositionQuery(_entityRepository);

            return new PositionViewModelComponent
            (
                id,
                addPositionListener,
                removePositionListener,
                getPositionQuery
            );
        }
    }
}