using UnityEngine;
using UnityEngine.UI;

namespace Sources.Client.Presentation.Views.Fishing
{
    public class FishingLineCursor : MonoBehaviour
    {
        [SerializeField] private Image[] _images;
        [SerializeField] private int _screenHeight = 1024;
        [SerializeField] private Color _noFishColor;
        [SerializeField] private Color _hasFishColor;

        private RectTransform _rectTransform;

        public bool CanCatch
        {
            set => ApplyColor(value);
        }

        private void Awake() =>
            _rectTransform = GetComponent<RectTransform>();

        public void Enable() =>
            gameObject.SetActive(true);

        public void Disable() =>
            gameObject.SetActive(false);

        public void SetPosition(Vector3 mousePosition)
        {
            _rectTransform.anchoredPosition = mousePosition;
            _rectTransform.localScale = Screen.height * Vector3.one / _screenHeight;
        }

        private void ApplyColor(bool canCatch)
        {
            Debug.Log(canCatch);

            Color color = canCatch
                ? _hasFishColor
                : _noFishColor;

            foreach (Image image in _images)
                image.color = color;
        }
    }
}