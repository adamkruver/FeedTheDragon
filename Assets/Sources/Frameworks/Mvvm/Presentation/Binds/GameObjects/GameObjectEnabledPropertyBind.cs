using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.GameObjects;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Binds.GameObjects
{
    public class GameObjectEnabledPropertyBind : BindableViewProperty<bool>, IGameObjectEnabledPropertyBind
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private bool _enabledOnStart;

        private void Awake() =>
            BindableProperty = _enabledOnStart;

        public override bool BindableProperty
        {
            get => _gameObject.activeSelf;
            set => _gameObject.SetActive(value);
        }
    }
}