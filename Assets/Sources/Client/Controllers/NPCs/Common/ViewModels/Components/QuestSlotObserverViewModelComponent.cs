using System.Collections.Generic;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Sources.Frameworks.Mvvm.PresentationInterfaces.Binds.AttachableViews;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.Domain.NPCs.Components;
using Sources.Client.Extensions;
using Sources.Client.InfrastructureInterfaces.Builders.Presentation.BindableViews;
using Sources.Client.UseCases.NPCs.Common.Quests.Queries;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Controllers.NPCs.Common.ViewModels.Components
{
    public class QuestSlotObserverViewModelComponent : IViewModelComponent
    {
        private readonly IBindableViewBuilder<QuestSlotViewModel> _questSlotViewBuilder;
        private readonly LiveData<int[]> _slotsIds;

        private int[] _slots = { };
        
        [PropertyBinding(typeof(IAttachableViewPropertyBind))]
        private IBindableProperty<IAttachableView> _view;

        public QuestSlotObserverViewModelComponent(
            int id,
            GetQuestSlotsIdsQuery getQuestSlotsIdsQuery,
            IBindableViewBuilder<QuestSlotViewModel> questSlotViewBuilder)
        {
            _questSlotViewBuilder = questSlotViewBuilder;
            _slotsIds = getQuestSlotsIdsQuery.Handle(id);
        }

        public void Enable()
        {
            _slotsIds.Observe(OnQuestSlotsChanged);
        }

        public void Disable()
        {
            _slotsIds.Unobserve(OnQuestSlotsChanged);
        }

        private void OnQuestSlotsChanged(int[] newQuestSlotsIds)
        {
            (IEnumerable<int> added, IEnumerable<int> removed) = _slots.Diff(newQuestSlotsIds, Compare);

            foreach (int questSlotId in added)
            {
                IBindableView view = _questSlotViewBuilder.Build(questSlotId, nameof(QuestSlot));
                _view.Value.Attach(view);
            }
            
            //todo: remove slots "removed"
            
            _slots = newQuestSlotsIds;
        }

        private bool Compare(int id1, int id2) =>
            id1 == id2;
    }
}