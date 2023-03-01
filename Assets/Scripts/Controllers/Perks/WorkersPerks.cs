using System.Linq;
using Constants;
using Data.PerksData;
using Enums;

namespace Controllers.Perks
{
    public class WorkersPerks : BasePerks
    {
        public WorkersPerks()
        {
            PerkData = new PerkWorkersData();
            
            ActivePerks.Add(PerkType.StartMinersLvl1Count);
            NotActivePerks = PerkData.PerksData.Keys.Except(ActivePerks).ToList();
        }
    }
}