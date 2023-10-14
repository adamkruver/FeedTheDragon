using Controllers.Frameworks.Mvvm.ViewModels;
using Domain.Frameworks.Mvvm.Attributes;
using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Presentation.Binds.Ids;
using Sources.Client.PresentationInterfaces.Binds.Ids;

namespace Sources.Client.Controllers.Characters.ViewModels
{
    public class CharacterViewModel : ViewModelBase
    {
        public CharacterViewModel(
            IViewModelComponent[] components) : base(components)
        {
        }

        protected override void OnEnable()
        {
        }

        protected override void OnDisable()
        {
        }
    }
}