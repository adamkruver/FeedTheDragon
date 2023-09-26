using Sources.Client.Domain.Components;
using Sources.Client.Domain.NPCs;
using Sources.Client.Domain.NPCs.Ogres;
using Sources.Client.Domain.Progresses;

namespace Sources.Client.Infrastructure.Factories.Domain.NPCs
{
    public class OgreFactory
    {
        public Ogre Create(int id, OgreSpawnInfo spawnInfo)
        {
            Ogre ogre = new Ogre(id);
            
            ogre.AddComponent(new VisibilityComponent(true));
            ogre.AddComponent(new PositionComponent(spawnInfo.Position));
            
            return ogre;
        } 
    }
}