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

        public float doubleHookChance = 0;

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
            if (otherPlayer.IchthologyPlayer().displaySCC)
                displaySCC = true;
        }
        public override void ResetInfoAccessories()
        {
            displaySCC = false;
        }
        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            /// <summary>
            /// Here the "Weight" of a Sea creature caught is calculated. Common mob value = 300. Uncommon mob value = 150. Rare mob value = 50
            /// </summary>
            if (Main.rand.NextBool(Math.Min((int)Math.Round(scChance * 100), 100), 100))
            {
                WeightedRandom<int> PossibleMobSpawns = new();
                if (Player.ZoneSkyHeight)
                {
                    PossibleMobSpawns.Add(NPCID.FlyingFish, 300);
                    //insert custom mob here, weight will be 100
                    if (Main.hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.WyvernHead, 50);
                    }
                }
                if (Player.ZoneForest)
                {
                    //insert custom mob here, weight will be 100
                    if (Main.dayTime)
                    {
                        PossibleMobSpawns.Add(NPCID.GreenSlime, 300);
                        PossibleMobSpawns.Add(NPCID.BlueSlime, 300);
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
                if (Player.ZoneNormalUnderground)
                {
                    PossibleMobSpawns.Add(NPCID.GiantWormHead, 150);
                    PossibleMobSpawns.Add(NPCID.BlueJellyfish, 300);
                    if (Main.hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.Mimic, 150);
                        PossibleMobSpawns.Add(NPCID.DiggerHead, 150);
                        PossibleMobSpawns.Add(NPCID.ToxicSludge, 150);
                    }
                }
                if (Player.ZoneNormalCaverns)
                {
                    PossibleMobSpawns.Add(NPCID.GiantWormHead, 150);
                    PossibleMobSpawns.Add(NPCID.Piranha, 300);
                    PossibleMobSpawns.Add(NPCID.Nymph, 50);
                    PossibleMobSpawns.Add(NPCID.BlueJellyfish, 300);
                    PossibleMobSpawns = Utils.AddToWeightedForSame(PossibleMobSpawns, 150, NPCID.GiantShelly, NPCID.GiantShelly2);
                    PossibleMobSpawns = Utils.AddToWeightedForSame(PossibleMobSpawns, 50, NPCID.Crawdad, NPCID.Crawdad2);
                    PossibleMobSpawns = Utils.AddToWeightedForSame(PossibleMobSpawns, 50,
                        NPCID.Salamander,
                        NPCID.Salamander2,
                        NPCID.Salamander3,
                        NPCID.Salamander4,
                        NPCID.Salamander5,
                        NPCID.Salamander6,
                        NPCID.Salamander7,
                        NPCID.Salamander8,
                        NPCID.Salamander9);
                    if(Main.hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.GreenJellyfish, 300);
                        PossibleMobSpawns.Add(NPCID.DiggerHead, 100);
                        PossibleMobSpawns.Add(NPCID.RockGolem, 50);
                        PossibleMobSpawns.Add(NPCID.AnglerFish, 100);
                    }
                }
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
                npcSpawn = PossibleMobSpawns; //This is where its determined which Mob out of all on the Weighted list spawns.
            }
        }
    }
}
