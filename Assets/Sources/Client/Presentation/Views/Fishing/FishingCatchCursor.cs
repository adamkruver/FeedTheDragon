using UnityEngine;

namespace Sources.Client.Presentation.Views.Fishing
{
    public class FishingCatchCursor : MonoBehaviour
    {
        [SerializeField] private RectTransform _widthBorderRectTransform;

        [field: SerializeField] public float Height { get; private set; } = 100;

        private RectTransform _rectTransform;

        private void Awake() =>
            _rectTransform = GetComponent<RectTransform>();

        public void SetSize(Vector2 size) =>
            _widthBorderRectTransform.sizeDelta = size;

        public void SetPosition(Vector2 position) =>
            _rectTransform.anchoredPosition = position;

        public void Enable() =>
            gameObject.SetActive(true);

        public void Disable() =>
            gameObject.SetActive(false);
    }
}