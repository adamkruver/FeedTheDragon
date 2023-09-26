using System;
using Controllers.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;

namespace Sources.Client.Controllers.Progresses.ViewModels
{
    public class MissionViewModel : ViewModelBase
    {
        public MissionViewModel(IViewModelComponent[] components) : base(components)
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