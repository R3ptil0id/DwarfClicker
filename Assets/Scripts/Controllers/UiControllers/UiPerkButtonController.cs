using System;
using Constants;
using Controls.UiControls;

using Object = UnityEngine.Object;

namespace Controllers.UiControllers
{
    public class UiPerkButtonController : IDisposable
    {
        private readonly ConstantPerkData _data;
        private readonly UiPerkButtonControl _uiPerkButtonControl;

        public UiPerkButtonController(ConstantPerkData data, UiPerkButtonControl uiPerkButtonControl)
        {
            _data = data;
            _uiPerkButtonControl = uiPerkButtonControl;
        }

        public void Dispose()
        {
            _uiPerkButtonControl?.Dispose();
            Object.Destroy(_uiPerkButtonControl);
        }
    }
}