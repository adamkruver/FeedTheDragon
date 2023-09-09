using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Transforms;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.UseCases.Common.Components.Positions.Queries;
using UnityEngine;
using Utils.LiveData;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class PositionViewModelComponent : IViewModelComponent
    {
        private readonly LiveData<Vector3> _position;

        [PropertyBinding(typeof(ITransformPositionPropertyBind))]
        private IBindableProperty<Vector3> _transformPosition;

        public PositionViewModelComponent(int id, GetPositionQuery getPositionQuery) =>
            _position = getPositionQuery.Handle(id);

        public void Enable() =>
            _position.Observe(OnPositionChanged);

        public void Disable() =>
            _position.Unobserve(OnPositionChanged);

        private void OnPositionChanged(Vector3 position) =>
            _transformPosition.Value = position;
    }
}