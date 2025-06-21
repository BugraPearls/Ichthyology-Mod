using Ichthyology.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ichthyology.Displays
{
    public class SCCDisplay : InfoDisplay
    {
        public static LocalizedText CurrentSCCText { get; private set; }
        //% chance to fish up an enemy
        public override void SetStaticDefaults()
        {
            CurrentSCCText = this.GetLocalization("CurrentSCCText");
        }
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            return Math.Round(Main.LocalPlayer.IchthyologyPlayer().scChance * 100, 2).ToString() + CurrentSCCText;
        }

        public override bool Active()
        {
            return Main.LocalPlayer.IchthyologyPlayer().displaySCC;
        }
        //public override string HoverTexture => Texture + "_Hover";
    }
}
