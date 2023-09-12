using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using NodeCanvas.Framework;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Transforms;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Triggers;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.Ingredients.Signals;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.PresentationInterfaces.Binds.BehaviourTrees;
using Sources.Client.UseCases.Common.Components.AnimationSpeeds.Queries;
using Sources.Client.UseCases.Common.Components.Destinations.Queries;
using Sources.Client.UseCases.Common.Components.LookDirection.Commands;
using Sources.Client.UseCases.Common.Components.LookDirection.Queries;
using Sources.Client.UseCases.Common.Components.Speeds.Commands;
using UnityEngine;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Controllers.Ingredients.ViewModels.Components
{
    public class ToxicFrogBehaviourTreeViewModelComponent : IViewModelComponent
    {
        private readonly int _id;
        private readonly ISignalBus _signalBus;
        private readonly SetLookDirectionCommand _setLookDirectionCommand;
        private readonly SetSpeedCommand _setSpeedCommand;
        private readonly GetMoveImpulseQuery _getMoveImpulseQuery;

        [PropertyBinding(typeof(IBlackboardPropertyBind))]
        private IBindableProperty<Blackboard> _blackboard;

        [PropertyBinding(typeof(ITransformPositionPropertyBind))]
        private IBindableProperty<Vector3> _position;

        private readonly LiveData<bool> _isReached;
        private readonly LiveData<float> _speed;
        private readonly LiveData<Vector3> _lookDirection;

        private float _jumpSpeed = 2f;

        public ToxicFrogBehaviourTreeViewModelComponent(
            int id,
            ISignalBus signalBus,
            SetLookDirectionCommand setLookDirectionCommand,
            GetLookDirectionQuery getLookDirectionQuery,
            GetSpeedQuery getSpeedQuery,
            GetDestinationReachedQuery getDestinationReachedQuery
        )
        {
            _id = id;
            _signalBus = signalBus;
            _setLookDirectionCommand = setLookDirectionCommand;

            _isReached = getDestinationReachedQuery.Handle(id);
            _speed = getSpeedQuery.Handle(_id);
            _lookDirection = getLookDirectionQuery.Handle(_id);
        }

        Vector3 Destination => _position.Value + _lookDirection.Value.normalized * _jumpSpeed;

        public void Enable()
        {
            _isReached.Observe(OnDestinationReached);
        }

        public void Disable()
        {
            _isReached.Unobserve(OnDestinationReached);
        }

        [MethodBinding(typeof(IActionMethodBind))]
        private void Jump(bool _)
        {
            _blackboard.Value.SetVariableValue("jumpDelay", Random.Range(1f, 3f));
            Debug.Log(_position.Value);
            Debug.Log(Destination);
            Debug.Log(_speed.Value);
            _signalBus.Handle(new ToxicFrogJumpSignal(_id, Destination, _jumpSpeed));
        }

        [MethodBinding(typeof(ITriggerStayMethodBind))]
        private void OnTriggerStay(Component component)
        {
            if(_isReached.Value == false)
                return;
            
            if (component.TryGetComponent(out CharacterController character) == false)
                return;

            var direction = _position.Value - character.transform.position;
            direction.y = 0;

            _setLookDirectionCommand.Handle(_id, direction);
        }

        private void OnDestinationReached(bool isReached)
        {
            _blackboard.Value.SetVariableValue("isReached", isReached);
        }
    }
}