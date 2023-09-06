using System;

namespace Sources.Client.Domain.Components
{
    public class VisibilityComponent : IComponent
    {
        public VisibilityComponent(bool isVisible)
        {
            IsVisible = isVisible;
        }

        public event Action Changed;

        public bool IsVisible { get; private set; }

        public void Show()
        {
            if (IsVisible)
                return;

            IsVisible = true;
            Changed?.Invoke();
        }

        public void Hide()
        {
            if (IsVisible == false)
                return;

            IsVisible = false;
            Changed?.Invoke();
        }
    }
}