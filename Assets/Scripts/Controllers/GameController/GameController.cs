
using Controllers.BotController;
using Controllers.DepositoryControllers;
using Services.Timers;
using Utils.Ioc;

namespace Controllers.GameController
{
    public class GameController : BaseController
    {
        [Inject] private DepositoryController _depositoryController;
        private BotsController _botsController;
        [Inject] private TimersService _timersService;

        public GameController()
        {
            _botsController = new BotsController();
            _timersService.Run();   
        }
    }
}