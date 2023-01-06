namespace Controllers.GameController
{
    public class GameController
    {
        private IocInitializer _iocInitializer;
        
        public void PreInitialize()
        {
            _iocInitializer = new IocInitializer();
        }

        public void Initialize()
        {
            _iocInitializer.Initialize();
        }
    }
}