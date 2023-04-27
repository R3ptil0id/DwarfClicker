using System.Collections.Generic;
using Enums;

namespace Constants
{
    public static class CommonConstants
    {
        public const string CScriptableObjectFolderPath = "Assets/ScriptableObjects";
        public static readonly Dictionary<CurrencyType, int> CCurrencyCountInType = new();
        public const int CZero = 0;

        public const float PositionXOffsetCurrency = 0.7f;
        public const int MaxCurrency0BarOnStart = 1;
        public const int MaxCurrency1BarOnStart = 0;
        public const int MaxCurrency2BarOnStart = 0;
        
        public const float BotCollectingTime = 2f;
        public const int BotMaxCountOnStart = 10;
        public const int BotCollectCount = 10;
        public const int BotMaxCount = 160;
        
        public const float XworkerOffset = 0.1f;
        public const float YworkerOffset = 0.4f;
                
        static CommonConstants()
        {
            CCurrencyCountInType.Add(CurrencyType.Currency0, 10);
            CCurrencyCountInType.Add(CurrencyType.Currency1, 10);
            CCurrencyCountInType.Add(CurrencyType.Currency2, 10);
            CCurrencyCountInType.Add(CurrencyType.Currency3, 10);
        }
    }
}