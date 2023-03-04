using System;
using Enums;

namespace Data.PerksData
{
    [Serializable]
    public class PerkData
    {
        public PerkType PerkType;
        public CurrencyType CurrencyType;
        public int Price;
        public float Modifier;
        public float Value;
        public float MaxValue;
        public bool ActiveOnStart;
    }
}