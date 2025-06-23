using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ichthyology.Items.Fish
{
    public class Obelfish : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Bass);
        }
    }
}
