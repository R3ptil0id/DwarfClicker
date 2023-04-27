using System;
using Enums;

namespace Data.PerksData
{
    [Serializable]
    public class LoadedPerkData
    {
        public PerkType PerkType;
        public CurrencyType CurrencyType;
        public int BasePrice;
        public float Modifier;
        public PriceCount PriceCount;
        public float Value;
        public float MaxValue;
    }
}