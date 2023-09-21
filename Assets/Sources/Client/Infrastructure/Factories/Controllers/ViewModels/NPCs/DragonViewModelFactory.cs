using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.NPCs.Dragons.ViewModels;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs.Components;
using Sources.Client.InfrastructureInterfaces.Factories.Controllers.ViewModels;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs
{
    public class DragonViewModelFactory : IViewModelFactory<DragonViewModel>
    {
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private readonly PositionViewModelComponentFactory _positionViewModelComponentFactory;
        public DragonViewModelFactory
        (
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            PositionViewModelComponentFactory positionViewModelComponentFactory
        )
        {
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _positionViewModelComponentFactory = positionViewModelComponentFactory;
        }

        public IViewModel Create(int id) =>
            new DragonViewModel(new[]
            {
                _visibilityViewModelComponentFactory.Create(id),
                _positionViewModelComponentFactory.Create(id)
            });
    }
}