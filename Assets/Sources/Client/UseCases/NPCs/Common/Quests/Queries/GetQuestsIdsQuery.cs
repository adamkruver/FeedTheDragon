using System;
using System.Linq;
using Sources.Client.Domain;
using Sources.Client.Domain.NPCs.Components;
using Sources.Client.Domain.NPCs.Ogres;
using Sources.Client.Domain.Progresses;
using Sources.Client.InfrastructureInterfaces.Repositories;
using UnityEngine;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.UseCases.NPCs.Common.Quests.Queries
{
    public class GetQuestsIdsQuery
    {
        private readonly IEntityRepository _entityRepository;

        public GetQuestsIdsQuery(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public LiveData<int[]> Handle(int questHolderId, int questOwnerId)
        {
            Debug.Log(questHolderId);
            
            if (_entityRepository.Get(questHolderId) is not Ogre ogre)
                throw new InvalidCastException();

            if (ogre.TryGetComponent(out Mission mission) == false)
                throw new InvalidOperationException();

            return mission.QuestIds; // todo: Add QuestOwner filter
        }
    }
}