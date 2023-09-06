using System;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Transforms;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Domain;
using Sources.Client.Domain.Components;
using Sources.Client.PresentationInterfaces.Binds.CharacterController;
using Sources.Client.UseCases.Common.Components.Positions.Commands;
using Sources.Client.UseCases.Common.Components.Positions.Listeners;
using Sources.Client.UseCases.Common.Components.Positions.Queries;
using UnityEngine;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class CharacterControllerMovementViewModelComponent : IViewModelComponent
    {
        private readonly int _id;
        private readonly AddPositionListener _addPositionListener;
        private readonly RemovePositionListener _removePositionListener;
        private readonly GetPositionQuery _getPositionQuery;
        private readonly SetPositionCommand _setPositionCommand;

        [PropertyBinding(typeof(ITransformPositionPropertyBind))]
        private IBindableProperty<Vector3> _transformPosition;

        [PropertyBinding(typeof(ICharacterControllerMovePropertyBind))]
        private IBindableProperty<Vector3> _controllerMovement;

        [PropertyBinding(typeof(ICharacterControllerPositionPropertyBind))]
        private IBindableProperty<Vector3> _controllerPosition;

        public CharacterControllerMovementViewModelComponent
        (
            int id,
            AddPositionListener addPositionListener,
            RemovePositionListener removePositionListener,
            GetPositionQuery getPositionQuery,
            SetPositionCommand setPositionCommand
        )
        {
            _id = id;
            _addPositionListener = addPositionListener;
            _removePositionListener = removePositionListener;
            _getPositionQuery = getPositionQuery;
            _setPositionCommand = setPositionCommand;
        }

        public void Enable()
        {
            _addPositionListener.Handle(_id, OnPositionChanged);
            _transformPosition.Value = _getPositionQuery.Handle(_id);
            OnPositionChanged();
        }

        public void Disable()
        {
            _removePositionListener.Handle(_id, OnPositionChanged);
        }

        private void OnPositionChanged()
        {
            _controllerMovement.Value = _getPositionQuery.Handle(_id) - _transformPosition.Value;

            Vector3 position = _transformPosition.Value;
            position.y = 0;

            _setPositionCommand.Handle(_id, position);
        }
    }
}