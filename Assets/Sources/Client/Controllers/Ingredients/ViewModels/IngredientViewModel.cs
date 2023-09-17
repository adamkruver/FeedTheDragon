using Controllers.Frameworks.Mvvm.ViewModels;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.BindableViews;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Triggers;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.PresentationInterfaces.Binds.Scales;
using UnityEngine;

namespace Sources.Client.Controllers.Ingredients.ViewModels
{
    public class IngredientViewModel : ViewModelBase
    {

        public IngredientViewModel(IViewModelComponent[] components) : base(components)
        {
        }

        protected override void OnEnable()
        {
        }

        protected override void OnDisable()
        {
        }


    }
}