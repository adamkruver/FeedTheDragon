using UnityEngine;

namespace Sources.Client.InfrastructureInterfaces.Providers
{
    public interface IResourceLoader
    {
        public T Load<T>(string path, string name) where T : Object;
    }
}