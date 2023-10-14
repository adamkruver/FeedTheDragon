using System;
using Domain.Frameworks.Mvvm.Attributes;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Triggers;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.PresentationInterfaces.Binds.Ids;
using Sources.Client.UseCases.Common.Components.FirstContacts.Commands;
using Sources.Client.UseCases.Ingredients.Queries;
using UnityEngine;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class ScopeViewModelComponent : IViewModelComponent
    {
        private readonly int _id;
        private readonly AddKnownTypeCommand _addKnownTypeCommand;
        private readonly GetIngredientTypeQuery _getIngredientTypeQuery;

        public ScopeViewModelComponent(
            int id,
            AddKnownTypeCommand addKnownTypeCommand,
            GetIngredientTypeQuery getIngredientTypeQuery
        )
        {
            _id = id;
            _addKnownTypeCommand = addKnownTypeCommand;
            _getIngredientTypeQuery = getIngredientTypeQuery;
        }

        public void Enable()
        {
        }

        public void Disable()
        {
        }

        [MethodBinding(typeof(ITriggerEnterMethodBind), componentName: "IngredientInteractor")]
        private void OnTriggerEnter(Component component)
        {
            Debug.Log(component.name);
            
            if (component.TryGetComponent(out IIdPropertyBind idPropertyBind) == false)
                return;

            Type type = _getIngredientTypeQuery.Handle(idPropertyBind.Id).GetType();
            
            _addKnownTypeCommand.Handle(_id, type);
        }
    }
}