﻿using System;
using Controllers.Frameworks.Mvvm.ViewModels;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.BindableViews;
using PresentationInterfaces.Frameworks.Mvvm.Binds.GameObjects;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Mouses;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Transforms;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Triggers;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Domain;
using Sources.Client.Domain.Components;
using Sources.Client.Domain.Ingredients;
using UnityEngine;

namespace Sources.Client.Controllers.Ingredients.ViewModels
{
    public class IngredientViewModel : ViewModelBase
    {
        private readonly Ingredient _ingredient;

        [PropertyBinding(typeof(IBindableViewEnabledPropertyBind))]
        private IBindableProperty<bool> _isEnabled;

        [PropertyBinding(typeof(ITransformPositionPropertyBind))]
        private IBindableProperty<Vector3> _position;

        [PropertyBinding(typeof(IGameObjectEnabledPropertyBind))]
        private IBindableProperty<bool> _isProximityEnabled;

        public IngredientViewModel(IViewModelComponent[] components, Ingredient ingredient) : base(components)
        {
            _ingredient = ingredient;
        }

        private PositionComponent IngredientPosition => GetComponent<PositionComponent>(_ingredient);

        protected override void OnEnable()
        {
            _isEnabled.Value = true;
            IngredientPosition.Changed += OnPositionChanged;

            OnPositionChanged();
        }

        protected override void OnDisable()
        {
            IngredientPosition.Changed -= OnPositionChanged;

            _isEnabled.Value = false;
        }

        private void OnPositionChanged()
        {
            _position.Value = IngredientPosition.Value;
        }

        [MethodBinding(typeof(ITriggerEnterMethodBind))]
        private void OnTriggerEnter(Component other)
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

        [MethodBinding(typeof(IMouseDownMethodBind))]
        private void OnClick(Vector3 position)
        {
            Debug.Log(position + " clicked!");
        }
        
        private T GetComponent<T>(Composite composite) where T : IComponent
        {
            if (composite.TryGetComponent(out T component) == false)
                throw new NullReferenceException();

            return component;
        }
    }
}