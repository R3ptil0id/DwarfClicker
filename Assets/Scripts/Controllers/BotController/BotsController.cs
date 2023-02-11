using Controls;
using Controls.InputsControls;
using Utils.Ioc;

namespace Controllers.BotController
{
    public class BotsController : BaseController
    {
        private readonly BotPoolController _botPoolController;
        [Inject] private readonly BotsInputControl _botsInputControl;

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