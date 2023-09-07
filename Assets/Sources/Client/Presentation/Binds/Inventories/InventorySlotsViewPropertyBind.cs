using System;
using Domain.Frameworks.Mvvm.Properties;
using Sources.Client.PresentationInterfaces.Binds.Inventories;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Client.Presentation.Binds.Inventories
{
    public class InventorySlotsViewPropertyBind : BindableViewProperty<Sprite[]>, IInventorySlotsViewPropertyBind
    {
        [SerializeField] private Image[] _images;

        public override Sprite[] BindableProperty
        {
            get => null;
            set
            {
                if (value.Length > _images.Length) //todo изобрести крутой эксепшен и решение
                    throw new Exception("В InventoryView недостаточно слотов");
                
                for (int i = 0; i < value.Length; i++)
                {
                    _images[i].sprite = value[i];
                }
            }
        }
    }
}