﻿using System;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Transforms;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Domain;
using Sources.Client.Domain.Components;
using UnityEngine;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class PositionViewModelComponent : IViewModelComponent
    {
        [PropertyBinding(typeof(ITransformPositionPropertyBind))]
        private IBindableProperty<Vector3> _transformPosition;

        public PositionViewModelComponent(IComposite composite)
        {
            if (composite.TryGetComponent(out PositionComponent positionComponent) == false)
                throw new NullReferenceException();

            Position = positionComponent;
        }

        private PositionComponent Position { get; }

        public void Enable()
        {
            Position.Changed += OnPositionChanged;
            OnPositionChanged();
        }

        public void Disable()
        {
            Position.Changed -= OnPositionChanged;
        }

        private void OnPositionChanged()
        {
            _transformPosition.Value = Position.Value;
        }        
    }
}