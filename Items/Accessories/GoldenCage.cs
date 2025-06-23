using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ichthyology.Systems;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ichthyology.Items.Accessories
{
    public class GoldenCage : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 1;
            Item.accessory = true;
            Item.value = Terraria.Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(Ichthyology.Gold, 12)
                .AddIngredient(ItemID.ApprenticeBait, 10)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
