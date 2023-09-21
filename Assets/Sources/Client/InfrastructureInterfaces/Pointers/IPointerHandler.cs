using UnityEngine;

namespace Sources.Client.InfrastructureInterfaces.Pointers
{
    public interface IPointerHandler
    {
        void OnStart(Vector3 position);
        void OnMove(Vector3 position);
        void OnFinish(Vector3 position);
    }
}