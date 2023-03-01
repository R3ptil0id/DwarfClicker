using System.Collections.Generic;
using Enums;

namespace Data.PerksData
{
    public class PerkWorkersData : IPerkData
    {
        public Dictionary<PerkType, PerkData> PerksData { get; } =
            new()
            {
                {
                    PerkType.StartMinersLvl1Count,
                    new PerkData(PerkType.MinersLvl1, CurrencyType.Undefined, 0, 4)
                },
                { PerkType.MinersLvl1, new PerkData(PerkType.MinersLvl1, CurrencyType.Currency2, 100, 1) },
            };
    }
}