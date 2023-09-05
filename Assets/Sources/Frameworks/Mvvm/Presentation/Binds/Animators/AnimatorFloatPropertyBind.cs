using System;
using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Animators;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Binds.Animators
{
    public class AnimatorFloatPropertyBind : BindableViewProperty<float>, IAnimatorFloatPropertyBind
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _propertyName;

        private int _speedHash;

        private void Awake() => 
            _speedHash = Animator.StringToHash(_propertyName);

        public override float BindableProperty
        {
            get => _animator.GetFloat(_speedHash);
            set => _animator.SetFloat(_speedHash, value);
        }
    }
}