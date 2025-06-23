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
    [AutoloadEquip(EquipType.Legs)]
    public class DivingLeggings : ModItem
    {
        public override LocalizedText Tooltip => Language.GetText("Mods.Ichthyology.Items.Armor.DivingLeggings.Tooltip");
        public override LocalizedText DisplayName => Language.GetText("Mods.Ichthyology.Items.Armor.DivingLeggings.DisplayName");
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 5;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Orange;
        }
        public override void UpdateEquip(Player player)
        {
            player.IchthyologyPlayer().scBonusDamage += 0.2f;
            player.IchthyologyPlayer().doubleHookChance += 0.15f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AnglerPants)
                .AddRecipeGroup(IchthyologyModSystem.Gold, 10)
                .AddIngredient(ModContent.ItemType<WitheredFish>(), 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
