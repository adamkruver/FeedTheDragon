using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.UseCases.NPCs.Common.Quests.Queries;
using Sources.Client.UseCases.Progresses.Queries;
using UnityEngine;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Controllers.Progresses.ViewModels.Components
{
    public class MissionCounterViewModelComponent : IViewModelComponent
    {
        private readonly int _requiredAmount;
        private readonly LiveData<int> _completedAmount;
        
        public MissionCounterViewModelComponent(
            int id,
            GetMissionQuestCompletedAmountQuery getMissionQuestCompletedAmountQuery,
            GetMissionQuestRequiredAmountQuery getMissionQuestRequiredAmountQuery
        )
        {
            _requiredAmount = getMissionQuestRequiredAmountQuery.Handle(id);
            _completedAmount = getMissionQuestCompletedAmountQuery.Handle(id);
        }

        public void Enable()
        {
            _completedAmount.Observe(OnCompletedAmountChanged);
        }

        public void Disable()
        {
            _completedAmount.Unobserve(OnCompletedAmountChanged);
        }

        private void OnCompletedAmountChanged(int completedAmount)
        {
            Debug.Log($"{completedAmount} / {_requiredAmount}");
        }
    }
}