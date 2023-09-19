using Controllers.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;

namespace Sources.Client.Controllers.Enemies.ViewModels
{
    public class EnemyViewModel : ViewModelBase
    {
        public EnemyViewModel(IViewModelComponent[] components) : base(components)
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