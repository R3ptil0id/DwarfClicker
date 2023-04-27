using System;
using Controls.UiControls.UiPerkButtonControls;
using Data.PerksData;
using TMPro;

using Object = UnityEngine.Object;

namespace Controllers.UiControllers.UiPerkButtonControllers
{
    public abstract class UiBasePerkButtonController : BaseController, IDisposable
    {
        protected readonly PerkData _data;
        protected readonly UiBasePerkButtonControl _control;
        
        protected UiBasePerkButtonController(PerkData data, UiBasePerkButtonControl control)
        {
            _data = data;
            _control = control;
        }

        public virtual void Dispose()
        {
            Object.Destroy(_control);
        }
        
        protected void SetText(TMP_Text text, string str)
        {
            if (text == null)
                return;

            text.text = str;
        }
    }
}