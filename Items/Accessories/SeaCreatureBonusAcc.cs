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
    public class SeaCreatureBonusAcc : ModItem
    {
        public override LocalizedText Tooltip => Language.GetText("Mods.Ichthyology.Items.Accessories.SeaCreatureBonusAcc.Tooltip");
        public override LocalizedText DisplayName => Language.GetText("Mods.Ichthyology.Items.Accessories.SeaCreatureBonusAcc.DisplayName");
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.Master;
        }
        public override void UpdateEquip(Player player) //TODO: OBVIOUSLY CHANGE THESE
        {
            player.IchthyologyPlayer().scBonusDamage += 2f;
            player.IchthyologyPlayer().scDamageResist += 0.8f;
            player.IchthyologyPlayer().doubleHookChance += 1.5f;
            player.IchthyologyPlayer().baitReserveChance += 2f;
        }
    }
}
