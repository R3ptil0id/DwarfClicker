using Enums;

namespace Data.PerksData
{
    public class PerkData
    {
        public PerkType PerkType;
        public CurrencyType CurrencyType;
        public float Price;
        public float Value;

        public PerkData(LoadedPerkData loadedPerkData)
        {
            PerkType = loadedPerkData.PerkType;
            Price = loadedPerkData.BasePrice;
            CurrencyType = loadedPerkData.CurrencyType;
            Value = loadedPerkData.Value;
        }
    }
}