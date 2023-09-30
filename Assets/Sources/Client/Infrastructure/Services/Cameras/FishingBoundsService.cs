using System;
using Sources.Client.Frameworks.StateMachines;
using Sources.Client.Infrastructure.Services.Raycasts;
using Sources.Client.Utils;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Cameras
{
    public class FishingBoundsService : IUpdatable
    {
        private readonly ScreenRayCastService _screenRayCastService;

        public FishingBoundsService(Camera camera)
        {
            _screenRayCastService = new ScreenRayCastService(camera, LayerConstants.TransparentFXMask);
        }

        public Bounds Bounds { get; private set; }

        public void Update(float deltaTime)
        {
            float width = Screen.width;
            float height = Screen.height;

            Vector3 screenLeftBottom = Vector3.zero;
            Vector3 screenRightTop = new Vector3(width, height);

            if (_screenRayCastService.TryRaycast(screenLeftBottom, out RaycastHit leftBottomHit) == false)
                throw new InvalidOperationException();

            if (_screenRayCastService.TryRaycast(screenRightTop, out RaycastHit rightTopHit) == false)
                throw new InvalidOperationException();

            Vector3 center = (leftBottomHit.point + rightTopHit.point) / 2f;

            Vector3 size = new Vector3()
            {
                x = Mathf.Abs(rightTopHit.point.x - leftBottomHit.point.x),
                y = Mathf.Abs(rightTopHit.point.y - leftBottomHit.point.y),
                z = Mathf.Abs(rightTopHit.point.z - leftBottomHit.point.z)
            };
            
            Bounds = new Bounds(center, size);
        }
    }
}