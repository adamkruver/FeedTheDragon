using System;
using Sources.Client.App.Configs.Presentation.Views;

namespace Sources.Client.App.Configs.Presentation
{
    [Serializable]
    public class ViewConfig
    {
        public ViewConfigCollection Ingredients;
        public ViewConfigCollection NPCs;
        public ViewConfigCollection Quests;
    }
}