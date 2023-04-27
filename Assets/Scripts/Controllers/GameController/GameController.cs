using Controllers.BotController;
using Controllers.DepositoryControllers;
using Controllers.UiControllers;
using Controllers.Workers;
using Services.Timers;
using Utils.Ioc;

namespace Controllers.GameController
{
    public class GameController : BaseController
    {
        [Inject] private DepositoryController _depositoryController;
        [Inject] private TimersService _timersService;
        
        private BotsController _botsController;
        private WorkersController _workersController;
        private UiInGameController _uiInGameController;

        public GameController()
        {
            _botsController = new BotsController();
            _workersController = new WorkersController();
            _uiInGameController = new UiInGameController();
            
            // ReSharper disable once PossibleNullReferenceException
            _timersService.Run();   
        }
    }
}