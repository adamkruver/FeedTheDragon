using UnityEngine;
using CameraType = Sources.Client.Presentation.Cameras.CameraType;

namespace Sources.Client.InfrastructureInterfaces.Providers
{
    public interface ICameraProvider
    {
        public Camera Get<T>() where T : CameraType;
    }
}