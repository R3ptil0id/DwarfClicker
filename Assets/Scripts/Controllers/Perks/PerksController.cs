using Controllers.UiControllers;
using Controls.InputsControls;
using Controls.UiControls;
using Data;
using Enums;
using Models;
using ScriptableObjects;
using Utils.Ioc;

namespace Controllers.Perks
{
    [RegistrateInIoc(needInitialize: true)]
    public class PerksController : BaseController, IInitializable
    {
        [Inject] private readonly LoadedData _loadedData;
        [Inject] private readonly UiPerksControl _perksControl;
        [Inject] private readonly InputUiControl _inputUiControl;
        [Inject] private readonly UiPrefabs _uiPrefabs;

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