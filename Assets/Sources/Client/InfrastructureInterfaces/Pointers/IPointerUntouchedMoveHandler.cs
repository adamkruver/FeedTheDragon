using UnityEngine;

namespace Sources.Client.InfrastructureInterfaces.Pointers
{
    public interface IPointerUntouchedMoveHandler
    {
        void OnMove(Vector3 position);
    }
}