using System.Collections.Generic;
using Constants;
using Controllers.Perks;
using Controllers.UiControllers.UiPerkButtonControllers;
using Controls.InputsControls;
using Controls.UiControls;
using Controls.UiControls.UiPerkButtonControls;
using Data.PerksData;
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

        private Dictionary<PerkType, UiBuyPerkButtonController> _buyingButtons = new();
        private Dictionary<PerkType, UiActivePerkButtonController> _activeButtons = new();
        
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

        public void BuyAndActivatePerk(PerkData perkData)
        {
            if (!_buyingButtons.TryGetValue(perkData.PerkType, out var buyButtonController))
                return;

            buyButtonController.Dispose();

            var activeButtonGameObject =
                Object.Instantiate(_uiPrefabs.ActivePerk, _uiPerksControl.ActivePerksContent.transform);
            var activeButtonControl = activeButtonGameObject.GetComponent<UiActivePerkButtonControl>();
            var activeButtonController = new UiActivePerkButtonController(perkData, activeButtonControl);

            _activeButtons.Add(perkData.PerkType, activeButtonController);
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

                var perkConstantsConstants = perk.PerkData.PerksData[notActivePerk];
                
                var buyButtonControl = buyButtonGameObject.GetComponent<UiBuyPerkButtonControl>();
                var buyButtonController =  new UiBuyPerkButtonController(perkConstantsConstants, buyButtonControl);

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

                var perkConstantsConstants = perk.PerkData.PerksData[activePerk];
                
                var activePerkButtonControl = activeButtonGameObject.GetComponent<UiActivePerkButtonControl>();
                var activePerkButtonController = new UiActivePerkButtonController(perkConstantsConstants, activePerkButtonControl);

                _activeButtons.Add(activePerk, activePerkButtonController);
            }
        }

        private void ClickOpenPerkPanelHandler()
        {
            _isEnable = !_isEnable;
            UpdateInGamePanelState();
        }
    }
}