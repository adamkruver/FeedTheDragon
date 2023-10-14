using System;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.InfrastructureInterfaces.Data;
using Sources.Client.InfrastructureInterfaces.Services.AudioPlayers;
using Sources.Client.UseCases.Common.Components.FirstContacts.Queries;
using UnityEngine;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class FirstContactViewModelComponent : IViewModelComponent
    {
        private readonly IAudioPlayerService _audioPlayerService;
        private readonly IKnownTypeAudioClipProvider _knownTypeAudioClipProvider;
        private readonly LiveData<Type> _lastKnownType;

        public FirstContactViewModelComponent(
            int id,
            IAudioPlayerService audioPlayerService,
            IKnownTypeAudioClipProvider knownTypeAudioClipProvider,
            GetLastKnownTypeQuery getLastKnownTypeQuery
        )
        {
            _audioPlayerService = audioPlayerService;
            _knownTypeAudioClipProvider = knownTypeAudioClipProvider;
            _lastKnownType = getLastKnownTypeQuery.Handle(id);
        }

        public void Enable() =>
            _lastKnownType.Observe(OnChangeLastKnowType);

        public void Disable() =>
            _lastKnownType.Unobserve(OnChangeLastKnowType);
        
        private void OnChangeLastKnowType(Type knownType)
        {
            if(knownType == null)
                return;
            
            AudioClip audioClip = _knownTypeAudioClipProvider.Provide(knownType);
            
            if (audioClip != null)
                _audioPlayerService.Add(audioClip);
        }
    }
}