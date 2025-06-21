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
        public bool isASeaCreature = false;
        public int fisherWhoAmI = -1;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return FishIDSets.AllSC[entity.type];
        }
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (source is EntitySource_FishedOut fisherman)
            {
                isASeaCreature = true;
                if (fisherman.Fisher is Player fisher)
                {
                    fisherWhoAmI = fisher.whoAmI;
                    FishPlayer.OnSeaCreatureCaught?.Invoke(npc, fisher);
                }
            }
        }
        public override void OnKill(NPC npc)
        {
            if (isASeaCreature && fisherWhoAmI >= 0)
            {
                FishPlayer.OnSeaCreatureKilled?.Invoke(npc, Main.player[fisherWhoAmI]);
            }
        }
    }
}
