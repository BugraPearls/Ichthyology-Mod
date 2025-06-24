using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ichthyology.Items.Baits;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ichthyology.Items.Tools
{
    public class SlimyRod : ModItem
    {
        public override LocalizedText Tooltip => Language.GetText("Mods.Ichthyology.Items.Tools.SlimyRod.Tooltip");
        public override LocalizedText DisplayName => Language.GetText("Mods.Ichthyology.Items.Tools.SlimyRod.DisplayName");
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WoodFishingPole);
            Item.fishingPole = 30;
            Item.shoot = ModContent.ProjectileType<Projectiles.SlimeBobber>();
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 10);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BloodFishingRod)
                .AddIngredient(ModContent.ItemType<SlimyBait>(), 20)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
