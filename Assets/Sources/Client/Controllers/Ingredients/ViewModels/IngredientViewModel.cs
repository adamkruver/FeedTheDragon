﻿using Controllers.Frameworks.Mvvm.ViewModels;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.BindableViews;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Triggers;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.PresentationInterfaces.Binds.Scales;
using UnityEngine;

namespace Sources.Client.Controllers.Ingredients.ViewModels
{
    public class IngredientViewModel : ViewModelBase
    {
        [PropertyBinding(typeof(IGameObjectEnableWithScaleFadePropertyBind))]
        private IBindableProperty<bool> _isProximityEnabled;

        [PropertyBinding(typeof(IBindableViewEnabledPropertyBind))]
        private IBindableProperty<bool> _isEnabled;

        public IngredientViewModel(IViewModelComponent[] components) : base(components)
        {
        }

        protected override void OnEnable()
        {
        }

        protected override void OnDisable()
        {
        }

        [MethodBinding(typeof(ITriggerEnterMethodBind))]
        private void OnTriggerEnter(Component other) // todo: вынести в компоненты
        {
            if (other.TryGetComponent<CharacterController>(out _))
                _isProximityEnabled.Value = true;
        }

        [MethodBinding(typeof(ITriggerExitMethodBind))]
        private void OnTriggerExit(Component other)
        {
            if (other.TryGetComponent<CharacterController>(out _))
                _isProximityEnabled.Value = false;
        }
    }
}