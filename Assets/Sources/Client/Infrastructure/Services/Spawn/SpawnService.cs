using System;
using System.Collections.Generic;
using Sources.Client.Controllers.Enemies.Spiders.Signals;
using Sources.Client.Controllers.Ingredients.Signals;
using Sources.Client.Controllers.NPCs.Dragons.Signals;
using Sources.Client.Controllers.NPCs.Ogres.Signals;
using Sources.Client.Domain.Enemies.Types;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.NPCs.Dragons;
using Sources.Client.Domain.NPCs.Ogres;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.Presentation.Views.SpawnPoints;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Client.Infrastructure.Services.Spawn
{
    public class SpawnService<TType, TSpawnerBase> where TSpawnerBase : SpawnPointBase
    {
        private readonly ISignalBus _signalBus;
        private readonly Dictionary<Type, Action<ISignalBus, object, Vector3>> _spawnStrategies;

        public SpawnService(ISignalBus signalBus)
        {
            _signalBus = signalBus;

            _spawnStrategies = new Dictionary<Type, Action<ISignalBus, object, Vector3>>()
            {
                [typeof(IIngredientType)] = (bus, @object, spawnPoint) =>
                    bus.Handle(new CreateIngredientSignal((IIngredientType)@object, spawnPoint)),

                [typeof(Ogre)] = (bus, @object, spawnPoint) => bus.Handle(new CreateOgreSignal(spawnPoint)),
                [typeof(Dragon)] = (bus, @object, spawnPoint) => bus.Handle(new CreateDragonSignal(spawnPoint)),
                [typeof(Spider)] = (bus, @object, spawnPoint) => bus.Handle(new CreateSpiderSignal(spawnPoint)),
            };
        }

        public void Spawn()
        {
            TSpawnerBase[] spawnPoints = Object.FindObjectsOfType<TSpawnerBase>();

            foreach (TSpawnerBase spawnPoint in spawnPoints)
            {
                _spawnStrategies[typeof(TType)].Invoke(_signalBus, spawnPoint.Type, spawnPoint.Position);
            }
        }
    }
}