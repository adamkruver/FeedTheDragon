using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Animators;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Binds.Animators
{
    public class AnimatorBoolPropertyBind : BindableViewProperty<bool>, IAnimatorBoolPropertyBind
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _propertyName;

        private int _speedHash;

        private void Awake() =>
            _speedHash = Animator.StringToHash(_propertyName);

        public override bool BindableProperty
        {
            get => _animator.GetBool(_speedHash);
            set => _animator.SetBool(_speedHash, value);
        }
    }
}