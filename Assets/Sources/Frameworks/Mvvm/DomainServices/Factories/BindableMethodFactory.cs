using System;
using System.Linq;
using System.Reflection;
using Domain.Frameworks.Mvvm.Methods;
using DomainInterfaces.Frameworks.Mvvm.Methods;

namespace DomainServices.Frameworks.Mvvm.Factories
{
    public class BindableMethodFactory : IBindableMethodFactory
    {
        public object Create(object viewModel, MethodInfo methodInfo)
        {
            Type actionGenericType = typeof(BindableMethod<>);

            Type[] parameterTypes = methodInfo
                .GetParameters()
                .Select(info => info.ParameterType)
                .ToArray();

            Type actionType = actionGenericType.MakeGenericType(parameterTypes);

            return Activator.CreateInstance(actionType, new object[] { viewModel, methodInfo });
        }
    }
}