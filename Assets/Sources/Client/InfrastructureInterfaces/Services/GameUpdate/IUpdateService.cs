using System;

namespace Sources.Client.InfrastructureInterfaces.Services.GameUpdate
{
    public interface IUpdateService
    {
        event Action<float> Updating;
    }
}