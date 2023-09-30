using System.Collections;
using Sources.Client.Presentation.Views.Fishing;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Client.Infrastructure.Services.Spawn
{
    public class CatchFishService : MonoBehaviour
    {
        [SerializeField] private RectTransform _catchCursorRectTransform;
        [SerializeField] private RectTransform _cursor;
        [SerializeField] private Image _catchCursor;
        [SerializeField] private FishingLine _fishingLine;
        [SerializeField] private FishingSightSphereCaster _fishingSightSphereCaster;
        [SerializeField] private Camera _camera;
        [SerializeField] private RectTransform _water;
        
        private Coroutine _catchJob;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                if (_fishingSightSphereCaster.TryGetFish(Input.mousePosition, 1f, out FishView fish))
                {
                    _catchCursorRectTransform.gameObject.SetActive(true);
                    _cursor.gameObject.SetActive(false);
                    StartCatch(fish);
                }

            if (Input.GetMouseButtonUp(0))
            {
                _catchCursorRectTransform.gameObject.SetActive(false);
                _cursor.gameObject.SetActive(true);
            }
        }

        private void StartCatch(FishView fish)
        {
            Stop();

            _catchJob = StartCoroutine(Catch(fish));
        }

        private void Stop()
        {
            if (_catchJob == null)
                return;

            StopCoroutine(_catchJob);
        }

        private IEnumerator Catch(FishView fish)
        {
            Vector3 position = Input.mousePosition;
            float radius = 5f;

            while (position.y < _water.sizeDelta.y && _fishingSightSphereCaster.HasCollision(position, fish, radius))
            {
                Vector3 nextPosition = position + Vector3.up;
                position = Vector3.MoveTowards(position, nextPosition, 500 * Time.deltaTime);
                if (Physics.Raycast(_camera.ScreenPointToRay(position), out RaycastHit hit, Mathf.Infinity,
                        1 << LayerMask.NameToLayer("TransparentFX")))
                {
                    fish.SetEndPoint(hit.point);
                    _fishingLine.SetEndPoint(hit.point);
                }
                radius = Mathf.Max(radius - .05f, 2);

                _catchCursorRectTransform.anchoredPosition = position;
                _catchCursor.transform.localScale = Vector3.one * radius;

                yield return null;
            }

            Stop();
        }
    }
}