using System;
using Controls;
using Controls.UiControls;
using Enums;
using Object = UnityEngine.Object;

namespace Controllers.Depository
{
    public class DepositoryController : IDisposable
    {
        private readonly InputControl _inputControl;
        private readonly CurrenciesUiControl _currenciesUiControl;
        
        private readonly AdderCurrencyToDepository _adderCurrencyToDepository;
        public DepositoryController(Installer installer)
        {
            _adderCurrencyToDepository = new AdderCurrencyToDepository(installer);
            _inputControl = installer.InputControl;
            _currenciesUiControl = installer.CurrenciesUiControl;
           
            Subscribe();
        }

        private void OnSimpleClick()
        {
            _adderCurrencyToDepository.AddCurrency(CurrencyType.Currency_0, CurrencyLevel.Units_1);
            
            foreach (var currencyValue in _adderCurrencyToDepository.CurrencyValues)
            {
                _currenciesUiControl.UpdateInfo(currencyValue.Key, currencyValue.Value);
            }
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