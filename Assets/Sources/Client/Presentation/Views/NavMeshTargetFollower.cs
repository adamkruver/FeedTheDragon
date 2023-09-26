using UnityEngine;
using UnityEngine.AI;

namespace Sources.Client.Presentation.Views
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshTargetFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (_target == null)
                return;

            _agent.destination = _target.position;
        }
    }
}