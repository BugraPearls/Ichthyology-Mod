using Ichthyology.IDSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Ichthyology.Systems
{
    public class IchthyologyBestiary : ModPlayer
    {
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
    }
}
