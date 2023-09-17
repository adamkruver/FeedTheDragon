using Controllers.Frameworks.Mvvm.ViewModels;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.GameObjects;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.UseCases.NPCs.Common.Quests.Queries;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Controllers.NPCs.Common.ViewModels
{
    public class QuestViewModel : ViewModelBase
    {
        private readonly LiveData<bool> _isCompleted;

        [PropertyBinding(typeof(IGameObjectEnabledPropertyBind), "IsCompleted")]
        private IBindableProperty<bool> _isEnabled;

        public QuestViewModel(
            IViewModelComponent[] components,
            int id,
            GetQuestIsCompletedQuery getQuestIsCompletedQuery
        ) : base(components)
        {
            _isCompleted = getQuestIsCompletedQuery.Handle(id);
        }

        protected override void OnEnable() =>
            _isCompleted.Observe(OnQuestComplete);

        protected override void OnDisable() =>
            _isCompleted.Unobserve(OnQuestComplete);

        private void OnQuestComplete(bool isCompleted) =>
            _isEnabled.Value = isCompleted;
    }
}