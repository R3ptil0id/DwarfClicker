using System.Collections.Generic;
using Constants;
using Enums;

namespace Controllers.Perks
{
    public class CurrencyBarPerks
    {
        public Dictionary<CurrencyType, int> CurrentMaxCurrencyBars { get; } = new(); 

        public CurrencyBarPerks()
        {
            CurrentMaxCurrencyBars.Add(CurrencyType.Currency_0, DataConstants.MaxCurrency0BarOnStart);
            CurrentMaxCurrencyBars.Add(CurrencyType.Currency_1, DataConstants.MaxCurrency1BarOnStart);
            CurrentMaxCurrencyBars.Add(CurrencyType.Currency_2, DataConstants.MaxCurrency2BarOnStart);    
        }

        public void AddMaxCount(CurrencyType currencyType, Enums.Perks perk)
        {
            // CurrentMaxCurrencyBars[currencyType] = PerkConstants.GetValue(perk)
        }
    }
}