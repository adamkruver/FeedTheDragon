using Sources.Client.Presentation.Cameras;

namespace Sources.Client.InfrastructureInterfaces.Services.Cameras
{
    public interface ICameraService
    {
        public void Enable<T>() where T : CameraType;
    }
}