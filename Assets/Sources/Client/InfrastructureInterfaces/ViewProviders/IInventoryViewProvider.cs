using Sources.Client.PresentationInterfaces.Views;

namespace Sources.Client.InfrastructureInterfaces.ViewProviders
{
    public interface IInventoryViewProvider
    {
        IInventoryView InventoryView { get; }
    }
}