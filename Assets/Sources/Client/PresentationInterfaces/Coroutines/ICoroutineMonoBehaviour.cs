using System.Collections;
using UnityEngine;

namespace Sources.Client.PresentationInterfaces.Coroutines
{
    public interface ICoroutineMonoBehaviour
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        
        void StopCoroutine(Coroutine coroutine);
    }
}