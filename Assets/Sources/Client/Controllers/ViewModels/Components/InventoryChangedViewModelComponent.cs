using System.Collections.Generic;
using Domain.Frameworks.Mvvm.Attributes;
using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.Providers;
using Sources.Client.PresentationInterfaces.Binds.Inventories;
using Sources.Client.UseCases.InventoryComponents.Listeners;
using Sources.Client.UseCases.InventoryComponents.Queries;
using UnityEngine;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class InventoryChangedViewModelComponent : IViewModelComponent
    {
        private readonly IResourceLoader _resourceLoader;
        
        private readonly int _id;
        private readonly AddInventoryListener _addInventoryListener;
        private readonly RemoveInventoryListener _removeInventoryListener;
        private readonly GetInvenoryItemTypesQuery _getInvenoryItemTypesQuery;
        
        [PropertyBinding(typeof(IInventorySlotsViewPropertyBind))]
        private BindableProperty<Sprite[]> _invenotySpriteRenderers;

        public InventoryChangedViewModelComponent(
            IResourceLoader resourceLoader,
            int id,
            AddInventoryListener addInventoryListener,
            RemoveInventoryListener removeInventoryListener,
            GetInvenoryItemTypesQuery getInvenoryItemTypesQuery
        )
        {
            _resourceLoader = resourceLoader;
            _id = id;
            _addInventoryListener = addInventoryListener;
            _removeInventoryListener = removeInventoryListener;
            _getInvenoryItemTypesQuery = getInvenoryItemTypesQuery;
        }

        public void Enable()
        {
            _addInventoryListener.Handle(_id, OnInventoryChanged);
        }

        public void Disable()
        {
            _removeInventoryListener.Handle(_id, OnInventoryChanged);
        }

        private void OnInventoryChanged()
        {
            List<Sprite> sprites = new List<Sprite>();
            
            foreach (IIngredientType ingredientType in _getInvenoryItemTypesQuery.Handle(_id))
            {
                Sprite sprite = _resourceLoader.Load<Sprite>("", ingredientType.GetType().Name); //todo to constants
                
                sprites.Add(sprite);
            }
            
            _invenotySpriteRenderers.Value = sprites.ToArray();
        }
    }
}