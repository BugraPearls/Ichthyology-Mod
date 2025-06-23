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

namespace Ichthyology.Items.Accessories
{
    public class GoldenCage : ModItem
    {
        public override LocalizedText Tooltip => Language.GetText("Mods.Ichthyology.Items.Accessories.GoldenCage.Tooltip");
        public override LocalizedText DisplayName => Language.GetText("Mods.Ichthyology.Items.Accessories.GoldenCage.DisplayName");
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 1;
            Item.accessory = true;
            Item.value = Item.sellPrice(silver: 50);
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
        public override void UpdateEquip(Player player)
        {
            player.IchthyologyPlayer().GoldenCage = true;
        }
    }
}
