using Sources.Client.Domain.Components;
using Sources.Client.Domain.NPCs.Dragons;

namespace Sources.Client.Infrastructure.Factories.Domain.NPCs
{
    public class DragonFactory
    {
        public Dragon Create(int id, DragonSpawnInfo spawnInfo)
        {
            Dragon dragon = new Dragon(id);

            dragon.AddComponent(new VisibilityComponent(true));
            dragon.AddComponent(new PositionComponent(spawnInfo.Position));
            
            return dragon;
        } 
    }
}