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
            /// Here the "Weight" of a Sea creature caught is calculated. Weight base value will be 1000 = 100% in PreHardmode
            /// </summary>
            if (Main.rand.NextBool(Math.Min((int)Math.Round(scChance * 100), 100), 100))
            {
                WeightedRandom<int> PossibleMobSpawns = new();
                if (Player.ZoneSkyHeight)
                {
                    PossibleMobSpawns.Add(NPCID.FlyingFish, 900);
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
                        PossibleMobSpawns.Add(NPCID.PurpleSlime, 250);
                        PossibleMobSpawns.Add(NPCID.Pinky, 50);
                    }
                    if (Main.dayTime is false)
                    {
                        PossibleMobSpawns.Add(NPCID.Zombie, 500);
                        PossibleMobSpawns = Utils.AddToWeightedForSame(PossibleMobSpawns, 250, NPCID.DemonEye, NPCID.DemonEye2);
                        if (Main.hardMode)
                        {
                            PossibleMobSpawns.Add(NPCID.Werewolf, 100);
                        }
                    }
                }
                if (Player.ZoneNormalUnderground)
                {
                    PossibleMobSpawns.Add(NPCID.GiantWormHead, 200);
                    PossibleMobSpawns.Add(NPCID.BlueJellyfish, 800);
                    if (Main.hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.Mimic, 100);
                        PossibleMobSpawns.Add(NPCID.DiggerHead, 200);
                        PossibleMobSpawns.Add(NPCID.ToxicSludge, 200);
                    }
                }
                if (Player.ZoneNormalCaverns)
                {
                    PossibleMobSpawns.Add(NPCID.GiantWormHead, 100);
                    PossibleMobSpawns.Add(NPCID.Piranha, 150);
                    PossibleMobSpawns.Add(NPCID.Nymph, 50);
                    PossibleMobSpawns.Add(NPCID.BlueJellyfish, 500);
                    PossibleMobSpawns.Add(NPCID.GiantShelly, 100);
                    PossibleMobSpawns.Add(NPCID.GiantShelly2, 100);
                    if(Main.hardMode)
                    {
                        PossibleMobSpawns.Add(NPCID.GreenJellyfish);
                        PossibleMobSpawns.Add(NPCID.DiggerHead);
                        PossibleMobSpawns.Add(NPCID.RockGolem);
                        PossibleMobSpawns.Add(NPCID.AnglerFish);
                    }
                }
                npcSpawn = PossibleMobSpawns; //This is where its determined which Mob out of all on the Weighted list spawns.
            }
        }
    }
}
