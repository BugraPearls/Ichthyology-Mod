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
        public List<int> KilledSeaCreatures = new(FishIDSets.AllSC.Length);
        public void AddToList(int IdOfSC)
        {
            if (KilledSeaCreatures.Contains(IdOfSC) == false)
            {
                KilledSeaCreatures.Add(IdOfSC);
            }
        }
        public override void SaveData(TagCompound tag)
        {
            tag.Add("KilledSeaCreatures", KilledSeaCreatures);
        }
        public override void LoadData(TagCompound tag)
        {
            if (tag.TryGet("KilledSeaCreatures", out List<int> seaCreatures))
            {
                KilledSeaCreatures = seaCreatures;
                KilledSeaCreatures = KilledSeaCreatures.Distinct().ToList();
            }
        }
    }
}
