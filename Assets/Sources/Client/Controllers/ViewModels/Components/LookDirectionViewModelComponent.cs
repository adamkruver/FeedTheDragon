using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.PresentationInterfaces.Binds.Rotations;
using Sources.Client.UseCases.Common.Components.LookDirection.Queries;
using UnityEngine;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class LookDirectionViewModelComponent : IViewModelComponent
    {
        private LiveData<Vector3> _lookDirection;

        [PropertyBinding(typeof(ILookDirectionPropertyBind))]
        private IBindableProperty<Vector3> _worldRotation;

        public LookDirectionViewModelComponent(int id, GetLookDirectionQuery getLookDirectionQuery) =>
            _lookDirection = getLookDirectionQuery.Handle(id);

        public void Enable() =>
            _lookDirection.Observe(OnLookDirectionChanged);

        public void Disable() =>
            _lookDirection.Unobserve(OnLookDirectionChanged);

        private void OnLookDirectionChanged(Vector3 directoin) =>
            _worldRotation.Value = directoin;
    }
}