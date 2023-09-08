using UnityEngine;

namespace Sources.Client.InfrastructureInterfaces.Repositories
{
    public interface IAssetRepository<T> where T : Object
    {
        void Set(string path, T asset);
        T Get(string path);
    }
}