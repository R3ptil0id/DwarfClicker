using System.Collections.Generic;
using Constants;
using Data.PerksData;
using Enums;

namespace Controllers.Perks
{
    public interface IPerks
    {
        IPerkData PerkData { get; }
        List<PerkType> NotActivePerks { get; }
        List<PerkType> ActivePerks { get; }
        void BuyPerk(PerkType perkType);
        PerkData GetConstantValue(PerkType type);
    }
}