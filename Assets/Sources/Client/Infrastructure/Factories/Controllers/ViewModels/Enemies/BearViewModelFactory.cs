using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.Enemies.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Factories.Controllers.ViewModels;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Enemies
{
    public class BearViewModelFactory : IViewModelFactory<EnemyViewModel>
    {
        private readonly PositionViewModelComponentFactory _positionViewModelComponentFactory;
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;

        public BearViewModelFactory(
            PositionViewModelComponentFactory positionViewModelComponentFactory,
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory
        )
        {
            _positionViewModelComponentFactory = positionViewModelComponentFactory;
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
        }

        public IViewModel Create(int id)
        {
            return new EnemyViewModel(new IViewModelComponent[]
            {
                _positionViewModelComponentFactory.Create(id),
                _visibilityViewModelComponentFactory.Create(id),
            });
        }
    }
}