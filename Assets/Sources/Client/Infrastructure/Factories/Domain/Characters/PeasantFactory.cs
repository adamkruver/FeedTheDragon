using Sources.Client.Domain.Characters;
using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Characters;
using UnityEngine;

namespace Sources.Client.Infrastructure.Factories.Domain.Characters
{
    public class PeasantFactory : IPeasantFactory
    {
        public Character Create(int id, PeasantSpawnInfo spawnInfo)
        {
            Character character = new Character(id);

            character.AddComponent(new VisibilityComponent(true));
            character.AddComponent(new LookDirectionComponent(Vector3.zero));
            character.AddComponent(new PositionComponent(spawnInfo.Position));
            character.AddComponent(new SpeedComponent(10f)); //TODO: Move values into Config
            
            return character;
        }
    }
}