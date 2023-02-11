using System;
using System.Collections.Generic;
using Controls;
using Controls.InputsControls;
using Controls.UiControls;
using Enums;
using Utils.EnumExtensions;
using Utils.Ioc;

namespace Controllers.DepositoryControllers
{
    [RegistrateInIoc()]
    public class DepositoryController : BaseController, IDisposable
    {
        [Inject] private readonly CurrenciesInputControl _currenciesInputControl;
        [Inject] private readonly CurrenciesUiControl _currenciesUiControl;
        
        private readonly CurrencyInDepositoryController _currencyInDepositoryController;
        
        private readonly Dictionary<CurrencyType, int> _currencyValues = new();
        
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

        private void ClickAddCurrencyBarHandler(CurrencyType currencyType)
        {
            _currencyInDepositoryController.AddCurrencyBar(currencyType);
        }

        private void Subscribe()
        {
            _currenciesInputControl.NotifyClickAddCurrency += ClickAddCurrencyBarHandler;
        }

        private void UnSubscribe()
        {
            _currenciesInputControl.NotifyClickAddCurrency -= ClickAddCurrencyBarHandler;
        }

        public void Dispose()
        {
            UnSubscribe();
        }
    }
}