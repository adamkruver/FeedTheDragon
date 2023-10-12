using System;
using UnityEngine;

namespace Sources.Client.Presentation.UI
{
    public class FitByScreen : MonoBehaviour
    {
        private RectTransform _rectTransform;

        private void Awake() =>
            _rectTransform = GetComponent<RectTransform>();

        private void Update() => 
            _rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}