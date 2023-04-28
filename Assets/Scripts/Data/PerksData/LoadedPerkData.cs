using System;
using Enums;

namespace Data.PerksData
{
    [Serializable]
    public class LoadedPerkData
    {
        public PerkType PerkType;
        public PerkType DependencyPerkType;
        public CurrencyType CurrencyType;
        public int DependencyPerkLevel;
        public int BasePrice;
        public float Modifier;
        public PriceCount PriceCount;
        public int Value;
        public int MaxValue;
    }
}