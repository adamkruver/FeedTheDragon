using System.Collections.Generic;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Providers;
using UnityEngine;

namespace Sources.Client.Infrastructure.Providers
{
    public class ResourceLoader : IResourceLoader
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