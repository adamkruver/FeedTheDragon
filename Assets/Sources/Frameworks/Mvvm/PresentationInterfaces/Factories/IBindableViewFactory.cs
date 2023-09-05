using System;
using PresentationInterfaces.Frameworks.Mvvm.Views;

namespace PresentationInterfaces.Frameworks.Mvvm.Factories
{
    public interface IBindableViewFactory
    {
        IBindableView Create(string viewPath, string name);
    }
}