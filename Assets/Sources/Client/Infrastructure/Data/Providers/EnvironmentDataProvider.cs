using Sources.Client.App.Configs;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Sources.Client.Infrastructure.Data.Providers
{
    public class EnvironmentDataProvider
    {
        private readonly string _environmentConfigPath = "Environment";

        public Environment Load() =>
            JsonConvert.DeserializeObject<Environment>(Resources.Load<TextAsset>(_environmentConfigPath).text);
    }
}