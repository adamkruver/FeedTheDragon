using System;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Transforms;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Domain;
using Sources.Client.Domain.Components;
using Sources.Client.UseCases.Common.Components.Positions.Listeners;
using Sources.Client.UseCases.Common.Components.Positions.Queries;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class PositionViewModelComponent : IViewModelComponent
    {
        private readonly int _id;
        private readonly AddPositionListener _addPositionListener;
        private readonly RemovePositionListener _removePositionListener;
        private readonly GetPositionQuery _getPositionQuery;

        [PropertyBinding(typeof(ITransformPositionPropertyBind))]
        private IBindableProperty<Vector3> _transformPosition;

        public PositionViewModelComponent
        (
            int id,
            AddPositionListener addPositionListener,
            RemovePositionListener removePositionListener,
            GetPositionQuery getPositionQuery
        )
        {
            _id = id;
            _addPositionListener = addPositionListener;
            _removePositionListener = removePositionListener;
            _getPositionQuery = getPositionQuery;
        }

        public void Enable()
        {
            _addPositionListener.Handle(_id, OnPositionChanged);
            OnPositionChanged();
        }

        public void Disable()
        {
            _removePositionListener.Handle(_id, OnPositionChanged);
        }

        private void OnPositionChanged()
        {
            _transformPosition.Value = _getPositionQuery.Handle(_id);
        }
    }
}