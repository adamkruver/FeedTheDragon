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
using Sources.Client.UseCases.NPCs.Common.Commands;
using Sources.Client.UseCases.NPCs.Common.Queries;
using UnityEngine;

namespace Sources.Client.Controllers.NPCs.Common.Actions
{
    public class CreateQuestSignalAction : ISignalAction<CreateQuestSignal>
    {
        private readonly ISignalBus _signalBus;
        private readonly CreateQuestQuery _createQuestQuery;

        private readonly IIngredientType[] _avaliableTypes = new IIngredientType[] // todo: move to config
        {
            new ToxicFrog(),
            new Chanterelle()
        };

        public CreateQuestSignalAction
        (
            ISignalBus signalBus,
            CreateQuestQuery createQuestQuery
        )
        {
            _signalBus = signalBus;
            _createQuestQuery = createQuestQuery;
        }

        public void Handle(CreateQuestSignal signal)
        {
            int questId = _createQuestQuery.Handle(signal.OwnerId);

            for (int i = 0; i < signal.TasksAmount; i++)
            {
                IIngredientType type = _avaliableTypes[Random.Range(0, _avaliableTypes.Length)];
                _signalBus.Handle(new CreateQuestSlotSignal(questId, type));
            }
        }
    }
}