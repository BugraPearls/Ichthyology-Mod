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
    public class IchthyologicalSonar : ModItem
    {
        public override LocalizedText Tooltip => Language.GetText("Mods.Ichthyology.Items.Accessories.IchthyologicalSonar.Tooltip");
        public override LocalizedText DisplayName => Language.GetText("Mods.Ichthyology.Items.Accessories.IchthyologicalSonar.DisplayName");
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.FishermansGuide);
        }
        public override void UpdateInfoAccessory(Player player)
        {
            player.IchthyologyPlayer().displaySCC = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.IchthyologyPlayer().scChance += 0.95f;
        }
    }
}
