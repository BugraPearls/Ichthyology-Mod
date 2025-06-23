using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ichthyology.Items.Fish
{
    public class WitheredFish : ModItem
    {
        public override LocalizedText Tooltip => Language.GetText("Mods.Ichthyology.Items.Fish.WitheredFish.Tooltip");
        public override LocalizedText DisplayName => Language.GetText("Mods.Ichthyology.Items.Fish.WitheredFish.DisplayName");
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Bass);
        }
    }
}
