
using Controllers.BotController;
using Controllers.DepositoryControllers;
using Services.Timers;
using Utils.Ioc;

namespace Controllers.GameController
{
    public class GameController
    {
        private IocInitializer _iocInitializer;
        private DepositoryController _depositoryController;
        private BotsController _botsController;
        private TimersService _timersService;
        public void PreInitialize()
        {
            _iocInitializer = new IocInitializer();
        }

        public void Initialize()
        {
            _iocInitializer.Initialize();
            _depositoryController = new DepositoryController();
            _botsController = new BotsController();

            _timersService = IoC.Resolve<TimersService>();
            _timersService.Run();
        }
    }
}