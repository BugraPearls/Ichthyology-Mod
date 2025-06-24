using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ichthyology.Items.Fish
{
    public class Obelfish : ModItem
    {
        public override LocalizedText Tooltip => Language.GetText("Mods.Ichthyology.Items.Fish.Obelfish.Tooltip");
        public override LocalizedText DisplayName => Language.GetText("Mods.Ichthyology.Items.Fish.Obelfish.DisplayName");
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Bass);
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 50);
        }
    }
}
