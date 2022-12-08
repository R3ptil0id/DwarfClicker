using System;
using System.Collections.Generic;
using Constants;
using Controls;
using Controls.GameElements.CurrencyBar;
using Enums;
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
        
        public AdderCurrencyToDepository(Installer installer)
        {
            _installer = installer;
            _perkController = installer.GetInstance<PerkController>();
            _currencyObjectsPool = installer.GetInstance<CurrencyObjectsPool>();
             
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
                    control.transform.SetParent(_installer.InnerBunker);
                    _currentBarController = new ComplexCurrencyBarController(control);
                    _currentBarController.AppearAtPosition(_installer.DepositoryStartTransform.position);
                    _currentBarController.AddLevel(DataConstants.CurrencyValues[CurrencyLevel.Units_1]);
                    
                    NotifyCurrencyBarControlCreated?.Invoke(_currentBarController);

                    return;
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
    }
}