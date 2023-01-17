using System.Collections.Generic;
using Enums;

namespace Constants
{
    public static class DataConstants
    {
        public const string ScriptableObjectFolderPath = "Assets/ScriptableObjects";
        public static readonly Dictionary<CurrencyType, int> CurrencyCountInType = new();

        public const float PositionXOffsetCurrency = 0.7f;
        public const int MaxCurrency0BarOnStart = 1;
        public const int MaxCurrency1BarOnStart = 0;
        public const int MaxCurrency2BarOnStart = 0;
        
        public const float BotCollectingTime = 2f;
        public const int BotMaxCountOnStart = 10;
        public const int BotMaxCount = 160;
                
        static DataConstants()
        {
            CurrencyCountInType.Add(CurrencyType.Currency_0, 10);
            CurrencyCountInType.Add(CurrencyType.Currency_1, 10);
            CurrencyCountInType.Add(CurrencyType.Currency_2, 10);
            CurrencyCountInType.Add(CurrencyType.Currency_3, 10);
        }
    }
}