using Domain.Frameworks.Mvvm.Methods;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Renderers;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Binds.Renderers
{
    public class RendererBoundsMethodBind : BindableViewMethod<Vector4>, IRendererBoundsMethodBind
    {
        [SerializeField] private Renderer _renderer;

        private Camera Camera => Camera.main;

        private void OnEnable() =>
            UpdateBounds();

        private void Update() =>
            UpdateBounds();

        private void UpdateBounds() =>
            BindingCallback?.Invoke(CalculateMinMaxScreenBounds());

        private Vector4 CalculateMinMaxScreenBounds()
        {
            Bounds bounds = _renderer.bounds;

            Vector3[] corners = Get3dCorners(bounds.center, bounds.extents);

            Vector3[] screenPoints = new Vector3[corners.Length];

            for (int i = 0; i < corners.Length; i++)
            {
                Vector3 corner = corners[i];
                screenPoints[i] = Camera.WorldToScreenPoint(corner);
            }

            Vector4 minMax = new Vector4(screenPoints[0].x, screenPoints[0].y, screenPoints[0].x, screenPoints[0].y);

            for (int i = 0; i < screenPoints.Length; i++)
            {
                Vector3 screenPoint = screenPoints[i];

                if (minMax.x > screenPoint.x)
                    minMax.x = screenPoint.x;

                if (minMax.y > screenPoint.y)
                    minMax.y = screenPoint.y;

                if (minMax.z < screenPoint.x)
                    minMax.z = screenPoint.x;

                if (minMax.w < screenPoint.y)
                    minMax.w = screenPoint.y;
            }

            minMax.x /= Screen.width;
            minMax.y /= Screen.height;
            minMax.z /= Screen.width;
            minMax.w /= Screen.height;

            return minMax;
        }

        private Vector3[] Get3dCorners(Vector3 center, Vector3 extents) =>
            new Vector3[]
            {
                new Vector3(center.x + extents.x, center.y + extents.y, center.z + extents.z),
                new Vector3(center.x + extents.x, center.y + extents.y, center.z - extents.z),
                new Vector3(center.x + extents.x, center.y - extents.y, center.z + extents.z),
                new Vector3(center.x + extents.x, center.y - extents.y, center.z - extents.z),
                new Vector3(center.x - extents.x, center.y - extents.y, center.z + extents.z),
                new Vector3(center.x - extents.x, center.y - extents.y, center.z - extents.z),
                new Vector3(center.x - extents.x, center.y + extents.y, center.z + extents.z),
                new Vector3(center.x - extents.x, center.y + extents.y, center.z - extents.z),
            };
    }
}