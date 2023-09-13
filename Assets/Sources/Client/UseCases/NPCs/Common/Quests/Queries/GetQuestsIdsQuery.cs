using System;
using System.Linq;
using Sources.Client.Domain;
using Sources.Client.Domain.NPCs.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.NPCs.Common.Quests.Queries
{
    public class GetQuestsIdsQuery
    {
        private readonly IEntityRepository _entityRepository;

        public GetQuestsIdsQuery(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public int[] Handle(int questHolderid, int questOwnerId)
        {
            if (_entityRepository.Get(questHolderid) is not Composite questHolder)
                throw new InvalidCastException();

            if (questHolder.TryGetComponents(out Quest[] quests) == false)
                throw new NullReferenceException();
            
            return quests.Select(quest => quest.Id).ToArray(); // todo: Add QuestOwner filter
        }
    }
}