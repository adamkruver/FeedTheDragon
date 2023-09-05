using Sources.Client.Domain.Characters;
using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Characters;
using UnityEngine;

namespace Sources.Client.Infrastructure.Factories.Domain.Characters
{
    public class PeasantFactory : ICharacterFactory
    {
        public Character Create(int id, Vector3 spawnPosition)
        {
            return new Character(
                id,
                new LookDirectionComponent(Vector3.zero),
                new PositionComponent(spawnPosition),
                new SpeedComponent(10)
            ); //todo Make spawn info
        }
    }
}