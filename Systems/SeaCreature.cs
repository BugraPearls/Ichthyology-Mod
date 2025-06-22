using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ichthyology.Systems
{
    public class SeaCreature : GlobalNPC
    {
        public override bool InstancePerEntity => true;
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
        public override void Load()
        {
            On_ItemDropResolver.ResolveRule += IncreasedSCDrops;
        }

        private static ItemDropAttemptResult IncreasedSCDrops(On_ItemDropResolver.orig_ResolveRule orig, ItemDropResolver self, IItemDropRule rule, DropAttemptInfo info)
        {
            ItemDropAttemptResult tempResult;
            if (rule is CommonDrop drop && info.npc.IchthyologySeaCreature(out SeaCreature sc) && sc.isASeaCreature)
            {
                float baseIncrease = Math.Max(3f + info.player.IchthyologyPlayer().scLootIncrease, 1);
                float currentChance = (float)Math.Max(drop.chanceNumerator * baseIncrease, 1) / Math.Max(drop.chanceDenominator, 1);
                float excessAmount = 0;

                if (currentChance > 1)
                {
                    excessAmount = currentChance - 1;
                    currentChance = 1;
                }

                int oldNumerator = drop.chanceNumerator;
                int oldDenominator = drop.chanceDenominator;
                int stackMult = FishUtils.Randomizer(100 + (int)Math.Round(excessAmount * 100));

                drop.chanceNumerator = Math.Min((int)Math.Round(currentChance * 1000),1000);
                drop.chanceDenominator = 1000;
                drop.amountDroppedMaximum *= stackMult;
                drop.amountDroppedMinimum *= stackMult;
                tempResult = orig(self, rule, info);
                drop.chanceNumerator = oldNumerator; //If not reversed back, this is applied permanently until reloaded.
                drop.chanceDenominator = oldDenominator;
                drop.amountDroppedMaximum /= stackMult;
                drop.amountDroppedMinimum /= stackMult;
                return tempResult;
            }
            tempResult = orig(self, rule, info);

            return tempResult;
        }
    }
}
