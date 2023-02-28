using Constants;
using Controllers.Perks;
using Controls.UiControls.UiPerkButtonControls;
using Utils.Ioc;

namespace Controllers.UiControllers.UiPerkButtonControllers
{
    public class UiActivePerkButtonController : UiBasePerkButtonController
    {
        [Inject] private PerksController _perksController;
        
        public UiActivePerkButtonController(ConstantPerkData data, UiBasePerkButtonControl uiBuyPerkButtonControl) : base(data, uiBuyPerkButtonControl)
        {
        }

        protected override void UpdateControl()
        {
            if (_uiBuyPerkButtonControl == null)
                return;

            var uiBuyPerkButtonControl = ((UiActivePerkButtonControl)_uiBuyPerkButtonControl);
            
            SetText(uiBuyPerkButtonControl.PerkTypeText, _data.PerkType.ToString());
            SetText(uiBuyPerkButtonControl.DescriptionText, "@Some Description");
        }
    }
}