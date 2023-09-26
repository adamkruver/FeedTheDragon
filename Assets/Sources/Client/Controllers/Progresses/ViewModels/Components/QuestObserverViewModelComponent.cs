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
using Sources.Client.UseCases.NPCs.Common.Quests.Queries;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Controllers.NPCs.Ogres.ViewModels.Components
{
    public class QuestObserverViewModelComponent : IViewModelComponent
    {
        private readonly IBindableViewBuilder<QuestViewModel> _questViewBuilder;
        private readonly LiveData<int[]> _questsIds;
        
        [PropertyBinding(typeof(IAttachableViewPropertyBind))]
        private IBindableProperty<IAttachableView> _view;
        
        private int[] _questSlotsIds = Array.Empty<int>();

        public QuestObserverViewModelComponent
        (
            int id, 
            IBindableViewBuilder<QuestViewModel> questViewBuilder,
            GetQuestsIdsQuery getQuestsIdsQuery
        )
        {
            _questViewBuilder = questViewBuilder;
            _questsIds = getQuestsIdsQuery.Handle(id, 0); //todo: fill ownerId
        }

        public void Enable()
        {
            _questsIds.Observe(OnMissionChanged);
        }

        public void Disable()
        {
            _questsIds.Unobserve(OnMissionChanged);
        }

        private void OnMissionChanged(int[] questSlotsIds)
        {
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