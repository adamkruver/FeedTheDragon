using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.Progresses.ViewModels;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Factories.Controllers.ViewModels;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Progresses
{
    public class MissionViewModelFactory : IViewModelFactory<MissionViewModel>
    {
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private readonly MissionCounterViewModelComponentFactory _missionCounterViewModelComponentFactory;

        public MissionViewModelFactory(
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            MissionCounterViewModelComponentFactory missionCounterViewModelComponentFactory
        )
        {
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _missionCounterViewModelComponentFactory = missionCounterViewModelComponentFactory;
        }

        public IViewModel Create(int id) =>
            new MissionViewModel(new IViewModelComponent[]
            {
                _visibilityViewModelComponentFactory.Create(id),
                _missionCounterViewModelComponentFactory.Create(id),
            });
    }
}