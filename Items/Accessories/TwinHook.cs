using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ichthyology.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ichthyology.Items.Accessories
{
    public class TwinHook : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.FishingBobber);
        }

        public override void UpdateEquip(Player player)
        {
            player.IchthyologyPlayer().TwinHook = true;
        }
    }
}
