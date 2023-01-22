using Controls;
using Utils.Ioc;

namespace Controllers.BotController
{
    public class BotsController : BaseController
    {
        [Inject] private readonly BotPoolController _botPoolController;
        [Inject] private readonly InputControl _inputControl;

        public BotsController()
        {
            _inputControl.NotifyClickAddBot += ClickAddBotHandler;
        }

        private void ClickAddBotHandler()
        {
            _botPoolController.GetBotController();
        }
    }
}