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
    public class SeaCreature : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (source is EntitySource_FishedOut fisherman)
            {
                if (fisherman.Fisher is Player fisher)
                {
                    FishPlayer.OnSeaCreatureCaught.Invoke(npc, fisher);
                }
            }
        }
    }
}
