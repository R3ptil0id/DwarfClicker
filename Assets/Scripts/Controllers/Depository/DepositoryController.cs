using System;
using Controls;
using Controls.UiControls;
using Enums;
using Utils.Ioc;

namespace Controllers.Depository
{
    public class DepositoryController : BaseController, IDisposable
    {
        [Inject] private readonly InputControl _inputControl;
        [Inject] private readonly CurrenciesUiControl _currenciesUiControl;
        
        private readonly CurrencyInDepositoryController _currencyInDepositoryController;
        public DepositoryController()
        {
            _currencyInDepositoryController = new CurrencyInDepositoryController();
            Subscribe();
        }

        private void OnSimpleClick()
        {
            _currencyInDepositoryController.AddCurrency(CurrencyType.Currency_0);
            
            foreach (var currencyValue in _currencyInDepositoryController.CurrencyValues)
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