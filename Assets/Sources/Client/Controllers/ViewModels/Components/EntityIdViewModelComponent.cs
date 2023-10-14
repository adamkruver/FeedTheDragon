using Domain.Frameworks.Mvvm.Attributes;
using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.PresentationInterfaces.Binds.Ids;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class EntityIdViewModelComponent : IViewModelComponent
    {
        private readonly int _id;

        [PropertyBinding(typeof(IIdPropertyBind))]
        private BindableProperty<int> _entityId;

        public EntityIdViewModelComponent(int id) => 
            _id = id;

        public void Enable() => 
            _entityId.Value = _id;

        public void Disable()
        {
        }
    }
}