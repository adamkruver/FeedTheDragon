using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.PresentationInterfaces.Views;

namespace Sources.Client.InfrastructureInterfaces.Factories.Domain.Presentation.Views
{
    public interface IInventoryViewFactory
    {
        IInventoryView Create();
    }
}