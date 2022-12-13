using System.Collections.Generic;
using Enums;

namespace Constants
{
    public static class DataConstants
    {
        public static readonly Dictionary<CurrencyLevel, int> CurrencyValues = new();
        public static readonly Dictionary<CurrencyType, int> CurrencyMaxValues = new();
        public static readonly Dictionary<CurrencyLevel, float> Sizes = new();

        public const float PositionOffsetCurrency = 0.0022f;
        public const float PositionOffsetCurrencyOtherType = 0.042f;
                
        static DataConstants()
        {
            CurrencyValues.Add(CurrencyLevel.Units_1, 1);
            CurrencyValues.Add(CurrencyLevel.Units_5, 5);
            CurrencyValues.Add(CurrencyLevel.Units_20, 20);
            CurrencyValues.Add(CurrencyLevel.Units_80, 80);
            CurrencyValues.Add(CurrencyLevel.Units_320, 320);
            CurrencyValues.Add(CurrencyLevel.Units_1280, 1280);
            
            CurrencyMaxValues.Add(CurrencyType.Currency_0, 10240);
            CurrencyMaxValues.Add(CurrencyType.Currency_1, 10240);
            CurrencyMaxValues.Add(CurrencyType.Currency_2, 10240);
            CurrencyMaxValues.Add(CurrencyType.Currency_3, 10240);
            CurrencyMaxValues.Add(CurrencyType.Currency_4, 10240);
            
            Sizes.Add(CurrencyLevel.Units_5, 0.1f);
            Sizes.Add(CurrencyLevel.Units_20, 0.15f);
            Sizes.Add(CurrencyLevel.Units_80, 0.225f);
            Sizes.Add(CurrencyLevel.Units_320, 0.3375f);
            Sizes.Add(CurrencyLevel.Units_1280, 0.50625f);
        }
    }
}