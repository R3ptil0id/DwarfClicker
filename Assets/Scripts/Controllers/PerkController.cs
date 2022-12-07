using System.Collections.Generic;
using Controls;
using Enums;

namespace Controllers
{
    public class PerkController
    {
        private readonly Installer _installer;
        private readonly List<Perks> _activePerks;

        public List<Perks> ActiveActivePerks => _activePerks;

        public PerkController(Installer installer)
        {
            _installer = installer;
            _activePerks = new List<Perks> { Perks.AutoConvert };
        }
    }
}