using Enums;

namespace Data.PerksData
{
    public class PerkData
    {
        public readonly PerkType PerkType;
        public readonly CurrencyType PriceType;
        public readonly int Price;
        public readonly int Value;

        public PerkData(PerkType perkType, CurrencyType priceType, int price, int value)
        {
            PerkType = perkType;
            PriceType = priceType;
            Price = price;
            Value = value;
        }
    }
}