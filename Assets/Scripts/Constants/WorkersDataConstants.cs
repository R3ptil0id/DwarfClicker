using System.Collections.Generic;
using Enums;
using UnityEngine;
using Perk = Enums.Perks;

namespace Constants
{
    public static class WorkersDataConstants
    {
        public static readonly Dictionary<Perks, int> Perks = new (){
            {Perk.MinersLvl1, 4},
        };

        public const float XworkerOffset = 0.1f;
        public const float YworkerOffset = 0.4f;
    }
}