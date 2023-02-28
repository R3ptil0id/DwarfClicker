using Constants;
using Controllers.Perks;
using Controls.UiControls.UiPerkButtonControls;
using Utils.Ioc;

namespace Controllers.UiControllers.UiPerkButtonControllers
{
    public class UiBuyPerkButtonController : UiBasePerkButtonController
    {
        [Inject] private PerksController _perksController;

        public UiBuyPerkButtonController(ConstantPerkData data, UiBasePerkButtonControl uiBuyPerkButtonControl) : base(data, uiBuyPerkButtonControl)
        {
        }

        protected override void UpdateControl()
        {
            if (_uiBuyPerkButtonControl == null)
                return;

            var uiBuyPerkButtonControl = ((UiBuyPerkButtonControl)_uiBuyPerkButtonControl);
            
            SetText(uiBuyPerkButtonControl.PriceText, _data.Price.ToString());
            SetText(uiBuyPerkButtonControl.TypePriceText, _data.PriceType.ToString());
            SetText(uiBuyPerkButtonControl.TypeText, _data.PerkType.ToString());
            SetText(uiBuyPerkButtonControl.DescriptionText, "@Some Description");
        }
    }
}