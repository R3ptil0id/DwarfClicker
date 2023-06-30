using System;
using System.Collections.Generic;
using Controllers.Perks;
using Controls.InputsControls;
using Controls.UiControls;
using Enums;
using Utils.EnumExtensions;
using Utils.Ioc;

namespace Controllers.DepositoryControllers
{
    [RegistrateInIoc(true, true)]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class EconomyController : IInitializable, ILateInitializable, IDisposable
    {
        [Inject] private CurrenciesInputControl _currenciesInputControl;
        [Inject] private UiInGameControl _uiInGameControl;
        [Inject] private PerksController _perksController;
        
        // private readonly CurrencyInDepositoryController _currencyInDepositoryController;
        
        private readonly Dictionary<CurrencyType, float> _currencyValues = new();
        private readonly Dictionary<CurrencyType, int> _currencyBarsCount = new();

        public EconomyController()
        {
            // _currencyInDepositoryController = new CurrencyInDepositoryController();
        }
        
        public void Initialize()
        {
            foreach (var currencyType in EnumExtension.GetAllItems<CurrencyType>())
            {
                _currencyValues.Add(currencyType, 0);
            }
        }

        public void LateInitialize()
        {
            Subscribe();
        }

        public bool TrySpendCurrency(CurrencyType currencyType, float price)
        {
            if (!_currencyValues.TryGetValue(currencyType, out var value) || value < price) 
                return false;
            
            _currencyValues[currencyType] -= price;
            _uiInGameControl.UpdateInfo(currencyType, _currencyValues[currencyType]);
                
            return true;

        }
        public void AddCurrency(CurrencyType currencyType, float value)
        {
            // var perkType = PerkType.Undefined;
            // switch (currencyType)
            // {
            //     case CurrencyType.Currency0:
            //         perkType = PerkType.Currency0Max;
            //         break;
            //     case CurrencyType.Currency1:
            //         perkType = PerkType.Currency1Max;
            //         break;
            //     case CurrencyType.Currency2:
            //         perkType = PerkType.Currency2Max;
            //         break;
            //     case CurrencyType.Currency3:
            //         perkType = PerkType.Currency3Max;
            //         break;
            //     case CurrencyType.Currency4:
            //         perkType = PerkType.Currency4Max;
            //         break;
            // }
            //
            // var data = _perksController.GetPerkData(perkType);
            //
            // if (data.Value <= _currencyValues[currencyType])
            // {
            //     return;
            // }
            
            var nextValue = _currencyValues[currencyType] + value;
            _currencyValues[currencyType] = Math.Min(nextValue, data.Value);
             
            _uiInGameControl.UpdateInfo(currencyType, _currencyValues[currencyType]);
        }

        private void ClickAddCurrencyBarHandler(CurrencyType currencyType)
        {
            // _currencyInDepositoryController.AddCurrencyBar(currencyType);
        }

        private void Subscribe()
        {
            // _currenciesInputControl.NotifyClickAddCurrency += ClickAddCurrencyBarHandler;
        }

        private void UnSubscribe()
        {
            // _currenciesInputControl.NotifyClickAddCurrency -= ClickAddCurrencyBarHandler;
        }

        public void Dispose()
        {
            UnSubscribe();
        }
    }
}