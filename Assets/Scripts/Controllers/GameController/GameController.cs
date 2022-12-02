using Controls;
using Controls.UiControls;
using ScriptableObjects;


namespace Controllers.GameController
{
    public class GameController : IUpdateListener
    {
        private readonly CurrenciesControl _currenciesControl;
        private readonly UiControl _uiControl;
        private readonly PrefabsTable _prefabsTable;
        
        private readonly ShaftController _shaftControl;

        public GameController(Installer installer)
        {
            _currenciesControl = installer.CurrenciesControl;
            _uiControl = installer.UiControl;
            _prefabsTable = installer.PrefabsTable;

            _shaftControl = new ShaftController(installer.ShaftControl);
            _shaftControl.AddLevel();
        }

        public void Update()
        {
            
        }
    }
}