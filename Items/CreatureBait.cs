using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ichthyology.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ichthyology.Items
{
    public class CreatureBait : ModItem
    {
        public int creatureBaitPower = 25;
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ApprenticeBait);
            Item.bait = creatureBaitPower;
        }
    }
}
