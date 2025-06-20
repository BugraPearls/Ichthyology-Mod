using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ichthyology.Systems
{
    public static class Utils
    {
        public static FishPlayer IchthologyPlayer(this Player player) => player.GetModPlayer<FishPlayer>();
    }
}
