using Sources.Client.InfrastructureInterfaces.ViewProviders;
using Sources.Client.PresentationInterfaces.Views;

namespace Sources.Client.Infrastructure.ViewProviders
{
    public class ViewProvider : IInventoryViewProvider
    {
        public IInventoryView InventoryView { get; set; }
    }
}