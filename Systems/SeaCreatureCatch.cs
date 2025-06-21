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
    public class SeaCreatureCatch
    {
        /// <summary>
        /// Method for calculating which Sea Creature will be caught in CatchFish
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static int CatchCreature(Player player)
        {
            /// <summary>
            /// Here the "Weight" of a Sea creature caught is calculated. Common mob value = 300. Uncommon mob value = 150. Rare mob value = 50
            /// </summary>
            WeightedRandom<int> PossibleMobSpawns = new();
            //Space SC
            if (player.ZoneSkyHeight)
            {
                PossibleMobSpawns.Add(NPCID.FlyingFish, 300);
                //insert custom mob here, weight will be 100
                if (Main.hardMode)
                {
                    PossibleMobSpawns.Add(NPCID.WyvernHead, 50);
                }
            }

            //Forest SC
            if (player.ZoneForest)
            {
                //insert custom mob here, weight will be 100
                if (Main.dayTime)
                {
                    PossibleMobSpawns = Utils.AddToWeightedForSame(PossibleMobSpawns, 300, NPCID.GreenSlime, NPCID.BlueSlime);
                    PossibleMobSpawns.Add(NPCID.PurpleSlime, 150);
                    PossibleMobSpawns.Add(NPCID.Pinky, 50);
                }
                if (Main.dayTime is false)
                {
                    PossibleMobSpawns.Add(NPCID.Zombie, 300);
                    PossibleMobSpawns = Utils.AddToWeightedForSame(PossibleMobSpawns, 150, NPCID.DemonEye, NPCID.DemonEye2);
                    if (Main.hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.Werewolf, 50);
                    }
                }
            }

            //Underground SC
            if (player.ZoneNormalUnderground)
            {
                PossibleMobSpawns.Add(NPCID.GiantWormHead, 150);
                PossibleMobSpawns.Add(NPCID.BlueJellyfish, 300);
                if (Main.hardMode)
                {
                    PossibleMobSpawns = Utils.AddToWeightedForSame(PossibleMobSpawns, 150, NPCID.Mimic, NPCID.DiggerHead, NPCID.ToxicSludge);
                }
            }

            //Caverns SC
            if (player.ZoneNormalCaverns)
            {
                PossibleMobSpawns = Utils.AddToWeightedForSame(PossibleMobSpawns, 300,
                    NPCID.Piranha,
                    NPCID.BlueJellyfish);
                PossibleMobSpawns = Utils.AddToWeightedForSame(PossibleMobSpawns, 150,
                    NPCID.GiantShelly,
                    NPCID.GiantShelly2,
                    NPCID.GiantWormHead);
                PossibleMobSpawns = Utils.AddToWeightedForSame(PossibleMobSpawns, 50,
                    NPCID.Salamander,
                    NPCID.Salamander2,
                    NPCID.Salamander3,
                    NPCID.Salamander4,
                    NPCID.Salamander5,
                    NPCID.Salamander6,
                    NPCID.Salamander7,
                    NPCID.Salamander8,
                    NPCID.Salamander9,
                    NPCID.Nymph,
                    NPCID.Crawdad,
                    NPCID.Crawdad2);
                if (Main.hardMode)
                {
                    PossibleMobSpawns.Add(NPCID.GreenJellyfish, 300);
                    PossibleMobSpawns.Add(NPCID.DiggerHead, 100);
                    PossibleMobSpawns.Add(NPCID.RockGolem, 50);
                    PossibleMobSpawns.Add(NPCID.AnglerFish, 100);
                }
            }

            //Snow Biome SC
            if (player.ZoneSnow)
            {
                if (!player.ZoneRockLayerHeight)
                {
                    if (Main.dayTime)
                    {
                        PossibleMobSpawns.Add(NPCID.IceSlime, 300);
                    }
                    if (Main.dayTime is false)
                    {
                        PossibleMobSpawns = Utils.AddToWeightedForSame(PossibleMobSpawns, 300, NPCID.ZombieEskimo, NPCID.ArmedZombieEskimo);
                    }
                    if (Main.hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.IceGolem, 50);
                        if (Main.dayTime is false)
                        {
                            PossibleMobSpawns.Add(NPCID.IceElemental, 50);
                        }
                    }
                    if ((Main.bloodMoon && Condition.CorruptWorld.IsMet()) || player.ZoneCorrupt)
                    {
                        PossibleMobSpawns.Add(NPCID.CorruptPenguin, 150);
                    }
                    if ((Main.bloodMoon && Condition.CrimsonWorld.IsMet()) || player.ZoneCrimson)
                    {
                        PossibleMobSpawns.Add(NPCID.CrimsonPenguin, 150);
                    }
                }
                //Underground Ice Biome SC
                if (player.ZoneRockLayerHeight)
                {
                    PossibleMobSpawns.Add(NPCID.SnowFlinx, 150);
                    PossibleMobSpawns.Add(NPCID.UndeadViking, 300);
                    if (Main.hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.ArmoredViking, 300);
                        PossibleMobSpawns.Add(NPCID.IceElemental, 50);
                        PossibleMobSpawns.Add(NPCID.IceTortoise, 50);
                        PossibleMobSpawns.Add(NPCID.IceMimic, 50);
                        PossibleMobSpawns.Add(NPCID.IcyMerman, 150);
                        if (player.ZoneHallow)
                        {
                            PossibleMobSpawns.Add(NPCID.PigronHallow, 50);
                        }
                        if (player.ZoneCorrupt)
                        {
                            PossibleMobSpawns.Add(NPCID.PigronCorruption, 50);
                        }
                        if (player.ZoneCrimson)
                        {
                            PossibleMobSpawns.Add(NPCID.PigronCrimson, 50);
                        }
                    }
                }
            }

            //Desert SC
            if (player.ZoneDesert)
            {
                PossibleMobSpawns.Add(NPCID.Antlion, 300);
                PossibleMobSpawns.Add(NPCID.SandSlime, 300);
                if (Main.hardMode)
                {
                    PossibleMobSpawns.Add(NPCID.Mummy, 150);
                    if (player.ZoneHallow)
                    {
                        PossibleMobSpawns.Add(NPCID.LightMummy, 50);
                    }
                    if (player.ZoneCorrupt)
                    {
                        PossibleMobSpawns.Add(NPCID.DarkMummy, 50);
                    }
                    if (player.ZoneCrimson)
                    {
                        PossibleMobSpawns.Add(NPCID.BloodMummy, 50);
                    }
                }
            }

            //Underground Desert SC
            if (player.ZoneUndergroundDesert)
            {
                PossibleMobSpawns.Add(NPCID.TombCrawlerHead, 150);
                PossibleMobSpawns.Add(NPCID.FlyingAntlion, 300);
                if (Main.hardMode)
                {
                    PossibleMobSpawns.Add(NPCID.DesertDjinn, 50);
                    PossibleMobSpawns.Add(NPCID.DuneSplicerHead, 150);
                }
            }

            //Corruption SC
            if (player.ZoneCorrupt)
            {
                PossibleMobSpawns.Add(NPCID.DevourerHead, 150);
                if (!(player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight))
                {
                    PossibleMobSpawns = Utils.AddToWeightedForSame(PossibleMobSpawns, 300,
                        NPCID.LittleEater,
                        NPCID.BigEater,
                        NPCID.EaterofSouls);
                    if (Main.hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.Corruptor, 300);
                        PossibleMobSpawns = Utils.AddToWeightedForSame(PossibleMobSpawns, 150,
                            NPCID.Slimer,
                            NPCID.Slimer2);
                        PossibleMobSpawns.Add(NPCID.SeekerHead, 50);
                    }
                }
                //Underground Corruption SC
                if (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight)
                {
                    if (Main.hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.CorruptGoldfish, 300);
                        PossibleMobSpawns = Utils.AddToWeightedForSame(PossibleMobSpawns, 150,
                            NPCID.SeekerHead,
                            NPCID.Corruptor);
                        PossibleMobSpawns = Utils.AddToWeightedForSame(PossibleMobSpawns, 50,
                            NPCID.CursedHammer,
                            NPCID.BigMimicCorruption);

                    }
                }
            }

            //Crimson SC
            if (player.ZoneCrimson)
            {
                if (!(player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight))
                {
                    PossibleMobSpawns = Utils.AddToWeightedForSame(PossibleMobSpawns, 300,
                        NPCID.Crimera,
                        NPCID.BigCrimera,
                        NPCID.LittleCrimera);
                    PossibleMobSpawns = Utils.AddToWeightedForSame(PossibleMobSpawns, 150,
                        NPCID.FaceMonster,
                        NPCID.BloodCrawler,
                        NPCID.CrimsonGoldfish);
                    if (Main.hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.Herpling, 150);
                        PossibleMobSpawns.Add(NPCID.Crimslime, 150);
                        PossibleMobSpawns.Add(NPCID.BloodFeeder, 300);
                    }
                }
                //Underground Crimson SC
                if (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight)
                {
                    if (Main.hardMode)
                    {

                    }
                }
            }
            
            return PossibleMobSpawns;
        }
    }
}
