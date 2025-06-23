using Ichthyology.IDSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Ichthyology.Systems
{
    public class IchthyologyBestiary : ModPlayer
    {
        public int TotalAnglerQuest => Player.anglerQuestsFinished;
        public int TotalUniqueSCKills => KilledSeaCreatures.Count;
        public int TotalUniqueFishingCatches => CaughtFishingDrops.Count;

        public List<int> KilledSeaCreatures = new(SeaCreatureIDSets.AllSC.Length);
        public List<int> CaughtFishingDrops = new();
        public void AddToSCList(int IdOfSC)
        {
            if (KilledSeaCreatures.Contains(IdOfSC) == false)
            {
                KilledSeaCreatures.Add(IdOfSC);
            }
        }
        public void AddToCatchList(int IdOfCatch)
        {
            if (CaughtFishingDrops.Contains(IdOfCatch) == false)
            {
                CaughtFishingDrops.Add(IdOfCatch);
            }
        }
        public override void SaveData(TagCompound tag)
        {
            tag.Add("KilledSeaCreatures", KilledSeaCreatures);
            tag.Add("CaughtFishingDrops", CaughtFishingDrops);
        }
        public override void LoadData(TagCompound tag)
        {
            if (tag.TryGet("KilledSeaCreatures", out List<int> seaCreatures))
            {
                KilledSeaCreatures = seaCreatures;
                KilledSeaCreatures = KilledSeaCreatures.Distinct().ToList();
            }

            if (tag.TryGet("CaughtFishingDrops", out List<int> fishingCatches))
            {
                CaughtFishingDrops = fishingCatches;
                CaughtFishingDrops = CaughtFishingDrops.Distinct().ToList();
            }
        }
        public override void PostUpdateMiscEffects()
        {
            Player.IchthyologyPlayer().scChance += (float)Math.Round(TotalUniqueSCKills * 0.003f,2);
            Player.fishingSkill += (int)Math.Round(TotalUniqueFishingCatches * 0.25);
            Player.IchthyologyPlayer().doubleHookChance += (float)Math.Round(Math.Min(TotalAnglerQuest,150) * 0.002f, 2); //Angler is capped at 150 quests.
            if (TotalAnglerQuest >= 50)
            {
                Player.IchthyologyPlayer().questFishCatchChance += 0.25f;
            }
            if (TotalAnglerQuest >= 100)
            {
                Player.IchthyologyPlayer().costCapForStackRaise += 500; //This increases cap for lower value items having their stacks increased
            }
            if (TotalUniqueFishingCatches >= 50)
            {
                Player.IchthyologyPlayer().baitReserveChance += 0.2f;
            }
            if (TotalUniqueFishingCatches >= 100)
            {
                Player.IchthyologyPlayer().doubleHookChance += 0.15f;
                Player.fishingSkill += 10;
            }
            if (TotalUniqueSCKills >= 50)
            {
                Player.IchthyologyPlayer().scBonusDamage += 0.1f;
                Player.IchthyologyPlayer().scDamageResist += 0.1f;
            }
            if (TotalUniqueSCKills >= 100)
            {
                Player.IchthyologyPlayer().scLootIncrease += 0.5f;
            }
        }
    }
}
