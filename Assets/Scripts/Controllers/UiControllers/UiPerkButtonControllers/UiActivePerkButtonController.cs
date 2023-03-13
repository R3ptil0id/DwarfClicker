using Controllers.Perks;
using Controls.UiControls.UiPerkButtonControls;
using Data.PerksData;
using Utils.Ioc;

namespace Controllers.UiControllers.UiPerkButtonControllers
{
    public class UiActivePerkButtonController : UiBasePerkButtonController
    {
        [Inject] private PerksController _perksController;
        
        public UiActivePerkButtonController(PerkData data, IClickListener uiBuyPerkButtonControl) : base(data, uiBuyPerkButtonControl)
        {
        }

        public override void UpdateControllerData()
        {
            if (_uiBuyPerkButtonControl == null)
                return;

            var uiBuyPerkButtonControl = ((UiActivePerkButtonControl)_uiBuyPerkButtonControl);
            
            SetText(uiBuyPerkButtonControl.PerkTypeText, _data.PerkType.ToString());
            SetText(uiBuyPerkButtonControl.DescriptionText, "@Some Description");
        }
    }
}