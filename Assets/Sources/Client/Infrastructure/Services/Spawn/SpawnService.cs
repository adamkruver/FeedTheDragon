using System;
using System.Collections.Generic;
using Sources.Client.Controllers.Ingredients.Signals;
using Sources.Client.Controllers.NPCs.Ogres.Signals;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.NPCs;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;
using Sources.Client.Presentation.Views.SpawnPoints;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Client.Infrastructure.Services.Spawn
{
    public class SpawnService<TType, TSpawnerBase> where TSpawnerBase : SpawnPointBase
    {
        private readonly ISignalBus _signalBus;
        private readonly Dictionary<Type, Func<object, Vector3, ISignal>> _spawnSignals;

        public SpawnService(ISignalBus signalBus)
        {
            _signalBus = signalBus;

            _spawnSignals = new Dictionary<Type, Func<object, Vector3, ISignal>>()
            {
                [typeof(IIngredientType)] = (obj, spawnPoint) =>
                    new CreateIngredientSignal(obj as IIngredientType, spawnPoint),

                [typeof(Ogre)] = (_, spawnPoint) => new CreateOgreSignal(spawnPoint)
            };
        }

        public void Spawn()
        {
            TSpawnerBase[] spawnPoints = Object.FindObjectsOfType<TSpawnerBase>();
            
            foreach (TSpawnerBase spawnPoint in spawnPoints)
            {
                dynamic signal = _spawnSignals[typeof(TType)].Invoke(spawnPoint.Type, spawnPoint.Position);
                
                _signalBus.Handle(signal);
            }
        }
    }
}