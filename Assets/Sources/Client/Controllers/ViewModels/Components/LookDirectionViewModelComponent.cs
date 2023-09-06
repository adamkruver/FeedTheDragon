using System;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Domain;
using Sources.Client.Domain.Components;
using Sources.Client.PresentationInterfaces.Binds.Rotations;
using Sources.Client.UseCases.Common.Components.LookDirections.Listeners;
using Sources.Client.UseCases.Common.Components.LookDirections.Queries;
using UnityEngine;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class LookDirectionViewModelComponent : IViewModelComponent
    {
        private readonly int _id;
        private readonly AddLookDirectionListner _addLookDirectionListner;
        private readonly RemoveLookDirectionListner _removeLookDirectionListner;
        private readonly GetLookDirectionQuery _getLookDirectionQuery;

        [PropertyBinding(typeof(ILookDirectionPropertyBind))]
        private IBindableProperty<Vector3> _worldRotation;

        public LookDirectionViewModelComponent
        (
            int id,
            AddLookDirectionListner addLookDirectionListner,
            RemoveLookDirectionListner removeLookDirectionListner,
            GetLookDirectionQuery getLookDirectionQuery
        )
        {
            _id = id;
            _addLookDirectionListner = addLookDirectionListner;
            _removeLookDirectionListner = removeLookDirectionListner;
            _getLookDirectionQuery = getLookDirectionQuery;
        }

        public void Enable()
        {
            _addLookDirectionListner.Handle(_id, OnLookDirectionChanged);
            OnLookDirectionChanged();
        }

        public void Disable()
        {
            _removeLookDirectionListner.Handle(_id, OnLookDirectionChanged);
        }

        private void OnLookDirectionChanged()
        {
            _worldRotation.Value = _getLookDirectionQuery.Handle(_id);
        }
    }
}