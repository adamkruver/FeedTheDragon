using PresentationInterfaces.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.InfrastructureInterfaces.Builders.Presentation.BindableViews;
using Sources.Client.InfrastructureInterfaces.Factories.Controllers.ViewModels;

namespace Sources.Client.Infrastructure.Builders.Presentation.BindableViews
{
    public class BindableViewBuilder<TViewModel> : IBindableViewBuilder<TViewModel> where TViewModel : IViewModel
    {
        private readonly IBindableViewFactory _bindableViewFactory;
        private readonly IViewModelFactory<TViewModel> _viewModelFactory;
        private readonly string _viewPath;
        private readonly IBindableView _parentView;

        public BindableViewBuilder(
            IBindableViewFactory bindableViewFactory,
            IViewModelFactory<TViewModel> viewModelFactory,
            string viewPath,
            IBindableView parentView = null
        )
        {
            _bindableViewFactory = bindableViewFactory;
            _viewModelFactory = viewModelFactory;
            _viewPath = viewPath;
            _parentView = parentView;
        }

        public IBindableView Build(int entityId, string prefabName)
        {
            IViewModel viewModel = _viewModelFactory.Create(entityId);
            IBindableView view = _bindableViewFactory.Create(_viewPath, prefabName, _parentView);
            
            view.Bind(viewModel);

            return view;
        }
    }
}