using PresentationInterfaces.Frameworks.Mvvm.Factories;
using UnityEngine;

namespace Sources.Client.Infrastructure.Factories.Presentation.Views
{
    public abstract class ViewFactoryBase<T> where T : MonoBehaviour
    {
        private readonly IBindableViewFactory _bindableViewFactory;
        private readonly string _prefabPath;

        protected ViewFactoryBase(IBindableViewFactory bindableViewFactory, string prefabPath)
        {
            _bindableViewFactory = bindableViewFactory;
            _prefabPath = prefabPath;
        }

        protected T Create(string prefabName) =>
            (T)_bindableViewFactory.Create(_prefabPath, prefabName);
    }
}