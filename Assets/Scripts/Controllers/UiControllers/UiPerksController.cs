using System.Collections.Generic;
using Controllers.Perks;
using Controllers.UiControllers.UiPerkButtonControllers;
using Controls.InputsControls;
using Controls.UiControls;
using Controls.UiControls.UiPerkButtonControls;
using Data.PerksData;
using Enums;
using Models;
using ScriptableObjects;
using UnityEngine;
using Utils.Ioc;

namespace Controllers.UiControllers
{
    public class UiPerksController : BaseController
    {
       [Inject] private InputUiControl _inputUiControl;
       [Inject] private UiPerksControl _uiPerksControl;
       [Inject] private UiPrefabs _uiPrefabs;
       
       private readonly PerksModel _perksModel;

        private Dictionary<PerkType, UiBuyPerkButtonController> _buyingButtons = new();
        private Dictionary<PerkType, UiActivePerkButtonController> _activeButtons = new();
        
        private bool _isEnable;

        public UiPerksController(PerksModel perksModel)
        {
            // _perkConstantsList = perkConstantsList;
            _perksModel = perksModel;
            _isEnable = false;
            UpdateInGamePanelState();
            
            _inputUiControl.NotifyClickPerkPanel += ClickOpenPerkPanelHandler;
            
            FillButtons();
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
            FillNotActivePerks();
            FillActivePerks();
        }

        private void FillNotActivePerks()
        {
            foreach (var perkType in _perksModel.NotActivePerks)
            {
                var perkData = _perksModel.GetPerkData(perkType);
                
                var buyButtonGameObject =
                    Object.Instantiate(_uiPrefabs.BuyPerk, _uiPerksControl.BuyingPerksContent.transform);
                
                var buyButtonControl = buyButtonGameObject.GetComponent<UiBuyPerkButtonControl>();
                var buyButtonController =  new UiBuyPerkButtonController(perkData, buyButtonControl);
        
                _buyingButtons.Add(perkType, buyButtonController);
            }
        }
        
        private void FillActivePerks()
        {
            foreach (var perkType in _perksModel.ActivePerks)
            {
                var perkData =  _perksModel.GetPerkData(perkType);
                
                if (perkData.ActiveOnStart)
                    continue;
                
                var buyButtonGameObject =
                    Object.Instantiate(_uiPrefabs.ActivePerk, _uiPerksControl.BuyingPerksContent.transform);
                
                var buyButtonControl = buyButtonGameObject.GetComponent<UiActivePerkButtonControl>();
                var buyButtonController =  new UiActivePerkButtonController(perkData, buyButtonControl);
        
                _activeButtons.Add(perkType, buyButtonController);
            }
        }

        private void UpdateInGamePanelState()
        {
            _uiPerksControl.PerksPanel.gameObject.SetActive(_isEnable);
        }
        
        private void ClickOpenPerkPanelHandler()
        {
            _isEnable = !_isEnable;
            UpdateInGamePanelState();
        }
    }
}