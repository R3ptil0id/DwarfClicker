using System.Collections.Generic;
using Constants;
using Data.PerksData;
using Enums;

namespace Controllers.Perks
{
    public abstract class BasePerks : IPerks
    {
        public IPerkData PerkData { get; protected set; }
        
        public List<PerkType> NotActivePerks { get; protected set; }
        public List<PerkType> ActivePerks { get; } = new ();

        public PerkData GetConstantValue(PerkType type)
        {
            return PerkData.PerksData.TryGetValue(type, out var data) ? data : null;
        }
        
        public void BuyPerk(PerkType perkType)
        {
            NotActivePerks.Remove(perkType);
            ActivePerks.Add(perkType);
        }
    }
}