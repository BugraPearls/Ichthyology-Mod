using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

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
    }
}
