using System.Collections.Generic;
using Controls;
using Enums;

namespace Controllers
{
    public class PerkController
    {
        private readonly List<Perks> _activePerks;
        public List<Perks> ActiveActivePerks => _activePerks;

        public PerkController()
        {
            _activePerks = new List<Perks> { Perks.AutoConvert };
        }
    }
}