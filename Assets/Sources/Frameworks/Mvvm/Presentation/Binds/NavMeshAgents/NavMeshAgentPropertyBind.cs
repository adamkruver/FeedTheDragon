using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.NavMeshAgents;
using UnityEngine;
using UnityEngine.AI;

namespace Presentation.Frameworks.Mvvm.Binds.NavMeshAgents
{
    public class NavMeshAgentPropertyBind : BindableViewProperty<NavMeshAgent>, INavMeshAgentPropertyBind
    {
        [SerializeField] private NavMeshAgent _agent;

        public override NavMeshAgent BindableProperty
        {
            get => _agent;
            set { }
        }
    }
}