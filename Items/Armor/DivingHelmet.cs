using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ichthyology.Items.Baits;
using Ichthyology.Items.Fish;
using Ichthyology.Systems;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ichthyology.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class DivingHelmet : ModItem
    {
        public override LocalizedText Tooltip => Language.GetText("Mods.Ichthyology.Items.Armor.DivingHelmet.Tooltip");
        public override LocalizedText DisplayName => Language.GetText("Mods.Ichthyology.Items.Armor.DivingHelmet.DisplayName");
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 8;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Orange;
        }
        public override void UpdateEquip(Player player)
        {
            player.IchthyologyPlayer().scChance += 0.2f;
            player.breathEffectiveness += 6;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<DivingChestplate>() && legs.type == ModContent.ItemType<DivingLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Set Bonus: Increases loot from Sea Creatures";
            player.IchthyologyPlayer().scLootIncrease += 1f;
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
