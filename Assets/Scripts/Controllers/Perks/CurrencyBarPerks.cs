using System.Collections.Generic;
using Constants;
using Enums;

namespace Controllers.Perks
{
    public class CurrencyBarPerks : BasePerks
    {
        public Dictionary<CurrencyType, int> CurrentMaxCurrencyBars { get; } = new(); 

        public CurrencyBarPerks()
        {
            CurrentMaxCurrencyBars.Add(CurrencyType.Currency0, CommonConstants.MaxCurrency0BarOnStart);
            CurrentMaxCurrencyBars.Add(CurrencyType.Currency1, CommonConstants.MaxCurrency1BarOnStart);
            CurrentMaxCurrencyBars.Add(CurrencyType.Currency2, CommonConstants.MaxCurrency2BarOnStart);    
        }

        public void AddMaxCount(CurrencyType currencyType, Enums.PerkType perkType)
        {
            // CurrentMaxCurrencyBars[currencyType] = PerkConstants.GetValue(perk)
        }
    }
}