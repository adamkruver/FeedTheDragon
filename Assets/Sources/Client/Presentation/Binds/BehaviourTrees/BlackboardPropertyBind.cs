using Domain.Frameworks.Mvvm.Properties;
using NodeCanvas.Framework;
using Sources.Client.PresentationInterfaces.Binds.BehaviourTrees;
using UnityEngine;

namespace Sources.Client.Presentation.Binds.BehaviourTrees
{
    public class BlackboardPropertyBind : BindableViewProperty<Blackboard>, IBlackboardPropertyBind
    {
        [SerializeField] private Blackboard _blackboard;

        public override Blackboard BindableProperty
        {
            get => _blackboard;
            set { return; }
        }
    }
}