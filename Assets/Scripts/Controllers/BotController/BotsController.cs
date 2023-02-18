using Controls;
using Controls.InputsControls;
using Utils.Ioc;

namespace Controllers.BotController
{
    public class BotsController : BaseController
    {
        [Inject] private readonly BotsInputControl _botsInputControl;
        
        private readonly BotPoolController _botPoolController;

        public BotsController()
        {
            _botPoolController = new BotPoolController();
            
            _botsInputControl.NotifyClickAddBot += ClickAddBotHandler;
        }

        private void ClickAddBotHandler()
        {
            _botPoolController.GetBotController();
        }
    }
}