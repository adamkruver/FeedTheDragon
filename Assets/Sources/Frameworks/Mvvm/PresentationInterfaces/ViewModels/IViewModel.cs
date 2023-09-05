using System;

namespace PresentationInterfaces.Frameworks.Mvvm.ViewModels
{
    public interface IViewModel
    {
        public event Action Destroyed;

        public void Enable();
        public void Disable();
    }
}