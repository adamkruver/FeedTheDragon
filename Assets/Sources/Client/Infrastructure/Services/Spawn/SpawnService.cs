using Sources.Client.Controllers.Ingredients.Signals;
using Sources.Client.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Spawners;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Spawn
{
    public class SpawnService<T> where T : IIngredientType
    {
        private readonly ISignalBus _signalBus;

        public SpawnService(ISignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Spawn()
        {
            SpawnerBase[] spawnPoints = Object.FindObjectsOfType<SpawnerBase>();

            foreach (SpawnerBase spawnPoint in spawnPoints)
            {
                if (spawnPoint.Type is T type)
                    _signalBus.Handle(new CreateIngredientSignal(type, spawnPoint.Position));
            }
        }
    }
}