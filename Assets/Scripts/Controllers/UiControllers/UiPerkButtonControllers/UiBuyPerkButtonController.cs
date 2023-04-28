using System;
using System.Globalization;
using Controllers.Perks;
using Controls.UiControls.UiPerkButtonControls;
using Data.PerksData;
using Utils.Ioc;

namespace Controllers.UiControllers.UiPerkButtonControllers
{
    public class UiBuyPerkButtonController : UiBasePerkButtonController
    {
        [Inject] private PerksController _perksController;

        private const int ToNextLevelValue = 1;
        
        public UiBuyPerkButtonController(PerkData data, UiBasePerkButtonControl control) : base(data, control)
        {
            ((UiBuyPerkButtonControl)_control).AddClickListener(ClickHandler);
        }

        public void UpdateControllerData()
        {
            if (_control == null)
                return;

            var uiBuyPerkButtonControl = ((UiBuyPerkButtonControl)_control);
            
            SetText(uiBuyPerkButtonControl.PerkTypeText, _data.PerkType.ToString());
            SetText(uiBuyPerkButtonControl.PriceText, _data.Price.ToString(CultureInfo.InvariantCulture));
            SetText(uiBuyPerkButtonControl.PerkLevelText, (_data.Level + ToNextLevelValue).ToString(CultureInfo.InvariantCulture));
            SetText(uiBuyPerkButtonControl.TypePriceText, _data.CurrencyType.ToString());
            SetText(uiBuyPerkButtonControl.DescriptionText, "@Some Description");
        }

        public override void Dispose()
        {
            base.Dispose();
            ((IDisposable)_control).Dispose();
        }

        private void ClickHandler()
        {
            _perksController?.BuyPerk(_data.PerkType);
        }
    }
}