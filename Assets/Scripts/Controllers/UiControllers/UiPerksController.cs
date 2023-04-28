using System.Collections.Generic;
using Controllers.UiControllers.UiPerkButtonControllers;
using Controls.InputsControls;
using Controls.UiControls;
using Controls.UiControls.UiPerkButtonControls;
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
            _perksModel = perksModel;
            _isEnable = false;
            UpdateInGamePanelState();

            FillNotActivePerks();
            FillActivePerks();
            PerksModelUpdatedHandler();

            _inputUiControl.NotifyClickPerkPanel += ClickOpenPerkPanelHandler;
            _perksModel.UpdatedNotify += PerksModelUpdatedHandler;
        }
        
        private void FillNotActivePerks()
        {
            foreach (var perkType in _perksModel.BuyablePerks)
            {
                var perkData = _perksModel.GetPerkData(perkType);

                var buyButtonGameObject =
                    Object.Instantiate(_uiPrefabs.BuyPerk, _uiPerksControl.BuyingPerksContent.transform);

                var buyButtonControl = buyButtonGameObject.GetComponent<UiBuyPerkButtonControl>();
                var buyButtonController = new UiBuyPerkButtonController(perkData, buyButtonControl);

                _buyingButtons.Add(perkType, buyButtonController);
            }
        }

        private void FillActivePerks()
        {
            foreach (var perkType in _perksModel.ActivePerks)
            {
                var perkData = _perksModel.GetPerkData(perkType);
                var loadedPerkData = _perksModel.GetLoadedPerkData(perkType);

                var buyButtonGameObject =
                    Object.Instantiate(_uiPrefabs.ActivePerk, _uiPerksControl.ActivePerksContent.transform);

                var activeButtonControl = buyButtonGameObject.GetComponent<UiActivePerkButtonControl>();
                var buyButtonController = new UiActivePerkButtonController(perkData, activeButtonControl);

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

        private void PerksModelUpdatedHandler()
        {
            foreach (var controller in _buyingButtons.Values)
            {
                controller.UpdateControllerData();
            }

            foreach (var controller in _activeButtons.Values)
            {
                controller.UpdateControllerData();
            }
        }
    }
}