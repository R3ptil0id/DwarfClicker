using System.Collections.Generic;
using Enums;

namespace Constants
{
    public class WorkersDataPerkConstants : IPerkConstants
    {
        public Dictionary<PerkType, ConstantPerkData> ConstantsList { get; } =
            new()
            {
                {
                    PerkType.StartMinersLvl1Count,
                    new ConstantPerkData(PerkType.MinersLvl1, CurrencyType.Undefined, 0, 4)
                },
                { PerkType.MinersLvl1, new ConstantPerkData(PerkType.MinersLvl1, CurrencyType.Undefined, 100, 1) },
            };

        public const float XworkerOffset = 0.1f;
        public const float YworkerOffset = 0.4f;
    }
}