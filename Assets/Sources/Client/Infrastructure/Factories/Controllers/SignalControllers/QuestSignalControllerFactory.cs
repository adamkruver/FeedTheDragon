using Sources.Client.Controllers;
using Sources.Client.Controllers.NPCs.Common.Actions;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Infrastructure.Factories.Domain.NPCs;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;
using Sources.Client.UseCases.NPCs.Common.Quests.Commands;
using Sources.Client.UseCases.NPCs.Common.Quests.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.SignalControllers
{
    public class QuestSignalControllerFactory
    {
        private readonly IIdGenerator _idGenerator;
        private readonly ISignalBus _signalBus;
        private readonly IEntityRepository _entityRepository;
        private readonly ICurrentPlayerService _currentPlayerService;

        public QuestSignalControllerFactory(
            IIdGenerator idGenerator,
            ISignalBus signalBus,
            IEntityRepository entityRepository,
            ICurrentPlayerService currentPlayerService
        )
        {
            _idGenerator = idGenerator;
            _signalBus = signalBus;
            _entityRepository = entityRepository;
            _currentPlayerService = currentPlayerService;
        }

        public SignalController Create(IIngredientType[] availableIngredientTypes)
        {
            QuestFactory questFactory = new QuestFactory();

            CreateQuestQuery createQuestQuery = new CreateQuestQuery(_idGenerator, questFactory, _entityRepository);

            QuestSlotFactory questSlotFactory = new QuestSlotFactory();

            CreateQuestSlotQuery createQuestSlotQuery =
                new CreateQuestSlotQuery(_idGenerator, _entityRepository, questSlotFactory);

            GiveQuestRequiredItemCommand giveQuestRequiredItemCommand =
                new GiveQuestRequiredItemCommand(_entityRepository, _currentPlayerService);

            CreateQuestSlotSignalAction createQuestSlotSignalAction = new CreateQuestSlotSignalAction
            (
                createQuestSlotQuery
            );

            CreateQuestSignalAction createQuestSignalAction = new CreateQuestSignalAction
            (
                _signalBus,
                availableIngredientTypes,
                createQuestQuery
            );

            GiveQuestRequiredItemSignalAction giveQuestRequiredItemSignalAction =
                new GiveQuestRequiredItemSignalAction(giveQuestRequiredItemCommand);

            return new SignalController(
                new ISignalAction[]
                {
                    createQuestSignalAction,
                    createQuestSlotSignalAction,
                    giveQuestRequiredItemSignalAction,
                });
        }
    }
}