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
    [RegistrateInIoc]
    public class PerksController : BaseController
    {
        [Inject] private readonly LoadedData _loadedData;
        [Inject] private readonly UiPerksControl _perksControl;
        [Inject] private readonly InputUiControl _inputUiControl;
        [Inject] private readonly DepositoryController _depositoryController;
        [Inject] private readonly UiPrefabs _uiPrefabs;

        private PerksModel _perksModel;
        
        private UiPerksController _uiPerksController;

        public override void Initialize()
        {
            base.Initialize();
            
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