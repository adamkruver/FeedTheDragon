using Sources.Client.Infrastructure.Services.Pointers.Handlers;
using Sources.Client.Infrastructure.Services.Terrains;
using UnityEngine;
using CharacterController = Sources.Client.Controllers.Characters.CharacterController;

namespace Sources.Client.Infrastructure.Factories.Services.Pointers.Handlers
{
    public class CharacterPointerHandlerFactory
    {
        private readonly TerrainService _terrainService;

        public CharacterPointerHandlerFactory(Camera camera) =>
            _terrainService = new TerrainService(camera);

        public CharacterPointerHandler Create(CharacterController characterController) =>
            new CharacterPointerHandler(_terrainService, characterController);
    }
}