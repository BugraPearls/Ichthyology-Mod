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
        public static LocalizedText CurrentSCC { get; private set; }
        //% chance to fish up an enemy
        public override void SetStaticDefaults()
        {
            CurrentSCC = this.GetLocalization("CurrentSCC");
        }
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            return Math.Round(Main.LocalPlayer.IchthologyPlayer().scChance * 100, 2).ToString();
        }

        public override bool Active()
        {
            return Main.LocalPlayer.IchthologyPlayer().displaySCC;
        }
        //public override string HoverTexture => Texture + "_Hover";
    }
}
