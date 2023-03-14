using Controls.InputsControls;
using Controls.UiControls;
using Utils.Ioc;

namespace Controllers.UiControllers
{
    public class UiInGameController : BaseController
    {
        [Inject] private UiInGameControl _inGameControl;
        [Inject] private InputUiControl _inputUiControl;
        
        private bool _isEnable;
        
        public UiInGameController()
        {
            _isEnable = true;
            UpdateInGamePanelState();
            
            _inputUiControl.NotifyClickPerkPanel += ClickOpenPerkPanelHandler;
        }

        private void UpdateInGamePanelState()
        {
            _inGameControl.InGamePanel.gameObject.SetActive(_isEnable);
        }

        private void ClickOpenPerkPanelHandler()
        {
            _isEnable = !_isEnable;
            UpdateInGamePanelState();
        }
    }
}