using Controllers.Depository;

namespace Controllers.GameController
{
    public class GameController
    {
        private IocInitializer _iocInitializer;
        private DepositoryController _depositoryController;
        
        public void PreInitialize()
        {
            _iocInitializer = new IocInitializer();
        }

        public void Initialize()
        {
            _iocInitializer.Initialize();
            _depositoryController = new DepositoryController();
        }
    }
}