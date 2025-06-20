using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
