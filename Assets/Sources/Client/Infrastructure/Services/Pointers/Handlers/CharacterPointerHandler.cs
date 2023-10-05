using Sources.Client.Controllers.Characters;
using Sources.Client.Infrastructure.Services.Terrains;
using Sources.Client.InfrastructureInterfaces.Pointers;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Pointers.Handlers
{
    public class CharacterPointerHandler : IPointerHandler
    {
        private readonly TerrainService _terrainService;
        private readonly CharacterMovementService _characterMovementService;

        public CharacterPointerHandler(TerrainService terrainService, CharacterMovementService characterMovementService)
        {
            _terrainService = terrainService;
            _characterMovementService = characterMovementService;
        }

        public void OnTouchStart(Vector3 position) => 
            OnMove(position);

        public void OnMove(Vector3 position)
        {
            if (_terrainService.TryGetRaycastHit(position, out Vector3 hitPoint) == false)
            {
                OnTouchEnd(position);
                
                return;
            }

            _characterMovementService.MoveTo(hitPoint);
        }

        public void OnTouchEnd(Vector3 position) => 
            _characterMovementService.Stop();
    }
}