using System.Reflection;
using Presentation.Frameworks.Mvvm.Views;
using PresentationInterfaces.Frameworks.Mvvm.Binders;
using PresentationInterfaces.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.Views;

namespace Presentation.Frameworks.Mvvm.Factories
{
    public class BindableViewFactory : IBindableViewFactory
    {
        private static readonly string s_constructorMethodName = "Construct";

        private static readonly BindingFlags s_bindingFlags =
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        private readonly PrefabFactory _prefabFactory = new PrefabFactory();
        private readonly IBinder _binder;

        public BindableViewFactory(IBinder binder) =>
            _binder = binder;

        public IBindableView Create(string viewPath, string name, IBindableView parent = null)
        {
            BindableView view = _prefabFactory.Create<BindableView>(viewPath + name);
            Construct(view);

            view.SetParent(parent);
            
            return view;
        }

        private void Construct(BindableView view)
        {
            MethodInfo methodInfo = typeof(BindableView).GetMethod(s_constructorMethodName, s_bindingFlags);
            methodInfo.Invoke(view, new object[] { _binder });
        }
    }
}