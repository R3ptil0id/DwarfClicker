using System.Linq;
using Constants;
using Enums;

namespace Controllers.Perks
{
    public class WorkersPerks : BasePerks
    {
        public WorkersPerks()
        {
            PerkConstants = new WorkersDataPerkConstants();
            
            ActivePerks.Add(PerkType.StartMinersLvl1Count);
            NotActivePerks = PerkConstants.ConstantsList.Keys.Except(ActivePerks).ToList();
        }
    }
}