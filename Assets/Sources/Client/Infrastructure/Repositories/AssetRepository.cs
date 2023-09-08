using System.Collections.Generic;
using Sources.Client.InfrastructureInterfaces.Repositories;
using UnityEngine;

namespace Sources.Client.Infrastructure.Repositories
{
    public class AssetRepository<T> : IAssetRepository<T> where T : Object
    {
        private readonly Dictionary<string, T> _assets = new Dictionary<string, T>();

        public void Set(string path, T asset) =>
            _assets[path] = asset;

        public T Get(string path) =>
            _assets.ContainsKey(path)
                ? _assets[path]
                : null;
    }
}