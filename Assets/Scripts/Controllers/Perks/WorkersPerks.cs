using Constants;

using Perk = Enums.Perks;

namespace Controllers.Perks
{
    public class WorkersPerks
    {
        public int MinersMaxCount { get; private set; }

        public WorkersPerks()
        {
            MinersMaxCount = WorkersDataConstants.Perks[Perk.MinersLvl1];
        }
        
        public void AddMaxCount(Perk perk)
        {
            // MaxCount = PerkConstants.GetValue(perk)
        }
    }
}