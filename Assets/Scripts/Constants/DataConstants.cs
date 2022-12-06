using System.Collections.Generic;
using Enums;

namespace Constants
{
    public static class DataConstants
    {
        public static readonly Dictionary<CurrencyLevel, int> CurrencyValues = new();

        static DataConstants()
        {
            CurrencyValues.Add(CurrencyLevel.Units_1, 1);
            CurrencyValues.Add(CurrencyLevel.Units_5, 5);
            CurrencyValues.Add(CurrencyLevel.Units_20, 20);
            CurrencyValues.Add(CurrencyLevel.Units_80, 80);
            CurrencyValues.Add(CurrencyLevel.Units_320, 320);
            CurrencyValues.Add(CurrencyLevel.Units_1280, 1280);
        }
    }
}