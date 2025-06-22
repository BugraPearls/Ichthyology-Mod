using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Ichthyology.Systems
{
    public static class FishUtils
    {
        /// <summary>
        /// Used as Utils.LocVal("type your given value in Localization after Mods.Ichthyology.")
        /// </summary>
        public static string LocVal(string val)
        {
            return Language.GetTextValue("Mods.Ichthyology." + val);
        }
        public static FishPlayer IchthyologyPlayer(this Player player) => player.GetModPlayer<FishPlayer>();
        public static IchthyologyBestiary IchthyologyBestiary(this Player player) => player.GetModPlayer<IchthyologyBestiary>();
        public static bool IchthyologyProjectile(this Projectile proj, out IchthyologyGlobalProj globalProj)
        {
            if (proj.TryGetGlobalProjectile(out IchthyologyGlobalProj gp))
            {
                globalProj = gp;
                return true;
            }
            else
            {
                globalProj = null;
                return false;
            }
        }
        public static bool IchthyologySeaCreature(this NPC npc, out SeaCreature creature)
        {
            if (npc.TryGetGlobalNPC(out SeaCreature sc))
            {
                creature = sc;
                return true;
            }
            else
            {
                creature = null;
                return false;
            }
        }
        /// <summary>
        /// Returns how many times a % chance returns 'true'. Input numToBeRandomized as how the % should be written out, ex. 150% should be Randomizer(150), which will return 1 + 1 with 50% chance. Suggested to use with a for.
        /// </summary>
        /// <param name="numToBeRandomized"></param>
        /// <param name="randomizeTo"></param>
        /// <returns></returns>
        public static int Randomizer(int numToBeRandomized, int randomizeTo = 100)
        {
            if (randomizeTo < 0)
                randomizeTo *= -1;
            if (randomizeTo == 0)
                randomizeTo = 1;

            int amount = numToBeRandomized / randomizeTo;
            numToBeRandomized %= randomizeTo;

            if (numToBeRandomized < 0 && Main.rand.NextBool(numToBeRandomized * -1, randomizeTo))
            {
                amount--;
            }
            else if (Main.rand.NextBool(numToBeRandomized, randomizeTo))
            {
                amount++;
            }
            return amount;
        }

        /// <summary>
        /// Adds to given WeightedRandom list with given weight, for all given id parameters.
        /// </summary>
        public static WeightedRandom<int> AddToWeightedForSame(WeightedRandom<int> WeightedList, double weight, params int[] ids)
        {
            foreach (var item in ids)
            {
                WeightedList.Add(item, weight);
            }
            return WeightedList;
        }

        public static int FloatToIntegerPerc(float value)
        {
            return (int)Math.Round(value * 100,2);
        }
    }
}
