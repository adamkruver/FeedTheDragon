using Sources.Client.Infrastructure.Services.Pointers;
using UnityEngine;

namespace Sources.Client.Presentation.Views.Fishing
{
    public class FishingLineRaycaster : MonoBehaviour
    {
        [SerializeField] private FishingLine _fishingLine;
        [SerializeField] private Camera _camera;

        private PointerUIService _pointerUIService;

        private void Awake()
        {
            _pointerUIService = new PointerUIService();
        }

        private void Update()
        {
            if (_pointerUIService.IsPointerOverUI == false)
                return;

            Vector3 mousePosition = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity,
                    1 << LayerMask.NameToLayer("TransparentFX")))
            {
                _fishingLine.SetEndPoint(raycastHit.point);
            }
        }
    }
}