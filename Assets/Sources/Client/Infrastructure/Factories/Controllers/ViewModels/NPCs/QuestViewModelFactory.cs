using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.NPCs.Common.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs
{
    public class QuestViewModelFactory
    {
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;

        public QuestViewModelFactory(VisibilityViewModelComponentFactory visibilityViewModelComponentFactory)
        {
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
        }

        public IViewModel Create(int id)
        {
            return new QuestViewModel(
                new IViewModelComponent[]
                {
                    _visibilityViewModelComponentFactory.Create(id)
                });
        }
    }
}