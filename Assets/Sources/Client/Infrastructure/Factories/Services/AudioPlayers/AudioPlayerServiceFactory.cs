using System;
using Presentation.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.Factories;
using Sources.Client.Infrastructure.Services.AudioPlayers;
using Sources.Client.Presentation.Views.AudioPlayers;
using Environment = Sources.Client.App.Configs.Environment;

namespace Sources.Client.Infrastructure.Factories.Services.AudioPlayers
{
    public class AudioPlayerServiceFactory
    {
        private readonly string _audioPlayerKey = "CharacterAudioPlayer";
        private readonly Environment _environment;
        private readonly IPrefabFactory _prefabFactory = new PrefabFactory();

        public AudioPlayerServiceFactory(Environment environment) =>
            _environment = environment;

        public AudioPlayerService Create()
        {
            AudioPlayer audioPlayer =
                _prefabFactory.Create<AudioPlayer>(_environment.AudioPlayers[_audioPlayerKey])
                ?? throw new NullReferenceException(_audioPlayerKey);

            return new AudioPlayerService(audioPlayer);
        }
    }
}