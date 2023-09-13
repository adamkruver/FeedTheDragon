using Domain.Frameworks.Mvvm.Properties;
using NodeCanvas.BehaviourTrees;
using Sources.Client.PresentationInterfaces.Binds.BehaviourTrees;
using UnityEngine;

namespace Sources.Client.Presentation.Binds.BehaviourTrees
{
    public class BehaviourTreeOwnerPropertyBind : BindableViewProperty<BehaviourTreeOwner>, IBehaviourTreeOwnerPropertyBind
    {
        [SerializeField] private BehaviourTreeOwner _behaviourTree;

        public override BehaviourTreeOwner BindableProperty
        {
            get => _behaviourTree;
            set { return; }
        }
    }
}