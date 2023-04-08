using System;
using Controllers.Perks;
using Controls.UiControls.UiPerkButtonControls;
using Data.PerksData;
using TMPro;
using Utils.Ioc;

using Object = UnityEngine.Object;

namespace Controllers.UiControllers.UiPerkButtonControllers
{
    public abstract class UiBasePerkButtonController : BaseController, IDisposable
    {
        [Inject] private PerksController _perksController;

        protected readonly PerkData _data;
        protected readonly UiBasePerkButtonControl _uiBuyPerkButtonControl;
        protected readonly IClickListener _clickListener;
        
        protected UiBasePerkButtonController(PerkData data, IClickListener clickListener)
        {
            _data = data;
            _clickListener = clickListener;
            
            clickListener.AddClickListener(ClickHandler);

            UpdateControllerData();
        }

        public void Dispose()
        {
            ((IDisposable)_clickListener)?.Dispose();
            Object.Destroy(_uiBuyPerkButtonControl);
        }

        public abstract void UpdateControllerData();
        
        protected void SetText(TMP_Text text, string str)
        {
            if (text == null)
                return;

            text.text = str;
        }

        private void ClickHandler()
        {
            _perksController?.BuyPerk(_data.PerkType);
        }
    }
}