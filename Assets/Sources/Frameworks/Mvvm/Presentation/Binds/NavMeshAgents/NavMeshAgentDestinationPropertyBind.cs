using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.NavMeshAgents;
using UnityEngine;
using UnityEngine.AI;

namespace Presentation.Frameworks.Mvvm.Binds.NavMeshAgents
{
    public class NavMeshAgentDestinationPropertyBind : BindableViewProperty<Vector3>,
        INavMeshAgentDestinationPropertyBind
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        public override Vector3 BindableProperty
        {
            get => _navMeshAgent.destination;
            set => _navMeshAgent.destination = value;
        }
    }
}