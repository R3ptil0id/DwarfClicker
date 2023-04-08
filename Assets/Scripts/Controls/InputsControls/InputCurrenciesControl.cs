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
        public Action<CurrencyType> NotifyClickAddMaxCurrency;
        
        public void ClickAddCurrency0()
        {
            NotifyClickAddCurrency?.Invoke(CurrencyType.Currency0);
        }
        
        public void ClickAddCurrency1()
        {
            NotifyClickAddCurrency?.Invoke(CurrencyType.Currency1);
        }
        
        public void ClickAddCurrency2()
        {
            NotifyClickAddCurrency?.Invoke(CurrencyType.Currency2);
        }
        
        public void ClickAddMaxCurrency0()
        {
            NotifyClickAddMaxCurrency?.Invoke(CurrencyType.Currency0);
        }
        
        public void ClickAddMaxCurrency1()
        {
            NotifyClickAddMaxCurrency?.Invoke(CurrencyType.Currency1);
        }
        
        public void ClickAddMaxCurrency2()
        {
            NotifyClickAddMaxCurrency?.Invoke(CurrencyType.Currency2);
        }
        
        public void ClickAddMaxCurrency3()
        {
            NotifyClickAddMaxCurrency?.Invoke(CurrencyType.Currency3);
        }
        
        public void ClickAddMaxCurrency4()
        {
            NotifyClickAddMaxCurrency?.Invoke(CurrencyType.Currency4);
        }
    }
}