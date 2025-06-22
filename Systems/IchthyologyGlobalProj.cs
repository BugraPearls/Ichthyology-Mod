using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Ichthyology.Systems
{
    public class IchthyologyGlobalProj : GlobalProjectile
    {
        public bool fromSC = false;
        public override bool InstancePerEntity => true;
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_Parent parent && ((parent.Entity is NPC npc && npc.IchthyologySeaCreature(out SeaCreature sc) && sc.isASeaCreature)
                || (parent.Entity is Projectile proj && proj.TryGetGlobalProjectile(out IchthyologyGlobalProj global) && global.fromSC)))
            {
                fromSC = true;
            }
        }
    }
}
