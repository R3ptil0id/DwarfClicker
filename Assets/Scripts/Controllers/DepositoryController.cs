using System;
using System.Collections.Generic;
using Constants;
using Controls;
using Controls.GameElements;
using Controls.UiControls;
using Enums;
using Utils;

namespace Controllers
{
    public class DepositoryController : IDisposable
    {
        private readonly CurrencyObjectsPool _currencyObjectsPool;
        private readonly DepositoryControl _depositoryControl;
        private readonly InputControl _inputControl;
        private readonly CurrenciesUiControl _currenciesUiControl;
        private readonly Dictionary<CurrencyType, int> _currencyValues = new();
        public DepositoryController(Installer installer, CurrencyObjectsPool currencyObjectsPool)
        {
            _currencyObjectsPool = currencyObjectsPool;
            _currenciesUiControl = installer.currenciesUiControl;
            _depositoryControl = installer.DepositoryControl;
            _inputControl = installer.InputControl;
            
            foreach (var currencyType in EnumExtension.GetAllItems<CurrencyType>())
            {
                _currencyValues.Add(currencyType, 0);
            }
            
            Subscribe();
        }

        public void AddCurrency(CurrencyType type, CurrencyLevel level)
        {
           _currencyValues[type] += DataConstants.CurrencyValues[level];
           _currenciesUiControl.UpdateInfo(type, _currencyValues[type]);
           
           var nextBar =  _currencyObjectsPool.GetCurrencyObject(type, level);
        }
        
        private void OnSimpleClick()
        {
            AddCurrency(CurrencyType.Currency_0, CurrencyLevel.Units_1);    
        }


        private void Subscribe()
        {
            _inputControl.NotifyClick += OnSimpleClick;
        }

        private void UnSubscribe()
        {
            _inputControl.NotifyClick -= OnSimpleClick;
        }

        public void Dispose()
        {
            UnSubscribe();
        }
    }
}