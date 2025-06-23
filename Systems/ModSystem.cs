using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ichthyology.Items.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ichthyology.Systems
{
    internal class IchthyologyModSystem : ModSystem
    {
        public static RecipeGroup Gold;
        public static List<int> gold = [ItemID.GoldBar, ItemID.PlatinumBar];

        public static RecipeGroup Silver;
        public static List<int> silver = [ItemID.SilverBar, ItemID.TungstenBar];

        public static RecipeGroup DemoniteBar;
        public static List<int> demoniteBar = [ItemID.DemoniteBar, ItemID.CrimtaneBar];

        public override void AddRecipeGroups()
        {
            Gold = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.GoldBar)}", [.. gold]);
            RecipeGroup.RegisterGroup(nameof(ItemID.GoldBar), Gold);
            Silver = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.SilverBar)}", [.. silver]);
            RecipeGroup.RegisterGroup(nameof(ItemID.SilverBar), Silver);
            DemoniteBar = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.DemoniteBar)}", [.. demoniteBar]);
            RecipeGroup.RegisterGroup(nameof(ItemID.DemoniteBar), DemoniteBar);
        }

        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.TryGetResult(ItemID.FishFinder, out Item result))
                {
                    recipe.AddIngredient(ModContent.ItemType<IchthyologicalSonar>());
                }
            }
        }
    }
}
