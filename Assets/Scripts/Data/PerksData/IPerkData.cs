using System.Collections.Generic;
using Constants;
using Enums;

namespace Data.PerksData
{
    public interface IPerkData
    {
        Dictionary<PerkType, PerkData> PerksData { get; }
    }
}