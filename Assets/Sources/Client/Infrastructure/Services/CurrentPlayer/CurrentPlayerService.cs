﻿using Sources.Client.Domain.Characters;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;

namespace Sources.Client.Infrastructure.Services.CurrentPlayer
{
    public class CurrentPlayerService : ICurrentPlayerService
    {
        public int CharacterId { get; set; }
    }
}