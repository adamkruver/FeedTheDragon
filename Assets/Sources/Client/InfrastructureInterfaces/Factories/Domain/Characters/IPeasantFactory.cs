using Sources.Client.Domain.Characters;
using UnityEngine;

namespace Sources.Client.InfrastructureInterfaces.Factories.Domain.Characters
{
    public interface IPeasantFactory
    {
        Character Create(int id, PeasantSpawnInfo spawnInfo);
    }
}