using System.Globalization;
using Controls.UiControls.UiPerkButtonControls;
using Data.PerksData;

namespace Controllers.UiControllers.UiPerkButtonControllers
{
    public class UiActivePerkButtonController : UiBasePerkButtonController
    {
        public UiActivePerkButtonController(PerkData data, UiBasePerkButtonControl control) : base(data, control)
        {
        }

        public void UpdateControllerData()
        {
            if (_control == null)
                return;

            var uiBuyPerkButtonControl = (UiActivePerkButtonControl)_control;
            
            SetText(uiBuyPerkButtonControl.PerkTypeText, _data.PerkType.ToString());
            SetText(uiBuyPerkButtonControl.PerkLevelText, _data.Value.ToString(CultureInfo.InvariantCulture));
            SetText(uiBuyPerkButtonControl.DescriptionText, "@Some Description");
        }
    }
}