using Controllers.DepositoryControllers;
using Controllers.UiControllers;
using Controls.InputsControls;
using Controls.UiControls;
using Data;
using Data.PerksData;
using Enums;
using Models;
using ScriptableObjects;
using Utils.Ioc;

namespace Controllers.Perks
{
    [RegistrateInIoc(true)]
    public class PerksController : IInitializable
    {
        [Inject] private LoadedData _loadedData;
        [Inject] private UiPerksControl _perksControl;
        [Inject] private InputUiControl _inputUiControl;
        [Inject] private DepositoryController _depositoryController;
        [Inject] private UiPrefabs _uiPrefabs;

        private PerksModel _perksModel;
        
        private UiPerksController _uiPerksController;

        public void Initialize()
        {
            _perksModel = new PerksModel(_loadedData.PerkDataCollection);
            _uiPerksController = new UiPerksController(_perksModel);
        }

        public void BuyPerk(PerkType perkType)
        {
            var data = _perksModel.BuyPerk(perkType);
            _uiPerksController.BuyAndActivatePerk(data);
        }
    }
}