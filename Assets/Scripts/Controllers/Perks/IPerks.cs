using System.Collections.Generic;
using Constants;
using Enums;

namespace Controllers.Perks
{
    public interface IPerks
    {
        IPerkConstants PerkConstants { get; }
        List<PerkType> NotActivePerks { get; }
        List<PerkType> ActivePerks { get; }
    }
}