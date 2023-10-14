using System;
using Domain.Frameworks.Mvvm.Attributes;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Triggers;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.PresentationInterfaces.Binds.Ids;
using Sources.Client.UseCases.Common.Components.FirstContacts.Commands;
using Sources.Client.UseCases.Entities.Queries;
using UnityEngine;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class ScopeViewModelComponent : IViewModelComponent
    {
        private readonly int _id;
        private readonly AddKnownTypeCommand _addKnownTypeCommand;
        private readonly GetEntityTypeQuery _getEntityTypeQuery;

        public ScopeViewModelComponent(
            int id,
            AddKnownTypeCommand addKnownTypeCommand,
            GetEntityTypeQuery getEntityTypeQuery
        )
        {
            _id = id;
            _addKnownTypeCommand = addKnownTypeCommand;
            _getEntityTypeQuery = getEntityTypeQuery;
        }

        public void Enable()
        {
        }

        public void Disable()
        {
        }

        [MethodBinding(typeof(ITriggerEnterMethodBind), componentName: "Scope")]
        private void OnTriggerEnter(Component component)
        {
            if (component.TryGetComponent(out IIdPropertyBind idPropertyBind) == false)
                return;

            Type type = _getEntityTypeQuery.Handle(idPropertyBind.Id).GetType();

            _addKnownTypeCommand.Handle(_id, type);
        }
    }
}