using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.GameObjects;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Binds.GameObjects
{
    public class GameObjectEnabledSelectorPropertyBind : BindableViewProperty<int>,
        IGameObjectEnabledSelectorPropertyBind
    {
        [SerializeField] private GameObject[] _gameObjects;

        public override int BindableProperty { get; set; }

        private void Select(int index)
        {
            if (index < 0 || index >= _gameObjects.Length)
                return;

            for (int i = 0; i < _gameObjects.Length; i++)
            {
                GameObject @object = _gameObjects[i];
                bool isEnabled = i == index;

                @object.SetActive(isEnabled);
            }
        }
    }
}