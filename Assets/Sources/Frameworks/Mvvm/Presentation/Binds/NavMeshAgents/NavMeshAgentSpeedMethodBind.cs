using Domain.Frameworks.Mvvm.Methods;
using PresentationInterfaces.Frameworks.Mvvm.Binds.NavMeshAgents;
using UnityEngine;
using UnityEngine.AI;

namespace Presentation.Frameworks.Mvvm.Binds.NavMeshAgents
{
    public class NavMeshAgentSpeedMethodBind : BindableViewMethod<float>, INavMeshAgentSpeedMethodBind
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private void Update() => 
            BindingCallback.Invoke(_navMeshAgent.velocity.magnitude);
    }
}