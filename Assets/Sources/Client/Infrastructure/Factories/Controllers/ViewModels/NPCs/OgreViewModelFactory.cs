using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.NPCs.Ogres.ViewModels;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Progresses.Components;
using Sources.Client.InfrastructureInterfaces.Factories.Controllers.ViewModels;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs
{
    public class OgreViewModelFactory : IViewModelFactory<OgreViewModel>
    {
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private readonly PositionViewModelComponentFactory _positionViewModelComponentFactory;
        private readonly QuestObserverViewModelComponentFactory _questObserverViewModelComponentFactory;

        public OgreViewModelFactory
        (
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            PositionViewModelComponentFactory positionViewModelComponentFactory,
            QuestObserverViewModelComponentFactory questObserverViewModelComponentFactory
        )
        {
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _positionViewModelComponentFactory = positionViewModelComponentFactory;
            _questObserverViewModelComponentFactory = questObserverViewModelComponentFactory;
        }

        public IViewModel Create(int id) =>
            new OgreViewModel(new[]
            {
                new EntityIdViewModelComponent(id),
                _visibilityViewModelComponentFactory.Create(id),
                _positionViewModelComponentFactory.Create(id),
                _questObserverViewModelComponentFactory.Create(id),
                new CharacterTriggerVisibilitySwitchViewModelComponent(),
            });
    }
}