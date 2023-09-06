using Sources.Client.Domain.Characters;
using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Characters;
using UnityEngine;

namespace Sources.Client.Infrastructure.Factories.Domain.Characters
{
    public class PeasantFactory : ICharacterFactory
    {
        public Character Create(int id, CharacterSpawnInfo spawnInfo)//todo Make spawn info
        {
            Character character = new Character(id);
            
            character.AddComponent(new VisibilityComponent(true));
            character.AddComponent(new LookDirectionComponent(Vector3.zero));
            character.AddComponent(new PositionComponent(spawnInfo.Position));
            character.AddComponent(new SpeedComponent(10));

            return character;
        }
    }
}