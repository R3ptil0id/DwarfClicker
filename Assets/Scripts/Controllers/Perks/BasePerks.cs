using System.Collections.Generic;
using Constants;
using Enums;

namespace Controllers.Perks
{
    public abstract class BasePerks : IPerks
    {
        public IPerkConstants PerkConstants { get; protected set; }
        
        public List<PerkType> NotActivePerks { get; protected set; }
        public List<PerkType> ActivePerks { get; } = new ();

        public int GetConstantValue(PerkType type)
        {
            return PerkConstants.ConstantsList.TryGetValue(type, out var data) ? data.Value : 0;
        }
        
        public void AddMaxCount(PerkType perkType)
        {
            ActivePerks.Add(perkType);
        }
    }
}