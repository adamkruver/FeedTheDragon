using System;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;

namespace Controllers.Frameworks.Mvvm.ViewModels
{
    public abstract class ViewModelBase : IViewModel
    {
        private readonly IViewModelComponent[] _components;

        private bool _isEnabled;

        protected ViewModelBase(IViewModelComponent[] components) =>
            _components = components;

        public event Action Destroyed;

        public void Enable()
        {
            if (_isEnabled)
                return;

            EnableParts();

            OnBeforeEnable();
            _isEnabled = true;
            OnEnable();
            OnAfterEnable();
        }

        public void Disable()
        {
            if (_isEnabled == false)
                return;

            OnBeforeDisable();
            _isEnabled = false;
            OnDisable();
            OnAfterDisable();

            DisableParts();
        }

        public void Destroy() =>
            Destroyed?.Invoke();

        protected abstract void OnEnable();

        protected abstract void OnDisable();

        protected virtual void OnBeforeEnable()
        {
        }

        protected virtual void OnBeforeDisable()
        {
        }

        protected virtual void OnAfterEnable()
        {
        }

        protected virtual void OnAfterDisable()
        {
        }

        private void EnableParts()
        {
            foreach (IViewModelComponent component in _components)
                component.Enable();
        }

        private void DisableParts()
        {
            foreach (IViewModelComponent component in _components)
                component.Disable();
        }
    }
}