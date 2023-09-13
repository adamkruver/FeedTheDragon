using System;
using PresentationInterfaces.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.Controllers.NPCs.Common.Signals;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.NPCs.Common.Commands;
using Sources.Client.UseCases.NPCs.Common.Queries;
using UnityEngine;
using Environment = Sources.Client.App.Configs.Environment;

namespace Sources.Client.Controllers.NPCs.Common.Actions
{
    public class CreateQuestSlotSignalAction : ISignalAction<CreateQuestSlotSignal>
    {
        private readonly IBindableViewFactory _bindableViewFactory;
        private readonly QuestSlotViewModelFactory _questViewModelFactory;
        private readonly CreateQuestSlotQuery _createQuestQuery;
        private readonly AddQuestSlotCommand _addQuestCommand;
        private readonly Environment _environment;

        public CreateQuestSlotSignalAction
        (
            IBindableViewFactory bindableViewFactory,
            QuestSlotViewModelFactory questViewModelFactory,
            CreateQuestSlotQuery createQuestQuery,
            AddQuestSlotCommand addQuestCommand,
            Environment environment
        )
        {
            _bindableViewFactory = bindableViewFactory;
            _questViewModelFactory = questViewModelFactory;
            _createQuestQuery = createQuestQuery;
            _addQuestCommand = addQuestCommand;
            _environment = environment;
        }

        public void Handle(CreateQuestSlotSignal signal)
        {
            IIngredientType ingredientType = signal.Type;
            
            int id = _createQuestQuery.Handle(ingredientType);

            throw new NotImplementedException("Тут должны быть другие пути к файлам");
            
            string ingredientTypeName = ingredientType.GetType().Name;
            string path = _environment.View["Ingredient"];
            
            IViewModel viewModel = _questViewModelFactory.Create(id);
            IBindableView view = _bindableViewFactory.Create(path, ingredientTypeName);
            
            view.Bind(viewModel);
            
            _addQuestCommand.Handle(signal.OwnerId, id);
        }
    }
}