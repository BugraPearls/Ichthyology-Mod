using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ichthyology.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ichthyology.Items.Baits
{
    public class SlimyBait : ModItem
    {
        public int creatureBaitPower = 25;
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ApprenticeBait);
            Item.bait = creatureBaitPower;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ApprenticeBait, 10)
                .AddIngredient(ItemID.Gel, 10)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}
