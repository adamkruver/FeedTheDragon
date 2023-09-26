using Sources.Client.Controllers.Characters;
using Sources.Client.Infrastructure.Services.Pointers.Handlers;
using Sources.Client.Infrastructure.Services.Terrains;
using Sources.Client.InfrastructureInterfaces.Providers;
using UnityEngine;

namespace Sources.Client.Infrastructure.Factories.Services.Pointers.Handlers
{
    public class CharacterPointerHandlerFactory
    {
        private readonly TerrainService _terrainService;

        public CharacterPointerHandlerFactory(ICameraProvider cameraProvider) =>
            _terrainService = new TerrainService(cameraProvider);

        public CharacterPointerHandler Create(CharacterMovementService characterMovementService) =>
            new CharacterPointerHandler(_terrainService, characterMovementService);
    }
}