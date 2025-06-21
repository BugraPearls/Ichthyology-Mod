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
            if (Main.rand.NextBool(Math.Min((int)Math.Round(scChance * 100), 100), 100))
            {
                WeightedRandom<int> PossibleMobSpawns = new();
                if (Player.ZoneSkyHeight)
                {
                    PossibleMobSpawns.Add(NPCID.FlyingFish, 850);
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
                        PossibleMobSpawns.Add(NPCID.GreenSlime, );
                        PossibleMobSpawns.Add(NPCID.BlueSlime, );
                        PossibleMobSpawns.Add(NPCID.PurpleSlime, );
                        PossibleMobSpawns.Add(NPCID.Pinky, );
                    }
                    if (Main.dayTime is false)
                    {
                        PossibleMobSpawns.Add(NPCID.Zombie, );
                        PossibleMobSpawns.Add(NPCID.Demon)
                        PossibleMobSpawns.Add(NPCID.Werewolf, )
                    }
                }
                npcSpawn = PossibleMobSpawns; //This is where its determined which Mob out of all on the Weighted list spawns.
            }
        }
    }
}
