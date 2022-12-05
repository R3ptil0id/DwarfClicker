using Controls;
using Controls.ScriptableObjects;
using Controls.UiControls;
using Services.GameLoop;
using Services.Timers;


namespace Controllers.GameController
{
    public class GameController : IUpdateListener
    {
        private readonly TimersService _timersService;
        
        private readonly CurrenciesUiControl _currenciesUiControl;
        private readonly UiControl _uiControl;
        private readonly PrefabsTable _prefabsTable;
        
        private readonly ShaftController _shaftControl;
        private readonly DepositoryController _depositoryController;

        public GameController(Installer installer)
        {
            _timersService = new TimersService();
            
            _currenciesUiControl = installer.currenciesUiControl;
            _uiControl = installer.UiControl;
            _prefabsTable = installer.PrefabsTable;

            _shaftControl = new ShaftController(installer.ShaftControl);
            _shaftControl.AddLevel();

            _depositoryController = new DepositoryController(installer.DepositoryControl);
            
            GameLoopService.Instance.Register(this);
        }

        public void Update(float deltaTime)
        {
            
        }
    }
}