using System;
using System.Collections.Generic;
using Constants;
using Controls;
using Controls.GameElements.CurrencyBar;
using Enums;
using UnityEngine;
using Utils;

namespace Controllers.Depository
{
    public class AdderCurrencyToDepository
    {
        private readonly Dictionary<CurrencyType, int> _currencyValues = new();
        private readonly LinkedList<CurrencyDepositoryBlock> BlocksInDepository = new();
        
        private readonly Installer _installer;
        private readonly PerkController _perkController;
        private readonly CurrencyObjectsPool _currencyObjectsPool;
        
        private ComplexCurrencyBarController _currentBarController;
        public Action<CurrencyBarController> NotifyCurrencyBarControlCreated;
        private bool _autoConvert;

        private Vector3 _currentPosition;
        
        public AdderCurrencyToDepository(Installer installer)
        {
            _installer = installer;
            _perkController = installer.GetInstance<PerkController>();
            _currencyObjectsPool = installer.GetInstance<CurrencyObjectsPool>();
            _currentPosition = _installer.DepositoryStartTransform.localPosition;
            
            foreach (var currencyType in EnumExtension.GetAllItems<CurrencyType>())
            {
                _currencyValues.Add(currencyType, 0);
            }
        }

        public Dictionary<CurrencyType, int> CurrencyValues => _currencyValues;

        public void AddCurrency(CurrencyType type, CurrencyLevel level)
        {
            if (type == CurrencyType.Currency_0 && level == CurrencyLevel.Units_1)
            {
                if (_currentBarController == null)
                {
                    var control = (ComplexCurrencyBarControl)_currencyObjectsPool.GetCurrencyObject(CurrencyType.Currency_0, CurrencyLevel.Units_5);
                    Transform transform;
                    (transform = control.transform).SetParent(_installer.InnerBunker);
                    transform.localPosition = _currentPosition; 
                    _currentBarController = new ComplexCurrencyBarController(control);
                }
                
                _currentBarController.AddLevel(DataConstants.CurrencyValues[CurrencyLevel.Units_1]);    
                NotifyCurrencyBarControlCreated?.Invoke(_currentBarController);
                if (_currentBarController.CurrentBarLvl == DataConstants.CurrencyValues[CurrencyLevel.Units_5])
                {
                    _currentBarController = null;
                    _currentPosition = _currentPosition + Vector3.right * DataConstants.PositionOffsetCurrencyLvl_5;
                }
                
                // _currentBarController.AddLevel(DataConstants.CurrencyValues[CurrencyLevel.Units_1]);
                // if (_currentBarControl.CurrencyLevel != CurrencyLevel.Units_5)
            }

            var currencyDepositoryBlock = BlocksInDepository.Last;
            if (currencyDepositoryBlock?.Value == null)
            {
                return;
            }
            
            while ((currencyDepositoryBlock.Value.CurrencyType != type &&
                    currencyDepositoryBlock.Value.CurrencyLevel != level) ||
                   currencyDepositoryBlock.Previous != null)
            {
                currencyDepositoryBlock = currencyDepositoryBlock.Previous;
            }
        }
        
        // private void AddToBlock
    }
}