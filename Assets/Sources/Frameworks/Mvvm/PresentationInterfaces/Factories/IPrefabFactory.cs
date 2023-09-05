using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PresentationInterfaces.Frameworks.Mvvm.Factories
{
    public interface IPrefabFactory
    {
        T Create<T>(string prefabPath = "") where T : MonoBehaviour;
        T Create<T>(Type viewType, string prefabPath = "") where T : Object;
    }
}