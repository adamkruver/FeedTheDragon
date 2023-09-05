using Sources.Client.Domain.Characters;
using UnityEngine;

namespace Sources.Client.InfrastructureInterfaces.Factories.Domain.Characters
{
    public interface ICharacterFactory
    {
        Character Create(int id, Vector3 spawnPosition);
    }
}