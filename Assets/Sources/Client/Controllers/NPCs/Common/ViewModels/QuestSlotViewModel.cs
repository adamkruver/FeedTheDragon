using System;
using Controllers.Frameworks.Mvvm.ViewModels;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.PresentationInterfaces.Binds.Ingredients;
using Sources.Client.UseCases.NPCs.Common.Quests.Queries;

namespace Sources.Client.Controllers.NPCs.Common.ViewModels
{
    public class QuestSlotViewModel : ViewModelBase
    {
        [PropertyBinding(typeof(IIngredientTypePropertyBind))]
        private IBindableProperty<Type> _ingredientType;

        private readonly int _id;
        private readonly GetQuestSlotRequiredTypeQuery _getQuestSlotRequiredTypeQuery;

        public QuestSlotViewModel(IViewModelComponent[] components, 
            int id,
            GetQuestSlotRequiredTypeQuery getQuestSlotRequiredTypeQuery) : base(components)
        {
            _id = id;
            _getQuestSlotRequiredTypeQuery = getQuestSlotRequiredTypeQuery;
        }

        protected override void OnEnable()
        {
            _ingredientType.Value = _getQuestSlotRequiredTypeQuery.Handle(_id).GetType();
        }

        protected override void OnDisable()
        {
        }
    }
}