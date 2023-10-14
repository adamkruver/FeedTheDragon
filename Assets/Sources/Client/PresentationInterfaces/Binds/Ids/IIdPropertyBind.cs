using DomainInterfaces.Frameworks.Mvvm.Properties.Generic;

namespace Sources.Client.PresentationInterfaces.Binds.Ids
{
    public interface IIdPropertyBind : IBindableViewProperty<int>
    {
        int Id { get; }
    }
}