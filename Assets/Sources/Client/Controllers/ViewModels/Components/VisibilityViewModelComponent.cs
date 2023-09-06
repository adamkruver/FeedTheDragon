using System;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.BindableViews;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Domain;
using Sources.Client.Domain.Components;
using Sources.Client.UseCases.Common.Components.Visibilities.Listeners;
using Sources.Client.UseCases.Common.Components.Visibilities.Queries;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class VisibilityViewModelComponent : IViewModelComponent
    {
        private readonly int _id;
        private readonly AddVisibilityListener _addVisibilityListener;
        private readonly RemoveVisibilityListener _removeVisibilityListener;
        private readonly GetVisibilityQuery _getVisibilityQuery;

        [PropertyBinding(typeof(IBindableViewEnabledPropertyBind))]
        private IBindableProperty<bool> _isEnabled;

        public VisibilityViewModelComponent
        (
            int id,
            AddVisibilityListener addVisibilityListener,
            RemoveVisibilityListener removeVisibilityListener,
            GetVisibilityQuery getVisibilityQuery
        )
        {
            _id = id;
            _addVisibilityListener = addVisibilityListener;
            _removeVisibilityListener = removeVisibilityListener;
            _getVisibilityQuery = getVisibilityQuery;
        }

        public void Enable()
        {
            OnVisibilityChanged();
            
            _addVisibilityListener.Handle(_id, OnVisibilityChanged);
        }

        public void Disable()
        {
            _removeVisibilityListener.Handle(_id, OnVisibilityChanged);
        }

        private void OnVisibilityChanged()
        {
            _isEnabled.Value = _getVisibilityQuery.Handle(_id);
        }
    }
}