using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.NPCs.Common.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs.Components;
using Sources.Client.InfrastructureInterfaces.Factories.Controllers.ViewModels;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs
{
    public class QuestViewModelFactory : IViewModelFactory<QuestViewModel>
    {
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private readonly QuestSlotObserverViewModelComponentFactory _questSlotObserverViewModelComponentFactory;

        public QuestViewModelFactory
        (
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            QuestSlotObserverViewModelComponentFactory questSlotObserverViewModelComponentFactory
        )
        {
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _questSlotObserverViewModelComponentFactory = questSlotObserverViewModelComponentFactory;
        }

        public IViewModel Create(int id) =>
            new QuestViewModel(
                new IViewModelComponent[]
                {
                    _visibilityViewModelComponentFactory.Create(id),
                    _questSlotObserverViewModelComponentFactory.Create(id)
                });
    }
}