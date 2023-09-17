using System;
using Controllers.Frameworks.Mvvm.ViewModels;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Buttons;
using PresentationInterfaces.Frameworks.Mvvm.Binds.GameObjects;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.NPCs.Common.Signals;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.PresentationInterfaces.Binds.Ingredients;
using Sources.Client.UseCases.NPCs.Common.Quests.Queries;
using UnityEngine;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Controllers.NPCs.Common.ViewModels
{
    public class QuestSlotViewModel : ViewModelBase
    {
        [PropertyBinding(typeof(IIngredientTypePropertyBind))]
        private IBindableProperty<Type> _ingredientType;

        [PropertyBinding(typeof(IGameObjectEnabledPropertyBind))]
        private IBindableProperty<bool> _isQuestItemReached;

        private readonly int _id;
        private readonly ISignalBus _signalBus;
        private readonly GetQuestSlotRequiredTypeQuery _getQuestSlotRequiredTypeQuery;
        private readonly LiveData<bool> _isReached;

        public QuestSlotViewModel(IViewModelComponent[] components, 
            int id,
            ISignalBus signalBus,
            GetQuestSlotRequiredTypeQuery getQuestSlotRequiredTypeQuery,
            GetQuestSlotIsReachedQuery getQuestSlotIsReachedQuery
            ) : base(components)
        {
            _id = id;
            _signalBus = signalBus;
            _getQuestSlotRequiredTypeQuery = getQuestSlotRequiredTypeQuery;
            _isReached = getQuestSlotIsReachedQuery.Handle(id);
        }

        protected override void OnEnable()
        {
            _ingredientType.Value = _getQuestSlotRequiredTypeQuery.Handle(_id).GetType();
            _isReached.Observe(OnReached);
        }

        protected override void OnDisable()
        {
            _isReached.Unobserve(OnReached);
        }

        [MethodBinding(typeof(IButtonClickMethodBind))]
        private void OnSlotButtonClick(Vector3 position) => 
            _signalBus.Handle(new GiveQuestRequiredItemSignal(_id));

        private void OnReached(bool isReached)
        {
            _isQuestItemReached.Value = isReached;
        }
    }
}