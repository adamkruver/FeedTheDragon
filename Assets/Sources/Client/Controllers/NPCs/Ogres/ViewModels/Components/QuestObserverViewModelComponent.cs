using System;
using System.Collections.Generic;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Sources.Frameworks.Mvvm.PresentationInterfaces.Binds.AttachableViews;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.Controllers.NPCs.Common.ViewModels;
using Sources.Client.Extensions;
using Sources.Client.InfrastructureInterfaces.Builders.Presentation.BindableViews;
using Sources.Client.UseCases.Common.Components.ComponentsListenets;
using Sources.Client.UseCases.NPCs.Common.Quests.Queries;

namespace Sources.Client.Controllers.NPCs.Ogres.ViewModels.Components
{
    public class QuestObserverViewModelComponent : IViewModelComponent
    {
        private readonly int _id;
        private readonly IBindableViewBuilder<QuestViewModel> _questViewBuilder;
        private readonly AddAfterComponentsChangedListnerCommand _addAfterComponentsChangedListnerCommand;
        private readonly RemoveAfterComponentsChangedListnerCommand _removeAfterComponentsChangedListnerCommand;
        private readonly GetQuestsIdsQuery _getQuestsIdsQuery;
        
        [PropertyBinding(typeof(IAttachableViewPropertyBind))]
        private IBindableProperty<IAttachableView> _view;
        
        private int[] _questSlotsIds = Array.Empty<int>();

        public QuestObserverViewModelComponent
        (
            int id, 
            IBindableViewBuilder<QuestViewModel> questViewBuilder,
            AddAfterComponentsChangedListnerCommand addAfterComponentsChangedListnerCommand,
            RemoveAfterComponentsChangedListnerCommand removeAfterComponentsChangedListnerCommand,
            GetQuestsIdsQuery getQuestsIdsQuery
        )
        {
            _id = id;
            _questViewBuilder = questViewBuilder;
            _addAfterComponentsChangedListnerCommand = addAfterComponentsChangedListnerCommand;
            _removeAfterComponentsChangedListnerCommand = removeAfterComponentsChangedListnerCommand;
            _getQuestsIdsQuery = getQuestsIdsQuery;
        }

        public void Enable()
        {
            _addAfterComponentsChangedListnerCommand.Hanlde(_id, OnAfterComponentsChanged);
        }

        public void Disable()
        {
            _removeAfterComponentsChangedListnerCommand.Hanlde(_id, OnAfterComponentsChanged);
        }

        private void OnAfterComponentsChanged()
        {
            int[] questSlotsIds = _getQuestsIdsQuery.Handle(_id, 0); //todo: fill ownerId

            (IEnumerable<int> added, IEnumerable<int> removed) = _questSlotsIds.Diff(questSlotsIds, Compare);

            foreach (int addedQuest in added)
            {
                IBindableView view = _questViewBuilder.Build(addedQuest, "Quest");
                _view.Value.Attach(view);
            }
            
            //todo: remove quests "removed"
            
            _questSlotsIds = questSlotsIds;
        }

        private bool Compare(int id1, int id2) =>
            id1 == id2;
    }
}