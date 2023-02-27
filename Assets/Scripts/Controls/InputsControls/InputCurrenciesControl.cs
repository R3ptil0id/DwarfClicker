using System;
using Enums;
using UnityEngine;
using Utils.Ioc;

namespace Controls.InputsControls
{
    [RegistrateMonoBehaviourInIoc]
    public class CurrenciesInputControl : MonoBehaviour
    {
        public Action<CurrencyType> NotifyClickAddCurrency;
        public Action<CurrencyType> NotifyClickAddBar;
        
        public void ClickAddCurrency0()
        {
            NotifyClickAddCurrency?.Invoke(CurrencyType.Currency_0);
        }
        
        public void ClickAddCurrency1()
        {
            NotifyClickAddCurrency?.Invoke(CurrencyType.Currency_1);
        }
        
        public void ClickAddCurrency2()
        {
            NotifyClickAddCurrency?.Invoke(CurrencyType.Currency_2);
        }
        
        public void ClickAddCurrencyBar0()
        {
            NotifyClickAddBar?.Invoke(CurrencyType.Currency_1);
        }
        
        public void ClickAddCurrencyBar1()
        {
            NotifyClickAddBar?.Invoke(CurrencyType.Currency_2);
        }
        
        public void ClickAddCurrencyBar2()
        {
            NotifyClickAddBar?.Invoke(CurrencyType.Currency_2);
        }
    }
}