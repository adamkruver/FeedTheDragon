using Domain.Frameworks.Mvvm.Methods;
using Sources.Client.PresentationInterfaces.Binds.BehaviourTrees;

namespace Sources.Client.Presentation.Binds.BehaviourTrees
{
    public class ActionMethodBind : BindableViewMethod<bool>, IActionMethodBind
    {
        public void Do() => 
            BindingCallback?.Invoke(true);
    }
}