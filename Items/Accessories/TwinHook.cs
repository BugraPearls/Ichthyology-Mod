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
    public class TwinHook : ModItem
    {
        public override LocalizedText Tooltip => Language.GetText("Mods.Ichthyology.Items.Accessories.TwinHook.Tooltip");
        public override LocalizedText DisplayName => Language.GetText("Mods.Ichthyology.Items.Accessories.TwinHook.DisplayName");
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.FishingBobber);
            Item.rare = ItemRarityID.Green;
        }

        public override void UpdateEquip(Player player)
        {
            player.IchthyologyPlayer().doubleHookChance += 0.2f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(IchthyologyModSystem.DemoniteBar, 5)
                .AddRecipeGroup(IchthyologyModSystem.Gold, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
