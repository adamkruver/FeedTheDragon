using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Client.Utils
{
    public class DialogeView : MonoBehaviour
    {
        [SerializeField] private GameObject _vertical;
        [SerializeField] private GameObject _horizontal;

        private void Update()
        {
            if (Screen.width <= Screen.height)
            {
                _vertical.gameObject.SetActive(true);
                _horizontal.gameObject.SetActive(false);
            }
            else
            {
                _vertical.gameObject.SetActive(false);
                _horizontal.gameObject.SetActive(true);
            }
        }
    }
}