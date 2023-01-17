using Constants;

namespace Controllers.Perks
{
    public class BotsPerks
    {
        public int MaxCount { get; private set; }

        public BotsPerks()
        {
            MaxCount = DataConstants.BotMaxCountOnStart;
        }
        
        public void AddMaxCount(Enums.Perks perk)
        {
            // MaxCount = PerkConstants.GetValue(perk)
        }
    }
}