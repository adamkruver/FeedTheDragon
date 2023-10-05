using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Client.InfrastructureInterfaces.Providers;
using UnityEngine;
using CameraType = Sources.Client.Presentation.Cameras.CameraType;

namespace Sources.Client.Infrastructure.Providers
{
    public class CameraProvider : ICameraProvider
    {
        private readonly Dictionary<Type, CameraType> _cameras = new Dictionary<Type, CameraType>();
        
        public IEnumerable<CameraType> CameraTypes => _cameras.Values;

        public void Register(CameraType cameraType) => 
            _cameras[cameraType.GetType()] = cameraType;

        public CameraType GetCameraType<T>() where T : CameraType =>
            _cameras[typeof(T)];
        
        public Camera Get<T>() where T : CameraType =>
            _cameras[typeof(T)].Camera;
    }
}