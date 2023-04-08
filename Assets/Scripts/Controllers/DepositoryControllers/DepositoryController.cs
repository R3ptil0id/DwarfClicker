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
    [RegistrateInIoc(false, true)]
    public class DepositoryController : ILateInitializable, IDisposable
    {
        [Inject] private CurrenciesInputControl _currenciesInputControl;
        [Inject] private UiInGameControl _uiInGameControl;
        [Inject] private PerksController _perksController;
        
        private readonly CurrencyInDepositoryController _currencyInDepositoryController;
        
        private readonly Dictionary<CurrencyType, float> _currencyValues = new();
        private readonly Dictionary<CurrencyType, int> _currencyBarsCount = new();

        public DepositoryController()
        {
            _currencyInDepositoryController = new CurrencyInDepositoryController();
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

        public float GetCurrencyValue(CurrencyType currencyType)
        {
            return _currencyValues.TryGetValue(currencyType, out var value) ? value : 0;
        }
        
        public int GetCurrencyBarCount(CurrencyType currencyType)
        {
            return _currencyBarsCount.TryGetValue(currencyType, out var value) ? value : 0;
        }

        public void AddCurrency(CurrencyType currencyType, float value)
        {
            var perkType = PerkType.Undefined;
            switch (currencyType)
            {
                case CurrencyType.Currency0:
                    perkType = PerkType.Currency0Add;
                    break;
                case CurrencyType.Currency1:
                    perkType = PerkType.Currency1Add;
                    break;
                case CurrencyType.Currency2:
                    perkType = PerkType.Currency2Add;
                    break;
                case CurrencyType.Currency3:
                    perkType = PerkType.Currency3Add;
                    break;
                case CurrencyType.Currency4:
                    perkType = PerkType.Currency4Add;
                    break;
            }

            // var data = _perksController.GetPerkData(perkType);
            //
            // if (data.Value <= _currencyValues[currencyType])
            // {
            //     return;
            // }
            //
            // var nextValue = _currencyValues[currencyType] + value;
            // _currencyValues[currencyType] = Math.Clamp(nextValue, nextValue, data.Value);
            //  
            // _uiInGameControl.UpdateInfo(currencyType, _currencyValues[currencyType]);
        }

        private void ClickAddCurrencyBarHandler(CurrencyType currencyType)
        {
            _currencyInDepositoryController.AddCurrencyBar(currencyType);
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