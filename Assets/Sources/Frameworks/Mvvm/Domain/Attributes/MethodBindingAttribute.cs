using System;

namespace Domain.Frameworks.Mvvm.Attributes
{
    public class MethodBindingAttribute : Attribute
    {
        public MethodBindingAttribute(Type componentType, string componentName = null)
        {
            ComponentType = componentType;
            ComponentName = componentName;
        }

        public Type ComponentType { get; }
        public string ComponentName { get; }
    }
}