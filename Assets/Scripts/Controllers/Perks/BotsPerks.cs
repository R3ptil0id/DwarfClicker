using Constants;

namespace Controllers.Perks
{
    public class BotsPerks : BasePerks
    {
        public int MaxCount { get; private set; }

        public BotsPerks()
        {
            MaxCount = DataConstants.BotMaxCountOnStart;
        }
        
        public void AddMaxCount(Enums.PerkType perkType)
        {
            // MaxCount = PerkConstants.GetValue(perk)
        }
    }
}