using Sources.Client.Domain.Characters;

namespace Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer
{
    public interface ICurrentPlayerService
    {
        public Character Character { get; }
    }
}