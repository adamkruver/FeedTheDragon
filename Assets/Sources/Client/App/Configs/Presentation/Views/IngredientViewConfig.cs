using System;
using System.Linq;

namespace Sources.Client.App.Configs.Presentation.Views
{
    [Serializable]
    public class IngredientViewConfig
    {
        public KeyValue[] KeyValues;

        public string this[string key] =>
            KeyValues
                .FirstOrDefault(keyValue => keyValue.Key == key)?
                .Value
            ?? throw new NullReferenceException();
    }
}