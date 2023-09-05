using System;
using PresentationInterfaces.Frameworks.Mvvm.Binders;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Views
{
    public class BindableView : MonoBehaviour, IBindableView
    {
        protected IBinder Binder;

        private IViewModel _viewModel;

        public Action AfterUnbindCallback { get; set; }

        private void Awake() =>
            gameObject.SetActive(false);

        public void Bind(IViewModel viewModel)
        {
            _viewModel = viewModel;
            Binder.Bind(this, viewModel);
            viewModel.Enable();
        }

        public void Unbind()
        {
            if (_viewModel != null)
                Binder.Unbind(this, _viewModel);

            gameObject.SetActive(false);

            AfterUnbindCallback?.Invoke();
        }

        public void Construct(IBinder binder) =>
            Binder = binder;
    }
}