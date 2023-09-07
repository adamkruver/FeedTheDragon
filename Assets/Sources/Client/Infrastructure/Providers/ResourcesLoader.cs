using System.Collections.Generic;
using UnityEngine;

namespace Sources.Client.Infrastructure.Providers
{
    public class ResourcesLoader
    {
        private Dictionary<string, Object> _resources = new Dictionary<string, Object>();

        public T Load<T>(string path, string name) where T : Object
        {
            string resultPath = path + name;

            if (_resources.ContainsKey(resultPath) == false)
                _resources[resultPath] = Resources.Load<T>(resultPath);

            return (T)_resources[resultPath];
        }
    }
}