using Sources.Client.Controllers.NPCs.Common;
using Sources.Client.Controllers.NPCs.Common.Actions;
using Sources.Client.Infrastructure.Factories.Domain.NPCs;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;
using Sources.Client.UseCases.NPCs.Common.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.SignalControllers
{
    public class QuestSignalControllerFactory
    {
        private readonly IIdGenerator _idGenerator;
        private readonly ISignalBus _signalBus;
        private readonly IEntityRepository _entityRepository;

        public QuestSignalControllerFactory(IIdGenerator idGenerator, ISignalBus signalBus, IEntityRepository entityRepository)
        {
            _idGenerator = idGenerator;
            _signalBus = signalBus;
            _entityRepository = entityRepository;
        }
        
        public QuestSignalController Create()
        {
            QuestFactory questFactory = new QuestFactory();
            
            CreateQuestQuery createQuestQuery = new CreateQuestQuery(_idGenerator, questFactory, _entityRepository);

            QuestSlotFactory questSlotFactory = new QuestSlotFactory();
            
            CreateQuestSlotQuery createQuestSlotQuery =
                new CreateQuestSlotQuery(_idGenerator, _entityRepository, questSlotFactory);

            CreateQuestSlotSignalAction createQuestSlotSignalAction = new CreateQuestSlotSignalAction
            (
                createQuestSlotQuery
            );
            
            CreateQuestSignalAction createQuestSignalAction = new CreateQuestSignalAction
            (
                _signalBus,
                createQuestQuery
            );

            return new QuestSignalController(
                new ISignalAction[]
                {
                    createQuestSignalAction,
                    createQuestSlotSignalAction
                });
        }
    }
}