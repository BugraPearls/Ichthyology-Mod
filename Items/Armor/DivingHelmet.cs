using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ichthyology.Items.Baits;
using Ichthyology.Items.Fish;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ichthyology.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class DivingHelmet : ModItem
    {
        public override LocalizedText Tooltip => Language.GetText("Mods.Ichthyology.Items.Armor.DivingHelmet.Tooltip");
        public override LocalizedText DisplayName => Language.GetText("Mods.Ichthyology.Items.Armor.DivingHelmet.DisplayName");
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 10;
            Item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AnglerHat)
                .AddIngredient(ItemID.DivingGear)
                .AddIngredient(ModContent.ItemType<Obelfish>(), 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
