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
            CurrentMaxCurrencyBars.Add(CurrencyType.Currency0, DataConstants.MaxCurrency0BarOnStart);
            CurrentMaxCurrencyBars.Add(CurrencyType.Currency1, DataConstants.MaxCurrency1BarOnStart);
            CurrentMaxCurrencyBars.Add(CurrencyType.Currency2, DataConstants.MaxCurrency2BarOnStart);    
        }

        public void AddMaxCount(CurrencyType currencyType, Enums.PerkType perkType)
        {
            // CurrentMaxCurrencyBars[currencyType] = PerkConstants.GetValue(perk)
        }
    }
}