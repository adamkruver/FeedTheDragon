using Sources.Client.App.Configs;
using UnityEngine;

namespace Sources.Client.Infrastructure.Data.Providers
{
    public class EnvironmentDataProvider
    {
        private readonly string _environmentConfigPath = "Environment";

        public Environment Load() =>
            JsonUtility.FromJson<Environment>(Resources.Load<TextAsset>(_environmentConfigPath).text);
    }
}