using System;

namespace Controls.UiControls.UiPerkButtonControls
{
    public interface IClickListener
    {
        void AddClickListener(Action action);
    }
}