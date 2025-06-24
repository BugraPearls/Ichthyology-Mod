using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ichthyology.Systems;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ichthyology.Items.Baits
{
    public class MagneticBait : ModItem
    {
        public override LocalizedText Tooltip => Language.GetText("Mods.Ichthyology.Items.Baits.MagneticBait.Tooltip");
        public override LocalizedText DisplayName => Language.GetText("Mods.Ichthyology.Items.Baits.MagneticBait.DisplayName");
        public int magneticPower = 35;
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ApprenticeBait);
            Item.bait = magneticPower;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(silver: 10);
        }
        public override void AddRecipes()
        {
            CreateRecipe(5)
                .AddRecipeGroup(IchthyologyModSystem.Gold, 5)
                .AddRecipeGroup(IchthyologyModSystem.Silver, 5)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}
