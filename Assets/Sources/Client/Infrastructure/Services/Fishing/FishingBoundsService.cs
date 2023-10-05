using System;
using Sources.Client.Frameworks.StateMachines;
using Sources.Client.Infrastructure.Services.Raycasts;
using Sources.Client.Utils;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Fishing
{
    public class FishingBoundsService : IUpdatable
    {
        private readonly RectTransform _boundsRectTransform;
        private readonly ScreenRayCastService _screenRayCastService;

        public FishingBoundsService(Camera camera, RectTransform boundsRectTransform)
        {
            _boundsRectTransform = boundsRectTransform;
            _screenRayCastService = new ScreenRayCastService(camera, LayerConstants.TransparentFXMask);
        }

        public Bounds Bounds { get; private set; }
        public Vector2 CanvasBounds => _boundsRectTransform.sizeDelta;

        public bool ContainScreenPoint(Vector3 screenPoint)
        {
            if (screenPoint.x < 0 || screenPoint.x > CanvasBounds.x)
                return false;

            if (screenPoint.y < 0 || screenPoint.y > CanvasBounds.y)
                return false;

            return true;
        }

        public void Update(float deltaTime)
        {
            Vector3 screenLeftBottom = Vector3.zero;
            Vector3 screenRightTop = CanvasBounds;

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