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
    public class SlimyBait : ModItem
    {
        public override LocalizedText Tooltip => Language.GetText("Mods.Ichthyology.Items.Baits.SlimyBait.Tooltip");
        public override LocalizedText DisplayName => Language.GetText("Mods.Ichthyology.Items.Baits.SlimyBait.DisplayName");
        public int creatureBaitPower = 25;
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ApprenticeBait);
            Item.bait = creatureBaitPower;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(silver: 10);
        }

        public override void AddRecipes()
        {
            CreateRecipe(5)
                .AddIngredient(ItemID.ApprenticeBait, 5)
                .AddIngredient(ItemID.Gel, 20)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}
