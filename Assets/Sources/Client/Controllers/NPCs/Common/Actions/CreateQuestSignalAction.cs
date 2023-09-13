using PresentationInterfaces.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.App.Configs;
using Sources.Client.Controllers.NPCs.Common.Signals;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.Ingredients.IngredientTypes;
using Sources.Client.Domain.NPCs.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs;
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
        private readonly IBindableViewFactory _bindableViewFactory;
        private readonly QuestViewModelFactory _questViewModelFactory;
        private readonly CreateQuestQuery _createQuestQuery;
        private readonly AddQuestCommand _addQuestCommand;
        private readonly Environment _environment;

        private readonly IIngredientType[] _avaliableTypes = new IIngredientType[]
        {
            new ToxicFrog(),
            new Chanterelle()
        };

        public CreateQuestSignalAction
        (
            ISignalBus signalBus,
            IBindableViewFactory bindableViewFactory,
            QuestViewModelFactory questViewModelFactory,
            CreateQuestQuery createQuestQuery,
            AddQuestCommand addQuestCommand,
            Environment environment
        )
        {
            _signalBus = signalBus;
            _bindableViewFactory = bindableViewFactory;
            _questViewModelFactory = questViewModelFactory;
            _createQuestQuery = createQuestQuery;
            _addQuestCommand = addQuestCommand;
            _environment = environment;
        }

        public void Handle(CreateQuestSignal signal)
        {
            int id = _createQuestQuery.Handle();

            IViewModel viewModel = _questViewModelFactory.Create(id);
            IBindableView view = _bindableViewFactory.Create(_environment.View["Quest"], nameof(Quest));

            view.Bind(viewModel);

            _addQuestCommand.Handle(signal.OwnerId, id);

            for (int i = 0; i < signal.TasksAmount; i++)
            {
                IIngredientType type = _avaliableTypes[Random.Range(0, _avaliableTypes.Length)];
                _signalBus.Handle(new CreateQuestSlotSignal(id, type));
            }
        }
    }
}