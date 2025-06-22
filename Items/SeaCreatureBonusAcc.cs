using Ichthyology.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Ichthyology.Items
{
    public class SeaCreatureBonusAcc : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
        }
        public override void UpdateEquip(Player player)
        {
            player.IchthyologyPlayer().scBonusDamage += 2f;
            player.IchthyologyPlayer().scDamageResist += 0.8f;
        }
    }
}
