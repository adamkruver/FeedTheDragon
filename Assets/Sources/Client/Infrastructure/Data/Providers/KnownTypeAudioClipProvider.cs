using System;
using Sources.Client.InfrastructureInterfaces.Data;
using UnityEngine;
using Environment = Sources.Client.App.Configs.Environment;

namespace Sources.Client.Infrastructure.Data.Providers
{
    public class KnownTypeAudioClipProvider : IKnownTypeAudioClipProvider
    {
        private readonly Environment _environment;

        public KnownTypeAudioClipProvider(Environment environment) =>
            _environment = environment;

        public AudioClip Provide(Type type) =>
            Resources.Load<AudioSource>(_environment.AudioClips.FirstContacts[type.Name])?.clip;
    }
}