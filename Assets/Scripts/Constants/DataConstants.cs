using System.Collections.Generic;
using Enums;

namespace Constants
{
    public static class DataConstants
    {
        public static readonly Dictionary<CurrencyLevel, int> CurrencyValues = new();
        public static readonly Dictionary<CurrencyLevel, float> Sizes = new();

        // public const float PositionOffsetCurrency = 0.0022f;
        // public const float PositionOffsetCurrencyOtherType = 0.042f;
        public const float SpaceLevel = 0.05f;
        public const float SpaceType = 0.1f;
                
        static DataConstants()
        {
            CurrencyValues.Add(CurrencyLevel.Units1, 1);
            CurrencyValues.Add(CurrencyLevel.Units10, 10);
            CurrencyValues.Add(CurrencyLevel.Units100, 100);
            CurrencyValues.Add(CurrencyLevel.Units1000, 1000);
            
            Sizes.Add(CurrencyLevel.Units1, 51f);
            Sizes.Add(CurrencyLevel.Units10, 61f);
            Sizes.Add(CurrencyLevel.Units100, 73f);
            Sizes.Add(CurrencyLevel.Units1000, 88f);
        }
    }
}