using System;
using System.Collections.Generic;
using Controls;
using Controls.UiControls;
using Enums;
using Utils.EnumExtensions;
using Utils.Ioc;

namespace Controllers.DepositoryControllers
{
    [RegistrateInIoc()]
    public class DepositoryController : BaseController, IDisposable
    {
        [Inject] private readonly InputControl _inputControl;
        [Inject] private readonly CurrenciesUiControl _currenciesUiControl;
        
        private readonly CurrencyInDepositoryController _currencyInDepositoryController;
        
        private readonly Dictionary<CurrencyType, int> _currencyValues = new();
        
        public Dictionary<CurrencyType, int> CurrencyValues => _currencyValues;
        
        public DepositoryController()
        {
            _currencyInDepositoryController = new CurrencyInDepositoryController();
            foreach (var currencyType in EnumExtension.GetAllItems<CurrencyType>())
            {
                _currencyValues.Add(currencyType, 0);
            }
            Subscribe();
        }

        public void AddCurrency(CurrencyType currencyType)
        {
           var added = _currencyInDepositoryController.TryAddCurrency(currencyType);
           
           if (!added)
           {
               return;
           }

           var currencyCount = ++_currencyValues[currencyType];
           
           _currenciesUiControl.UpdateInfo(currencyType, currencyCount);
        }

        private void OnClickAddCurrencyBar(CurrencyType currencyType)
        {
            _currencyInDepositoryController.AddCurrencyBar(currencyType);
        }

        private void Subscribe()
        {
            _inputControl.NotifyClickAddCurrency += OnClickAddCurrencyBar;
        }

        private void UnSubscribe()
        {
            _inputControl.NotifyClickAddCurrency -= OnClickAddCurrencyBar;
        }

        public void Dispose()
        {
            UnSubscribe();
        }
    }
}