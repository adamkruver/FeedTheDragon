using Sources.Client.App.Configs;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Data.Providers;
using Sources.Client.Infrastructure.Services.AudioPlayers;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components.FirstContacts.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components
{
    public class FirstContactViewModelComponentFactory
    {
        private readonly AudioPlayerService _audioPlayerService;
        private readonly KnownTypeAudioClipProvider _knownTypeAudioClipProvider;
        private readonly GetLastKnownTypeQuery _getLaseKnownQuery;

        public FirstContactViewModelComponentFactory(
            IEntityRepository entityRepository,
            AudioPlayerService audioPlayerService,
            Environment environment
        )
        {
            _audioPlayerService = audioPlayerService;
            _knownTypeAudioClipProvider = new KnownTypeAudioClipProvider(environment);
            _getLaseKnownQuery = new GetLastKnownTypeQuery(entityRepository);
        }

        public FirstContactViewModelComponent Create(int id) =>
            new FirstContactViewModelComponent(
                id,
                _audioPlayerService,
                _knownTypeAudioClipProvider,
                _getLaseKnownQuery
            );
    }
}