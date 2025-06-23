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
        public static int CatchCreature(Player player, FishingAttempt attempt)
        {
            /// <summary>
            /// Here the "Weight" of a Sea creature caught is calculated. Common mob value = 300. Uncommon mob value = 150. Rare mob value = 50
            /// </summary>
            WeightedRandom<int> PossibleMobSpawns = new();
            bool hardMode = Main.hardMode;
            bool plantDead = NPC.downedPlantBoss;
            bool cavernLayer = player.ZoneRockLayerHeight;
            bool undergroundLayer = player.ZoneDirtLayerHeight;
            bool nightTime = Main.dayTime == false;

            if (attempt.inLava) //Lava catches
            {
                PossibleMobSpawns.Add(NPCID.Skeleton, 300);
                if (hardMode)
                {
                    if (plantDead)
                    {
                        PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 150,
                            NPCID.BlueArmoredBones,
                            NPCID.BlueArmoredBonesMace,
                            NPCID.BlueArmoredBonesNoPants,
                            NPCID.BlueArmoredBonesSword,
                            NPCID.RustyArmoredBonesAxe,
                            NPCID.RustyArmoredBonesFlail,
                            NPCID.RustyArmoredBonesSword,
                            NPCID.RustyArmoredBonesSwordNoArmor,
                            NPCID.HellArmoredBones,
                            NPCID.HellArmoredBonesMace,
                            NPCID.HellArmoredBonesSpikeShield,
                            NPCID.HellArmoredBonesSword);
                    }
                    PossibleMobSpawns.Add(NPCID.Mimic, 50);
                }
                if (player.ZoneUnderworldHeight)
                {
                    PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 300,
                        NPCID.Demon,
                        NPCID.LavaSlime,
                        NPCID.Hellbat);
                    PossibleMobSpawns.Add(NPCID.BoneSerpentHead, 150);
                    if (hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.Lavabat, 300);
                        PossibleMobSpawns.Add(NPCID.RedDevil, 50);
                    }
                }
            }
            else if (attempt.inHoney) //Honey catches
            {
                PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 300,
                    NPCID.Hornet,
                    NPCID.Bee,
                    NPCID.BeeSmall);
            }
            else //All Water catches
            {
                //Blood Moon SC
                if (Main.bloodMoon)
                {
                    PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 300,
                            NPCID.ZombieMerman,
                            NPCID.EyeballFlyingFish);

                    if (!NPC.unlockedSlimeRedSpawn)
                    {
                        PossibleMobSpawns.Add(NPCID.TownSlimeRed, 300);
                    }
                    else if (hardMode)
                    {
                        PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 300,
                            NPCID.BloodEelHead,
                            NPCID.GoblinShark);
                        PossibleMobSpawns.Add(NPCID.BloodNautilus, 120);
                    }
                }
                //Space SC
                else if (player.ZoneSkyHeight)
                {
                    PossibleMobSpawns.Add(NPCID.WindyBalloon, 300);
                    PossibleMobSpawns.Add(NPCID.FlyingFish, 300);
                    //insert custom mob here, weight will be 100
                    if (hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.WyvernHead, 50);
                    }
                }

                //Forest SC
                else if (player.ZoneForest)
                {
                    //insert custom mob here, weight will be 100
                    if (Main.dayTime)
                    {
                        PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 300, NPCID.GreenSlime, NPCID.BlueSlime);
                        PossibleMobSpawns.Add(NPCID.PurpleSlime, 150);
                        PossibleMobSpawns.Add(NPCID.Pinky, 50);
                    }
                    if (Main.dayTime is false)
                    {
                        PossibleMobSpawns.Add(NPCID.Zombie, 300);
                        PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 150, NPCID.DemonEye, NPCID.DemonEye2);
                        if (hardMode)
                        {
                            PossibleMobSpawns.Add(NPCID.Werewolf, 50);
                        }
                    }
                }

                //Underground SC
                else if (player.ZoneNormalUnderground)
                {
                    PossibleMobSpawns.Add(NPCID.GiantWormHead, 150);
                    PossibleMobSpawns.Add(NPCID.BlueJellyfish, 300);
                    if (hardMode)
                    {
                        PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 150, NPCID.Mimic, NPCID.DiggerHead, NPCID.ToxicSludge);
                    }
                }

                //Caverns SC
                else if (player.ZoneNormalCaverns)
                {
                    PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 300,
                        NPCID.Piranha,
                        NPCID.BlueJellyfish);
                    PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 150,
                        NPCID.GiantShelly,
                        NPCID.GiantShelly2,
                        NPCID.GiantWormHead);
                    PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 50,
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
                    if (hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.GreenJellyfish, 300);
                        PossibleMobSpawns.Add(NPCID.DiggerHead, 100);
                        PossibleMobSpawns.Add(NPCID.RockGolem, 50);
                        PossibleMobSpawns.Add(NPCID.AnglerFish, 100);
                    }
                }

                //Snow Biome SC
                else if (FishUtils.SnowBiomeVanillaRules(player))
                {
                    if (!cavernLayer)
                    {
                        if (Main.dayTime)
                        {
                            PossibleMobSpawns.Add(NPCID.IceSlime, 300);
                        }
                        if (Main.dayTime is false)
                        {
                            PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 300,
                                NPCID.ZombieEskimo,
                                NPCID.ArmedZombieEskimo);
                        }
                        if (hardMode)
                        {
                            PossibleMobSpawns.Add(NPCID.IceGolem, 50);
                            if (Main.dayTime is false)
                            {
                                PossibleMobSpawns.Add(NPCID.IceElemental, 50);
                            }
                        }
                        if ((Main.bloodMoon && Condition.CorruptWorld.IsMet()) || FishUtils.CorruptBiomeVanillaRules(player, attempt.heightLevel))
                        {
                            PossibleMobSpawns.Add(NPCID.CorruptPenguin, 150);
                        }
                        if ((Main.bloodMoon && Condition.CrimsonWorld.IsMet()) || FishUtils.CrimsonBiomeVanillaRules(player, attempt.heightLevel))
                        {
                            PossibleMobSpawns.Add(NPCID.CrimsonPenguin, 150);
                        }
                    }
                    //Underground Ice Biome SC
                    else if (cavernLayer)
                    {
                        PossibleMobSpawns.Add(NPCID.SnowFlinx, 150);
                        PossibleMobSpawns.Add(NPCID.UndeadViking, 300);
                        if (hardMode)
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
                            else if (FishUtils.CorruptBiomeVanillaRules(player, attempt.heightLevel))
                            {
                                PossibleMobSpawns.Add(NPCID.PigronCorruption, 50);
                            }
                            else if (FishUtils.CrimsonBiomeVanillaRules(player, attempt.heightLevel))
                            {
                                PossibleMobSpawns.Add(NPCID.PigronCrimson, 50);
                            }
                        }
                    }
                }

                //Desert SC
                else if (player.ZoneDesert)
                {
                    PossibleMobSpawns.Add(NPCID.Antlion, 300);
                    PossibleMobSpawns.Add(NPCID.SandSlime, 300);
                    if (hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.Mummy, 150);
                        if (player.ZoneHallow)
                        {
                            PossibleMobSpawns.Add(NPCID.LightMummy, 50);
                        }
                        else if (FishUtils.CorruptBiomeVanillaRules(player, attempt.heightLevel))
                        {
                            PossibleMobSpawns.Add(NPCID.DarkMummy, 50);
                        }
                        else if (FishUtils.CrimsonBiomeVanillaRules(player, attempt.heightLevel))
                        {
                            PossibleMobSpawns.Add(NPCID.BloodMummy, 50);
                        }
                    }
                }

                //Underground Desert SC
                else if (player.ZoneUndergroundDesert)
                {
                    PossibleMobSpawns.Add(NPCID.TombCrawlerHead, 150);
                    PossibleMobSpawns.Add(NPCID.FlyingAntlion, 300);
                    if (hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.DesertDjinn, 50);
                        PossibleMobSpawns.Add(NPCID.DuneSplicerHead, 150);
                    }
                }

                //Corruption SC
                else if (FishUtils.CorruptBiomeVanillaRules(player, attempt.heightLevel))
                {
                    PossibleMobSpawns.Add(NPCID.DevourerHead, 150);
                    if (!(undergroundLayer || cavernLayer))
                    {
                        PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 300,
                            NPCID.LittleEater,
                            NPCID.BigEater,
                            NPCID.EaterofSouls);
                        if (hardMode)
                        {
                            PossibleMobSpawns.Add(NPCID.Corruptor, 300);
                            PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 150,
                                NPCID.Slimer,
                                NPCID.Slimer2);
                            PossibleMobSpawns.Add(NPCID.SeekerHead, 50);
                        }
                    }
                    //Underground Corruption SC
                    else if (undergroundLayer || cavernLayer)
                    {
                        if (hardMode)
                        {
                            PossibleMobSpawns.Add(NPCID.CorruptGoldfish, 300);
                            PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 150,
                                NPCID.SeekerHead,
                                NPCID.Corruptor);
                            PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 50,
                                NPCID.CursedHammer,
                                NPCID.BigMimicCorruption);

                        }
                    }
                }

                //Crimson SC
                else if (FishUtils.CrimsonBiomeVanillaRules(player, attempt.heightLevel))
                {
                    if (!(undergroundLayer || cavernLayer))
                    {
                        PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 300,
                            NPCID.Crimera,
                            NPCID.BigCrimera,
                            NPCID.LittleCrimera);
                        PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 150,
                            NPCID.FaceMonster,
                            NPCID.BloodCrawler,
                            NPCID.CrimsonGoldfish);
                        if (hardMode)
                        {
                            PossibleMobSpawns.Add(NPCID.Herpling, 150);
                            PossibleMobSpawns.Add(NPCID.Crimslime, 150);
                            PossibleMobSpawns.Add(NPCID.BloodFeeder, 300);
                        }
                    }
                    //Underground Crimson SC
                    else if (undergroundLayer || cavernLayer)
                    {
                        if (hardMode)
                        {
                            PossibleMobSpawns.Add(NPCID.FloatyGross, 150);
                            PossibleMobSpawns.Add(NPCID.IchorSticker, 150);
                            PossibleMobSpawns.Add(NPCID.BloodJelly, 300);
                            PossibleMobSpawns.Add(NPCID.BloodFeeder, 300);
                            PossibleMobSpawns.Add(NPCID.BigMimicCrimson, 50);
                        }
                    }
                }

                //Jungle SC
                else if (FishUtils.JungleBiomeVanillaRules(player))
                {
                    if (!(undergroundLayer || cavernLayer))
                    {
                        PossibleMobSpawns.Add(NPCID.Piranha, 300);
                        PossibleMobSpawns.Add(NPCID.JungleSlime, 300);
                        if (Main.dayTime is false)
                        {
                            PossibleMobSpawns.Add(NPCID.DoctorBones, 50);
                        }
                        if (hardMode)
                        {
                            PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 50,
                                NPCID.GiantTortoise,
                                NPCID.Derpling);
                            PossibleMobSpawns.Add(NPCID.Arapaima, 150);
                            PossibleMobSpawns.Add(NPCID.AnglerFish, 300);
                        }
                    }
                    //Underground Jungle
                    else if (undergroundLayer || cavernLayer)
                    {
                        PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 300,
                            NPCID.JungleCreeper,
                            NPCID.Piranha);
                        PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 50,
                            NPCID.Hornet,
                            NPCID.HornetFatty,
                            NPCID.HornetHoney,
                            NPCID.HornetLeafy,
                            NPCID.HornetSpikey,
                            NPCID.HornetStingy,
                            NPCID.BigHornetFatty,
                            NPCID.BigHornetHoney,
                            NPCID.BigHornetLeafy,
                            NPCID.BigHornetSpikey,
                            NPCID.BigHornetStingy,
                            NPCID.BigMossHornet,
                            NPCID.GiantMossHornet,
                            NPCID.LittleHornetFatty,
                            NPCID.LittleHornetHoney,
                            NPCID.LittleHornetLeafy,
                            NPCID.LittleHornetSpikey,
                            NPCID.LittleHornetStingy,
                            NPCID.LittleMossHornet,
                            NPCID.MossHornet);
                        if (hardMode)
                        {
                            PossibleMobSpawns.Add(NPCID.Arapaima, 150);
                            PossibleMobSpawns.Add(NPCID.GiantTortoise, 50);
                            PossibleMobSpawns.Add(NPCID.Moth, 50);
                            PossibleMobSpawns.Add(NPCID.LacBeetle, 50);
                        }
                    }
                }

                //Dungeon SC
                else if (FishUtils.DungeonBiomeVanillaRules(player))
                {
                    //insert custom enemy here
                    PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 300,
                        NPCID.AngryBones,
                        NPCID.AngryBonesBig,
                        NPCID.AngryBonesBigHelmet,
                        NPCID.AngryBonesBigMuscle);
                    PossibleMobSpawns.Add(NPCID.CursedSkull, 150);
                    PossibleMobSpawns.Add(NPCID.DungeonSlime, 50);
                    if (hardMode && plantDead)
                    {
                        PossibleMobSpawns.Add(NPCID.DungeonSpirit, 150);
                    }
                }

                //Ocean SC
                else if (player.ZoneBeach)
                {
                    PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 300,
                        NPCID.PinkJellyfish,
                        NPCID.Crab);
                    PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 150,
                        NPCID.Shark,
                        NPCID.SeaSnail);
                    PossibleMobSpawns.Add(NPCID.Squid, 50);
                }

                //Hallow SC
                else if (player.ZoneHallow)
                {
                    PossibleMobSpawns.Add(NPCID.Pixie, 300);
                    PossibleMobSpawns.Add(NPCID.RainbowSlime, 50);
                    if (nightTime)
                    {
                        PossibleMobSpawns.Add(NPCID.Gastropod, 150);
                    }
                }

                //Glowshroom SC
                else if (player.ZoneGlowshroom)
                {
                    PossibleMobSpawns.Add(NPCID.SporeSkeleton, 300);
                    PossibleMobSpawns = FishUtils.AddToWeightedForSame(PossibleMobSpawns, 150,
                        NPCID.ZombieMushroom,
                        NPCID.ZombieMushroomHat);
                    if (hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.FungoFish, 150);
                    }
                }
            }
            return PossibleMobSpawns;
        }
    }
}
