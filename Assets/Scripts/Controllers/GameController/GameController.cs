using Controls;
using Controls.ScriptableObjects;
using Services.GameLoop;
using Services.Timers;


namespace Controllers.GameController
{
    public class GameController : IUpdateListener
    {
        private readonly TimersService _timersService;
        
        private readonly InputControl _inputControl;
        
        private readonly PrefabsTable _prefabsTable;

        private readonly CurrencyObjectsPool _currencyObjectsPool;
        
        private readonly ShaftController _shaftControl;
        private readonly DepositoryController _depositoryController;
        
        public GameController(Installer installer)
        {
            _timersService = new TimersService();
            _prefabsTable = installer.PrefabsTable;

            _currencyObjectsPool = new CurrencyObjectsPool(installer);
            
            _shaftControl = new ShaftController(installer);
            _depositoryController = new DepositoryController(installer, _currencyObjectsPool);
            
            
            GameLoopService.Instance.Register(this);
            Initialize();
        }

        public void Update(float deltaTime)
        {
            
        }

        private void Initialize()
        {
            _currencyObjectsPool.Initialize();
        }
    }
}