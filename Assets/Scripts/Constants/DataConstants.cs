using System.Collections.Generic;
using Enums;

namespace Constants
{
    public static class DataConstants
    {
        public static readonly Dictionary<CurrencyLevel, int> CurrencyValues = new();
        public static readonly Dictionary<CurrencyType, int> CurrencyMaxValues = new();
                
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
        }
    }
}