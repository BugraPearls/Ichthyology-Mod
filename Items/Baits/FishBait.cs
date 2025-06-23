using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ichthyology.Items.Baits
{
    public class FishBait : ModItem
    {
        public override LocalizedText Tooltip => Language.GetText("Mods.Ichthyology.Items.Baits.FishBait.Tooltip");
        public override LocalizedText DisplayName => Language.GetText("Mods.Ichthyology.Items.Baits.FishBait.DisplayName");
        public int fishBaitPower = 25;
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ApprenticeBait);
            Item.bait = fishBaitPower;
            Item.rare = ItemRarityID.Green;
        }

        public override void AddRecipes()
        {
            CreateRecipe(5)
                .AddIngredient(ItemID.ApprenticeBait, 10)
                .AddIngredient(ItemID.Bass, 5)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}
