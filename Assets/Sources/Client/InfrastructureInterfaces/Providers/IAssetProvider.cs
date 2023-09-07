using UnityEngine;

namespace Sources.Client.InfrastructureInterfaces.Providers
{
    public interface IAssetProvider
    {
        T Instantiate<T>(string path, string name) where T : MonoBehaviour;
    }
}