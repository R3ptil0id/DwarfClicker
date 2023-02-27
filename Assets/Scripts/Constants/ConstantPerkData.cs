using Enums;

namespace Constants
{
    public class ConstantPerkData
    {
        public readonly PerkType PerkType;
        public readonly CurrencyType PriceType;
        public readonly int Price;
        public readonly int Value;

        public ConstantPerkData(PerkType perkType, CurrencyType priceType, int price, int value)
        {
            PerkType = perkType;
            PriceType = priceType;
            Price = price;
            Value = value;
        }
    }
}