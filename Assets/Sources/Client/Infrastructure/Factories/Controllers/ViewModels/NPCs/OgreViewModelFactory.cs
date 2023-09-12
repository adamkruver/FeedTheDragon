using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.NPCs.Ogres.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs
{
    public class OgreViewModelFactory
    {
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private readonly PositionViewModelComponentFactory _positionViewModelComponentFactory;

        public OgreViewModelFactory(
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            PositionViewModelComponentFactory positionViewModelComponentFactory)
        {
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _positionViewModelComponentFactory = positionViewModelComponentFactory;
        }

        public IViewModel Create(int id) =>
            new OgreViewModel(new[]
            {
                _visibilityViewModelComponentFactory.Create(id),
                _positionViewModelComponentFactory.Create(id)
            });
    }
}