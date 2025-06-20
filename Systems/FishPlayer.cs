using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace Ichthyology.Systems
{
    /// <summary>
    /// ModPlayer class that contains fields this mod uses.
    /// </summary>
    public class FishPlayer : ModPlayer
    {
        //sc = Sea Creature = Enemies caught by reeling in.

        /// <summary>
        /// Chance to catch Sea Creatures.
        /// </summary>
        public float scChance = 0.1f;
        /// <summary>
        /// Bonus chance to not use bait.
        /// </summary>
        public float baitReserveChance = 0;
        /// <summary>
        /// Bonus damage dealt to Sea Creatures.
        /// </summary>
        public float scBonusDamage = 1;
        /// <summary>
        /// Damage reduction towards Sea Creatures.
        /// </summary>
        public float scDamageResist = 1;
    }
}
