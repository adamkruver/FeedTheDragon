using Controllers.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;

namespace Sources.Client.Controllers.Characters.ViewModels
{
    public class CharacterViewModel : ViewModelBase
    {
        public CharacterViewModel(IViewModelComponent[] components) : base(components)
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