using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;

namespace Sources.Client.InfrastructureInterfaces.Builders.Presentation.BindableViews
{
    public interface IBindableViewBuilder<TViewModel> where TViewModel : IViewModel
    {
        IBindableView Build(int entityId, string prefabName);
    }
}