using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ichthyology.Items.Fish;
using Ichthyology.Systems;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ichthyology.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class DivingChestplate : ModItem
    {
        public override LocalizedText Tooltip => Language.GetText("Mods.Ichthyology.Items.Armor.DivingChestplate.Tooltip");
        public override LocalizedText DisplayName => Language.GetText("Mods.Ichthyology.Items.Armor.DivingChestplate.DisplayName");
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 10;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Orange;
        }
        public override void UpdateEquip(Player player)
        {
            player.IchthyologyPlayer().scDamageResist += 0.2f;
            player.fishingSkill += 10;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AnglerVest)
                .AddRecipeGroup(IchthyologyModSystem.Gold, 10)
                .AddIngredient(ModContent.ItemType<Puffer>(), 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
