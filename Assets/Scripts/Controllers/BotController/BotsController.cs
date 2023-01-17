using Controls;
using Utils.Ioc;

namespace Controllers.BotController
{
    public class BotsController : BaseController
    {
        [Inject] private readonly BotPool _botPool;
        [Inject] private readonly InputControl _inputControl;

        public BotsController()
        {
            _inputControl.NotifyClickAddBot += ClickAddBotHandler;
        }

        private void ClickAddBotHandler()
        {
            _botPool.GetBotController();
        }
    }
}