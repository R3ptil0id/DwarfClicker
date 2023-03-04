using System.Collections.Generic;
using Data.PerksData;
using Enums;

namespace Models
{
    public class PerksModel
    {
        private readonly Dictionary<PerkType, PerkData> _perks = new Dictionary<PerkType, PerkData>();

        public PerkData[] PerkData { get; private set; }
        public List<PerkType> NotActivePerks { get; } = new();
        public List<PerkType> ActivePerks { get; } = new ();

        public PerkData GetPerkData(PerkType type)
        {
            return _perks.TryGetValue(type, out var data) ? data : null;
        }
        
        public PerkData BuyPerk(PerkType perkType)
        {
            // NotActivePerks.Remove(perkType);
            // ActivePerks.Add(perkType);
            //TODO Recalculate All Depends perks
            
            return null;
        }

        public PerksModel(PerkData[] perks)
        {
            foreach (var perk in perks)
            {
                if (_perks.ContainsKey(perk.PerkType))
                {
                    _perks[perk.PerkType] = perk;
                }
                else
                {   
                    _perks.Add(perk.PerkType, perk);
                }
                
                if (perk.ActiveOnStart)
                {
                    ActivePerks.Add(perk.PerkType);
                }
            }
        }

        public void UpdatePrice(PerkType perkType, int newPrice)
        {
            if (_perks.TryGetValue(perkType, out var data))
            {
                data.Price = newPrice;
            }
        }
    }
}