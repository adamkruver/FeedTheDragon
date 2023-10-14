using Domain.Frameworks.Mvvm.Properties;
using Sources.Client.PresentationInterfaces.Binds.Ids;

namespace Sources.Client.Presentation.Binds.Ids
{
    public class IdPropertyBind : BindableViewProperty<int>, IIdPropertyBind
    {
        private int _id;

        public override int BindableProperty
        {
            get => _id; 
            set => _id = value;
        }

        public int Id => _id;
    }
}