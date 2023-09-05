using Sources.Infrastructure.Signals;
using Sources.InfrastructureInterfaces.SignalBus;
using UnityEngine;

namespace Sources.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private LayerMask _layerMask;

        private CharacterController _characterController;
        private Transform _transform;
        private UnityEngine.Camera _camera;
    
        private ISignalBus _signalBus;

        private Vector3 _direction;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _transform = GetComponent<Transform>();
            _camera = UnityEngine.Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, _layerMask))
                {
                    _direction = raycastHit.point - _transform.position;
                    _direction.y = 0;

                    _signalBus?.Handle(new CharacterMoveSignal(_speed * Time.deltaTime * _direction.normalized));
                }
            }
        }

        public void Init(ISignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Move(Vector3 moveDelta)
        {
            _characterController.Move(moveDelta);
        }
    }
}