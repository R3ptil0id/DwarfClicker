using System.Collections.Generic;
using Constants;
using Controllers.Perks;
using Controls.InputsControls;
using Controls.UiControls;
using Enums;
using ScriptableObjects;
using UnityEngine;
using Utils.Ioc;

namespace Controllers.UiControllers
{
    public class UiPerksController : BaseController
    {
       [Inject] private readonly InputUiControl _inputUiControl;
       [Inject] private readonly UiPerksControl _uiPerksControl;
       [Inject] private readonly UiPrefabs _uiPrefabs;
       
       private readonly List<IPerks> _perkConstantsList;

        private Dictionary<PerkType, UiPerkButtonController> _buyingButtons = new();
        private Dictionary<PerkType, UiPerkButtonController> _activeButtons = new();
        
        private bool _isEnable;

        public UiPerksController(List<IPerks> perkConstantsList)
        {
            _perkConstantsList = perkConstantsList;
            
            _isEnable = false;
            UpdateInGamePanelState();
            
            _inputUiControl.NotifyClickPerkPanel += ClickOpenPerkPanelHandler;
            
            FillButtons();
        }

        private void UpdateInGamePanelState()
        {
            _uiPerksControl.PerksPanel.gameObject.SetActive(_isEnable);
        }

        public void BuyAndActivatePerk(KeyValuePair<PerkType, ConstantPerkData> perkData)
        {
            if (!_buyingButtons.TryGetValue(perkData.Key, out var buyButtonController))
                return;

            buyButtonController.Dispose();

            var activeButtonGameObject =
                Object.Instantiate(_uiPrefabs.ActivePerk, _uiPerksControl.ActivePerksContent.transform);
            var activeButtonControl = activeButtonGameObject.GetComponent<UiPerkButtonControl>();
            var activeButtonController = new UiPerkButtonController(perkData.Value, activeButtonControl);

            _activeButtons.Add(perkData.Key, activeButtonController);
        }
        
        private void FillButtons()
        {
            foreach (var perk in _perkConstantsList)
            {
                FillNotActivePerks(perk);
                FillActivePerks(perk);
            }
        }

        private void FillNotActivePerks(IPerks perk)
        {
            if (perk.NotActivePerks == null)
                return;

            foreach (var notActivePerk in perk.NotActivePerks)
            {
                var buyButtonGameObject =
                    Object.Instantiate(_uiPrefabs.BuyPerk, _uiPerksControl.BuyingPerksContent.transform);

                var buyButtonControl = buyButtonGameObject.GetComponent<UiPerkButtonControl>();
                var buyButtonController =
                    new UiPerkButtonController(perk.PerkConstants.ConstantsList[notActivePerk], buyButtonControl);

                _buyingButtons.Add(notActivePerk, buyButtonController);
            }
        }
        
        private void FillActivePerks(IPerks perk)
        {
            if (perk.NotActivePerks == null)
                return;

            foreach (var activePerk in perk.ActivePerks)
            {
                var activeButtonGameObject =
                    Object.Instantiate(_uiPrefabs.ActivePerk, _uiPerksControl.ActivePerksContent.transform);

                var buyButtonControl = activeButtonGameObject.GetComponent<UiPerkButtonControl>();
                var buyButtonController =
                    new UiPerkButtonController(perk.PerkConstants.ConstantsList[activePerk], buyButtonControl);

                _buyingButtons.Add(activePerk, buyButtonController);
            }
        }

        private void ClickOpenPerkPanelHandler()
        {
            _isEnable = !_isEnable;
            UpdateInGamePanelState();
        }
    }
}