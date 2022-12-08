using Controllers.Depository;
using Controls;
using Controls.ScriptableObjects;
using Services.GameLoop;
using Services.Timers;


namespace Controllers.GameController
{
    public class GameController : IUpdateListener
    {
        private readonly PrefabsTable _prefabsTable;

        private readonly CurrencyObjectsPool _currencyObjectsPool;
        
        private readonly ShaftController _shaftControl;
        private readonly InputControl _inputControl;
        
        public GameController(Installer installer)
        {
            _currencyObjectsPool = new CurrencyObjectsPool(installer);
            
            installer.AddInstance(_currencyObjectsPool);
            installer.AddInstance(new PerkController(installer));
            installer.AddInstance(new DepositoryController(installer));
            installer.AddInstance(new ShaftController(installer));
            installer.AddInstance(new TimersService());
            
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