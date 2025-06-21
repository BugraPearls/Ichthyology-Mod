using Ichthyology.Items;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Ichthyology.Systems
{
    /// <summary>
    /// ModPlayer class that contains fields this mod uses.
    /// </summary>
    public class FishPlayer : ModPlayer
    {
        //sc = Sea Creature = Enemies caught by reeling in.

        /// <summary>
        /// Invoked in SeaCreature.cs, OnSpawn hook.
        /// </summary>
        public static Action<NPC, Player> OnSeaCreatureCaught;
        /// <summary>
        /// Invoked in SeaCreature.cs, OnKill hook.
        /// </summary>
        public static Action<NPC, Player> OnSeaCreatureKilled;
        /// <summary>
        /// Chance to catch Sea Creatures.
        /// </summary>
        public float scChance = 0.1f;
        /// <summary>
        /// Bonus chance to not use bait.
        /// </summary>
        public float baitReserveChance = 0;
        /// <summary>
        /// Bonus damage dealt to Sea Creatures.
        /// </summary>
        public float scBonusDamage = 1;
        /// <summary>
        /// Damage reduction towards Sea Creatures.
        /// </summary>
        public float scDamageResist = 1;

        /// <summary>
        /// Chance to throw out another bobber.
        /// </summary>
        public float doubleHookChance = 0;
        /// <summary>
        /// Whether or not SCC to be displayed.
        /// </summary>
        public bool displaySCC;
        public override void ResetEffects()
        {
            scChance = 0.1f;
            baitReserveChance = 0;
            scBonusDamage = 1;
            scDamageResist = 1;
            doubleHookChance = 0;
        }
        public override void RefreshInfoAccessoriesFromTeamPlayers(Player otherPlayer)
        {
            if (otherPlayer.IchthyologyPlayer().displaySCC)
                displaySCC = true;
        }
        public override void ResetInfoAccessories()
        {
            displaySCC = false;
        }
        public override void Load()
        {
            OnSeaCreatureKilled += AddToBestiary;
        }
        public override void Unload()
        {
            OnSeaCreatureKilled -= AddToBestiary;
        }
        static void AddToBestiary(NPC npc, Player player)
        {
            player.IchthyologyBestiary().AddToList(npc.type);
        }

        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            /// <summary>
            /// Here the "Weight" of a Sea creature caught is calculated. Common mob value = 300. Uncommon mob value = 150. Rare mob value = 50
            /// </summary>
            if (Main.rand.NextBool(Math.Min((int)Math.Round(scChance * 100), 100), 100))
            {
                WeightedRandom<int> PossibleMobSpawns = new();
                //Space SC
                if (Player.ZoneSkyHeight)
                {
                    PossibleMobSpawns.Add(NPCID.FlyingFish, 300);
                    //insert custom mob here, weight will be 100
                    if (Main.hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.WyvernHead, 50);
                    }
                }

                //Forest SC
                if (Player.ZoneForest)
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
                if (Player.ZoneNormalUnderground)
                {
                    PossibleMobSpawns.Add(NPCID.GiantWormHead, 150);
                    PossibleMobSpawns.Add(NPCID.BlueJellyfish, 300);
                    if (Main.hardMode)
                    {
                        PossibleMobSpawns = Utils.AddToWeightedForSame(PossibleMobSpawns, 150, NPCID.Mimic, NPCID.DiggerHead, NPCID.ToxicSludge);
                    }
                }

                //Caverns SC
                if (Player.ZoneNormalCaverns)
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
                    if(Main.hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.GreenJellyfish, 300);
                        PossibleMobSpawns.Add(NPCID.DiggerHead, 100);
                        PossibleMobSpawns.Add(NPCID.RockGolem, 50);
                        PossibleMobSpawns.Add(NPCID.AnglerFish, 100);
                    }
                }

                //Snow Biome SC
                if (Player.ZoneSnow)
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
                    if ((Main.bloodMoon && Condition.CorruptWorld.IsMet()) || Player.ZoneCorrupt)
                    {
                        PossibleMobSpawns.Add(NPCID.CorruptPenguin, 150);
                    }
                    if((Main.bloodMoon && Condition.CrimsonWorld.IsMet()) || Player.ZoneCrimson)
                    {
                        PossibleMobSpawns.Add(NPCID.CrimsonPenguin, 150);
                    }
                }

                //Ice Biome SC
                if (Player.ZoneSnow && Player.ZoneRockLayerHeight)
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
                        if (Player.ZoneHallow)
                        {
                            PossibleMobSpawns.Add(NPCID.PigronHallow, 50);
                        }
                        if (Player.ZoneCorrupt)
                        {
                            PossibleMobSpawns.Add(NPCID.PigronCorruption, 50);
                        }
                        if (Player.ZoneCrimson)
                        {
                            PossibleMobSpawns.Add(NPCID.PigronCrimson, 50);
                        }
                    }
                }

                //Desert SC
                if (Player.ZoneDesert)
                {
                    PossibleMobSpawns.Add(NPCID.Antlion, 300);
                    PossibleMobSpawns.Add(NPCID.SandSlime, 300);
                    if(Main.hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.Mummy, 150);
                        if(Player.ZoneHallow)
                        {
                            PossibleMobSpawns.Add(NPCID.LightMummy, 50);
                        }
                        if (Player.ZoneCorrupt)
                        {
                            PossibleMobSpawns.Add(NPCID.DarkMummy, 50);
                        }
                        if (Player.ZoneCrimson)
                        {
                            PossibleMobSpawns.Add(NPCID.BloodMummy, 50);
                        }
                    }
                }

                //Underground Desert SC
                if (Player.ZoneUndergroundDesert)
                {
                    PossibleMobSpawns.Add(NPCID.)
                }

                npcSpawn = PossibleMobSpawns; //This is where its determined which Mob out of all on the Weighted list spawns.
            }
        }
    }
}
