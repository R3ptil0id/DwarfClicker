using System.Collections.Generic;
using Enums;

namespace Data.PerksData
{
    public class PerksCurrencyData : IPerkData
    {
        public Dictionary<PerkType, PerkData> PerksData { get; }
    }
}