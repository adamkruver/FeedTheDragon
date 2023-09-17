using PresentationInterfaces.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.App.Configs;
using Sources.Client.Controllers.NPCs.Common.Signals;
using Sources.Client.Controllers.NPCs.Common.ViewModels;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.Ingredients.IngredientTypes;
using Sources.Client.Domain.NPCs.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs;
using Sources.Client.InfrastructureInterfaces.Builders.Presentation.BindableViews;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.NPCs.Common.Quests.Queries;
using UnityEngine;

namespace Sources.Client.Controllers.NPCs.Common.Actions
{
    public class CreateQuestSignalAction : ISignalAction<CreateQuestSignal>
    {
        private readonly ISignalBus _signalBus;
        private readonly CreateQuestQuery _createQuestQuery;

        private readonly IIngredientType[] _availableIngredientTypes;

        public CreateQuestSignalAction
        (
            ISignalBus signalBus,
            IIngredientType[] availableIngredientTypes,
            CreateQuestQuery createQuestQuery
        )
        {
            _signalBus = signalBus;
            _availableIngredientTypes = availableIngredientTypes;
            _createQuestQuery = createQuestQuery;
        }

        public void Handle(CreateQuestSignal signal)
        {
            int questId = _createQuestQuery.Handle(signal.OwnerId);

            for (int i = 0; i < signal.TasksAmount; i++)
            {
                IIngredientType type = _availableIngredientTypes[Random.Range(0, _availableIngredientTypes.Length)];
                _signalBus.Handle(new CreateQuestSlotSignal(questId, type));
            }
        }
    }
}