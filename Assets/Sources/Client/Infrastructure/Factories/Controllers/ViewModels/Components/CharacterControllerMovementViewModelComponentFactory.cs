using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components.Positions.Commands;
using Sources.Client.UseCases.Common.Components.Positions.Listeners;
using Sources.Client.UseCases.Common.Components.Positions.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components
{
    public class CharacterControllerMovementViewModelComponentFactory
    {
        private readonly IEntityRepository _entityRepository;

        public CharacterControllerMovementViewModelComponentFactory(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public IViewModelComponent Create(int id)
        {
            AddPositionListener addPositionListener = new AddPositionListener(_entityRepository);
            RemovePositionListener removePositionListener = new RemovePositionListener(_entityRepository);
            GetPositionQuery getPositionQuery = new GetPositionQuery(_entityRepository);
            SetPositionCommand setPositionCommand = new SetPositionCommand(_entityRepository);

            return new CharacterControllerMovementViewModelComponent
            (
                id,
                addPositionListener,
                removePositionListener,
                getPositionQuery,
                setPositionCommand
            );
        }
    }
}