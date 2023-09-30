using Sources.Client.Infrastructure.Services.CoroutineRunners;
using Sources.Client.Presentation.Coroutines;
using UnityEngine;

namespace Sources.Client.Infrastructure.Factories.Services.CoroutineRunners
{
    public class CoroutineMonoRunnerFactory
    {
        private CoroutineMonoBehaviour _coroutineMonoBehaviour;

        public CoroutineMonoRunnerFactory()
        {
            _coroutineMonoBehaviour = new GameObject(nameof(CoroutineMonoRunner))
                .AddComponent<CoroutineMonoBehaviour>();
        }

        public CoroutineMonoRunner Create()
        {
            return new CoroutineMonoRunner(_coroutineMonoBehaviour);
        }
    }
}