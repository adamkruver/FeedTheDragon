using System;
using System.Collections.Generic;
using PresentationInterfaces.Frameworks.Mvvm.Factories;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Presentation.Frameworks.Mvvm.Factories
{
    public class PrefabFactory : IPrefabFactory
    {
        private readonly Dictionary<string, Object> _resources = new Dictionary<string, Object>();

        public T Create<T>(string prefabPath = "") where T : MonoBehaviour
        {
            T @object = Object.Instantiate((T)GetResource(prefabPath, typeof(T)));
            MeshFilter[] prefabMeshes = ((T)_resources[prefabPath]).GetComponentsInChildren<MeshFilter>(true);
            MeshFilter[] objectMeshes = @object.GetComponentsInChildren<MeshFilter>(true);

            for (int i = 0; i < objectMeshes.Length; i++)
            {
                objectMeshes[i].mesh = prefabMeshes[i].sharedMesh;
                objectMeshes[i].sharedMesh = prefabMeshes[i].sharedMesh;
            }

            return @object;
        }

        public T Create<T>(Type viewType, string prefabPath = "") where T : Object =>
            Object.Instantiate((T)GetResource(prefabPath, viewType));

        private Object GetResource(string prefabPath, Type type)
        {
            if (_resources.ContainsKey(prefabPath) == false)
            {
                Object resource = Resources.Load(prefabPath, type);
                _resources[prefabPath] = resource;
            }

            return _resources[prefabPath];
        }
    }
}