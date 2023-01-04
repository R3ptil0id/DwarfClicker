using System.Collections.Generic;
using Enums;

namespace Constants
{
    public static class DataConstants
    {
        
        public const string ScriptableObjectFolderPath = "Assets/ScriptableObjects";
        public static readonly Dictionary<CurrencyType, int> CurrencyCountInType = new();

        public const float PositionXOffsetCurrency = 0.15f;
                
        static DataConstants()
        {
            CurrencyCountInType.Add(CurrencyType.Currency_0, 10);
            CurrencyCountInType.Add(CurrencyType.Currency_1, 10);
            CurrencyCountInType.Add(CurrencyType.Currency_2, 10);
            CurrencyCountInType.Add(CurrencyType.Currency_3, 10);
        }
    }
}