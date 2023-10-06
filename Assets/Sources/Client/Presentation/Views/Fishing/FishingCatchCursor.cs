using UnityEngine;

namespace Sources.Client.Presentation.Views.Fishing
{
    public class FishingCatchCursor : MonoBehaviour
    {
        [SerializeField] private RectTransform _sizeRectTransform;
        [SerializeField] private RectTransform _widthRectTransform;
        [SerializeField] private RectTransform _widthBorderRectTransform;

        private RectTransform _rectTransform;

        public Vector2 Position => _rectTransform.anchoredPosition;

        private void Awake() =>
            _rectTransform = GetComponent<RectTransform>();

        public void SetSize(Vector2 size)
        {
            _sizeRectTransform.sizeDelta = new Vector2(size.x, _sizeRectTransform.sizeDelta.y);
            _widthRectTransform.sizeDelta = new Vector2(size.x, _widthRectTransform.sizeDelta.y);
            _widthBorderRectTransform.sizeDelta = new Vector2(size.x, _widthBorderRectTransform.sizeDelta.y);
        }

        public void SetPosition(Vector2 position) =>
            _rectTransform.anchoredPosition = position;

        public void Enable() =>
            gameObject.SetActive(true);

        public void Disable() =>
            gameObject.SetActive(false);
    }
}