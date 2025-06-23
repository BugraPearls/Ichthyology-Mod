using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ichthyology.Items.Tools
{
    public class SlimyRod : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WoodFishingPole);

            Item.fishingPole = 30;
        }
    }
}
