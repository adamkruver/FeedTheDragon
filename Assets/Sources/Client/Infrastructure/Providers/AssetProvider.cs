using System.Collections.Generic;
using Sources.Client.InfrastructureInterfaces.Providers;
using UnityEngine;

namespace Sources.Client.Infrastructure.Providers
{
    public class AssetProvider : IAssetProvider
    {
        private Dictionary<string, MonoBehaviour> _prefabs = new Dictionary<string, MonoBehaviour>();

        public T Instantiate<T>(string path, string name) where T : MonoBehaviour
        {
            string resultPath = path + name;

            if (_prefabs.ContainsKey(resultPath) == false)
                _prefabs[resultPath] = Resources.Load<T>(resultPath);
            
            return (T)Object.Instantiate(_prefabs[resultPath]);
        }
    }
}