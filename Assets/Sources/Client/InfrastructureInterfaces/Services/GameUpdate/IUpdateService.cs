using System;

namespace Sources.Client.InfrastructureInterfaces.Services.GameUpdateService
{
    public interface IUpdateService
    {
        event Action<float> Updating;
    }
}