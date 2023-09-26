using Sources.Client.Infrastructure.Providers;
using UnityEngine;
using CameraType = Sources.Client.Presentation.Cameras.CameraType;

namespace Sources.Client.Infrastructure.Factories.Presentation.Cameras
{
    public class CameraProviderFactory
    {
        public CameraProvider Create()
        {
            CameraProvider cameraProvider = new CameraProvider();

            CameraType[] cameraTypes = Object.FindObjectsOfType<CameraType>();

            foreach (CameraType cameraType in cameraTypes)
                cameraProvider.Register(cameraType);
            
            return cameraProvider;
        }
    }
}