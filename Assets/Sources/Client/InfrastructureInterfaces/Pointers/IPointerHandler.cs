using UnityEngine;

namespace Sources.Client.InfrastructureInterfaces.Pointers
{
    public interface IPointerHandler : IPointerUntouchedMoveHandler
    {
        void OnTouchStart(Vector3 position);
        void OnTouchEnd(Vector3 position);
    }
}