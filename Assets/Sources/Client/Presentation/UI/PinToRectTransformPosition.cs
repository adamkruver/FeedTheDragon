using TMPro;
using UnityEngine;

namespace Sources.Client.Presentation.UI
{
    public class PinToRectTransformPosition : MonoBehaviour
    {
        [SerializeField] private RectTransform _pinPoint;
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        [SerializeField] private float _fontSize = 24;

        private readonly float _preferredScreenHeight = 1080; 
        private readonly Vector3[] _screenCorners = new Vector3[4];

        private RectTransform _rectTransform;

        private void Awake() =>
            _rectTransform = GetComponent<RectTransform>();

        private void Update()
        {
            _rectTransform.anchoredPosition = GetScreenPosition(_pinPoint);
            _textMeshPro.fontSize = _fontSize * Screen.height / _preferredScreenHeight;
        }

        private Vector3 GetScreenPosition(RectTransform uiElement)
        {
            uiElement.GetWorldCorners(_screenCorners);

            return _screenCorners[0];
        }
    }
}