using System;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Domain;
using Sources.Client.Domain.Components;
using Sources.Client.PresentationInterfaces.Binds.Rotations;
using UnityEngine;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class LookDirectionViewModelComponent : IViewModelComponent
    {
        [PropertyBinding(typeof(ILookDirectionPropertyBind))]
        private IBindableProperty<Vector3> _worldRotation;
        
        public LookDirectionViewModelComponent(IComposite composite)
        {
            if (composite.TryGetComponent(out LookDirectionComponent lookDirectionComponent) == false)
                throw new NullReferenceException();

            LookDirection = lookDirectionComponent;
        }
        
        private LookDirectionComponent LookDirection { get; }

        public void Enable()
        {
            LookDirection.Changed += OnLookDirectionChanged;
            OnLookDirectionChanged();
        }

        public void Disable()
        {
            LookDirection.Changed -= OnLookDirectionChanged;
        }
        
        
        private void OnLookDirectionChanged()
        {
            _worldRotation.Value = LookDirection.Value;
        }
    }
}