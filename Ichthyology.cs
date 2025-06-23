using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ichthyology
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class Ichthyology : Mod
    {
        public static RecipeGroup Gold;
        public static List<int> gold = [ItemID.GoldBar, ItemID.PlatinumBar];
        public override void AddRecipeGroups()
        {
            Gold = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.GoldBar)}", [.. gold]);
            RecipeGroup.RegisterGroup(nameof(ItemID.GoldBar), Gold);
        }

    }
}
