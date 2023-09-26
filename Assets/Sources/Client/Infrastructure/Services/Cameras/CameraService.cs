using System;
using System.Collections.Generic;
using Sources.Client.Infrastructure.Providers;
using Sources.Client.InfrastructureInterfaces.Services.Cameras;
using UnityEngine;
using CameraType = Sources.Client.Presentation.Cameras.CameraType;

namespace Sources.Client.Infrastructure.Services.Cameras
{
    public class CameraService : ICameraService
    {
        private readonly CameraProvider _cameraProvider;

        public CameraService(CameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
        }

        public void Enable<T>() where T : CameraType
        {
            foreach (CameraType cameraType in _cameraProvider.CameraTypes)
                cameraType.Disable();

            _cameraProvider.GetCameraType<T>().Enable();
        }
    }
}