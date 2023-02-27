using System.Collections.Generic;
using Enums;

namespace Constants
{
    public interface IPerkConstants
    {
        Dictionary<PerkType, ConstantPerkData> ConstantsList { get; }
    }
}