using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.NPCs.Common.ViewModels;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs
{
    public class QuestSlotViewModelFactory
    {
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;

        public QuestSlotViewModelFactory(VisibilityViewModelComponentFactory visibilityViewModelComponentFactory)
        {
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
        }

        public IViewModel Create(int id)
        {
            return new QuestSlotViewModel(new IViewModelComponent[]
            {
                _visibilityViewModelComponentFactory.Create(id)
            });
        }
    }
}