using Ichthyology.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ichthyology.Items
{
    public class SCCDisplayItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.FishermansGuide);
        }
        public override void UpdateInfoAccessory(Player player)
        {
            player.IchthologyPlayer().displaySCC = true;
        }
    }
}
