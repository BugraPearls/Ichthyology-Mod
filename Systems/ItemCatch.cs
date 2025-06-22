using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Utilities;

namespace Ichthyology.Systems
{
    public class ItemCatch
    {
        public static int CatchItem(Player player, FishingAttempt attempt)
        {
            List<int> LegendaryItems = new List<int>();
            //Space Items
            if (player.ZoneSkyHeight)
            {
                if(attempt.common == true)
                {

                }
                if (Main.hardMode)
                {
                }
            }

            //Forest Items
            else if (player.ZoneForest)
            {
                if (Main.dayTime)
                {
                }
                if (Main.hardMode)
                {   
                }
            }

            //Underground Items
            else if (player.ZoneNormalUnderground)
            {
                if (Main.hardMode)
                {
                }
            }

            //Caverns Items
            else if (player.ZoneNormalCaverns)
            {
                if (Main.hardMode)
                {
                }
            }

            //Snow Biome Items
            else if (player.ZoneSnow)
            {
                if (!player.ZoneRockLayerHeight)
                {
                    if (Main.hardMode)
                    {
                    }
                }
                //Underground Ice Biome Items
                else if (player.ZoneRockLayerHeight)
                {
                    if (Main.hardMode)
                    {
                    }
                }
            }

            //Desert Items
            else if (player.ZoneDesert)
            {
                if (Main.hardMode)
                {
                }
            }

            //Underground Desert Items
            else if (player.ZoneUndergroundDesert)
            {
            }

            //Corruption
            else if (player.ZoneCorrupt)
            {
                if (!(player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight))
                {
                    if (Main.hardMode)
                    {
                    }
                }
                //Underground Corruption
                else if (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight)
                {
                    if (Main.hardMode)
                    {
                    }
                }
            }

            //Crimson Items
            else if (player.ZoneCrimson)
            {
                if (!(player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight))
                {
                    if (Main.hardMode)
                    {
                    }
                }
                //Underground Crimson Items
                else if (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight)
                {
                    if (Main.hardMode)
                    {
                    }
                }
            }

            //Jungle Items
            else if (player.ZoneJungle)
            {
                if (!(player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight))
                {
                    if (Main.hardMode)
                    {
                    }
                }
                //Underground Jungle
                else if (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight)
                {
                    if (Main.hardMode)
                    {
                    }
                }
            }

            //Dungeon Items
            else if (player.ZoneDungeon)
            {
                if (Main.hardMode)
                {
                }
            }

            //Ocean Items
            else if (player.ZoneBeach)
            {
            }

            //Lava Items
            else if (attempt.inLava)
            {
                if (player.ZoneUnderworldHeight)
                {
                    if (Main.hardMode)
                    {
                    }
                }
            }

            return PossibleItemCatches;
        }
    }
}

