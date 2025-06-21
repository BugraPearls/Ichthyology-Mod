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

namespace Ichthyology.Systems
{
    public static class Utils
    {
        /// <summary>
        /// Used as Utils.LocVal("type your given value in Localization after Mods.Ichthyology.")
        /// </summary>
        public static string LocVal(string val)
        {
            return Language.GetTextValue("Mods.Ichthyology." + val);
        }
        public static FishPlayer IchthologyPlayer(this Player player) => player.GetModPlayer<FishPlayer>();

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
    }
}
