using System;
using Sources.Client.InfrastructureInterfaces.Services.GameUpdate;

namespace Sources.Client.Infrastructure.Services.GameUpdate
{
    public class GameUpdateService : IUpdateService
    {
        public event Action<float> Updating;

        public void Update(float deltaTime) =>
            Updating?.Invoke(deltaTime);
    }
}