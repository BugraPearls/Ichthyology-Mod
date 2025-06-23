using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ichthyology.Items.Baits
{
    public class FishBait : ModItem
    {
        public int fishBaitPower = 25;
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ApprenticeBait);
            Item.bait = fishBaitPower;
        }
    }
}
