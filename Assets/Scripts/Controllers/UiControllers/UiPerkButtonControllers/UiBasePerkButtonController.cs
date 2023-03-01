using System;
using Constants;
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

        protected UiBasePerkButtonController(PerkData data, UiBasePerkButtonControl uiBuyPerkButtonControl)
        {
            _data = data;
            
            _uiBuyPerkButtonControl = uiBuyPerkButtonControl;
            _uiBuyPerkButtonControl.Clicked += ClickHandler;

            UpdateControl();
        }

        public void Dispose()
        {
            _uiBuyPerkButtonControl.Clicked -= ClickHandler;
            _uiBuyPerkButtonControl?.Dispose();

            Object.Destroy(_uiBuyPerkButtonControl);
        }

        protected abstract void UpdateControl();
        
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