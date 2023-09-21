using Sources.Client.Infrastructure.Services.Terrains;
using Sources.Client.InfrastructureInterfaces.Pointers;
using UnityEngine;
using CharacterController = Sources.Client.Controllers.Characters.CharacterController;

namespace Sources.Client.Infrastructure.Services.Pointers.Handlers
{
    public class CharacterPointerHandler : IPointerHandler
    {
        private readonly TerrainService _terrainService;
        private readonly CharacterController _characterController;

        public CharacterPointerHandler(TerrainService terrainService, CharacterController characterController)
        {
            _terrainService = terrainService;
            _characterController = characterController;
        }

        public void OnStart(Vector3 position) => 
            OnMove(position);

        public void OnMove(Vector3 position)
        {
            if(_terrainService.TryGetRaycastHit(position, out Vector3 hitPoint) == false)
                return;

            _characterController.MoveTo(hitPoint);
        }

        public void OnFinish(Vector3 position) => 
            _characterController.Stop();
    }
}