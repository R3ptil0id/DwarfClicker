using System;
using System.Collections.Generic;
using Controllers.Perks;
using Controls.InputsControls;
using Controls.UiControls;
using Enums;
using Models;
using Utils.EnumExtensions;
using Utils.Ioc;

namespace Controllers.DepositoryControllers
{
    [RegistrateInIoc()]
    public class DepositoryController : IDisposable
    {
        [Inject] private CurrenciesInputControl _currenciesInputControl;
        [Inject] private UiInGameControl _uiInGameControl;
        [Inject] private PerksController _perksController;
        
        private readonly CurrencyInDepositoryController _currencyInDepositoryController;
        
        private readonly Dictionary<CurrencyType, float> _currencyValues = new();
        
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
           if (!_currencyInDepositoryController.TryAddCurrency(currencyType))
           {
               return;
           }

           var currencyCount = ++_currencyValues[currencyType];
           
           _uiInGameControl.UpdateInfo(currencyType, currencyCount);
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