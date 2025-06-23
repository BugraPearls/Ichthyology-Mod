using Ichthyology.Items;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
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
        public float scBonusDamage = 0;
        /// <summary>
        /// Damage reduction towards Sea Creatures.
        /// </summary>
        public float scDamageResist = 0;
        /// <summary>
        /// Chance to throw out another bobber.
        /// </summary>
        public float doubleHookChance = 0;
        /// <summary>
        /// Amount which will increase the loot obtained from Sea Creatures.
        /// </summary>
        public float scLootIncrease = 0;
        /// <summary>
        /// Chance to catch quest fish IF the given catch can be a uncommon catch.
        /// </summary>
        public float questFishCatchChance = 0.25f;
        /// <summary>
        /// Items up to this price point can have their stack raised as they are caught up. Defaults to 10 silver.
        /// </summary>
        public int costCapForStackRaise = 1000;
        /// <summary>
        /// Whether or not SCC to be displayed.
        /// </summary>
        public bool displaySCC;
        public bool GoldenCage = false;

        public override void ResetEffects()
        {
            scChance = 0.1f;
            baitReserveChance = 0;
            scBonusDamage = 0;
            scDamageResist = 0;
            doubleHookChance = 0;
            scLootIncrease = 0;
            questFishCatchChance = 0.25f;
            costCapForStackRaise = 1000;
            GoldenCage = false;
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
            On_Player.ItemCheck_CheckFishingBobber_PullBobber += AdjustEnemySpawns;
            On_Projectile.FishingCheck_RollEnemySpawns += RevampVanillaSeaCreatureSystem;
            On_Projectile.FishingCheck_RollItemDrop += RevampVanillaItemDropSystem;
        }
        private static void AdjustEnemySpawns(On_Player.orig_ItemCheck_CheckFishingBobber_PullBobber orig, Player self, Projectile bobber, int baitTypeUsed)
        { //Same as vanilla method, except more clarified and check for LocalAI is checking if its not 0, rather than if above. Also adds point.Y -= 64 for WindyBalloon SC.
            if (baitTypeUsed == ItemID.TruffleWorm)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(self.whoAmI, 370);
                }
                else
                {
                    NetMessage.SendData(61, -1, -1, null, self.whoAmI, 370f);
                }
                bobber.ai[0] = 2f;
            }
            else if (bobber.localAI[1] < 0f)
            {
                Point point = new((int)bobber.position.X, (int)bobber.position.Y);
                int enemyID = (int)(0f - bobber.localAI[1]);
                if (bobber.TryGetGlobalProjectile(out BobberGlobal bobberGlobal) && bobberGlobal.spawningIntIsNegative)
                {
                    enemyID *= -1;
                    bobberGlobal.spawningIntIsNegative = false;
                }
                if (enemyID == NPCID.BloodNautilus)
                {
                    point.Y += 64;
                }
                if (enemyID == NPCID.WindyBalloon)
                {
                    point.Y -= 64;
                }
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.FishOutNPC, -1, -1, null, point.X / 16, point.Y / 16, enemyID);
                }
                else
                {
                    if (enemyID == 682)
                    {
                        NPC.unlockedSlimeRedSpawn = true;
                    }
                    NPC.NewNPC(new EntitySource_FishedOut(self), point.X, point.Y, enemyID);
                    bobber.ai[0] = 2f;
                    WorldGen.CheckAchievement_RealEstateAndTownSlimes();
                }
            }
            else if (Main.rand.NextBool(7) && !self.accFishingLine)
            {
                bobber.ai[0] = 2f;
            }
            else
            {
                bobber.ai[1] = bobber.localAI[1];
            }
            bobber.netUpdate = true;
        }
        private static void RevampVanillaSeaCreatureSystem(On_Projectile.orig_FishingCheck_RollEnemySpawns orig, Projectile self, ref FishingAttempt fisher)
        {
            Player plr = Main.player[self.owner];
            if (Main.rand.NextBool(Math.Min(FishUtils.FloatToIntegerPerc(plr.IchthyologyPlayer().scChance), 100), 100))
            {
                int id = SeaCreatureCatch.CatchCreature(plr, fisher);
                if (id < 0)
                {
                    self.GetGlobalProjectile<BobberGlobal>().spawningIntIsNegative = true;
                    id *= -1;
                }
                fisher.rolledEnemySpawn = id;
            }
        }
        private void RevampVanillaItemDropSystem(On_Projectile.orig_FishingCheck_RollItemDrop orig, Projectile self, ref FishingAttempt fisher)
        {
            if (fisher.rolledEnemySpawn != 0)
            {
                return;
            }
            fisher.rolledItemDrop = ItemCatch.CatchItem(Main.player[self.owner], fisher);
        }

        public override void Unload()
        {
            OnSeaCreatureKilled -= AddToBestiary;
        }
        //todo: change type to netID bc not separate rn
        static void AddToBestiary(NPC npc, Player player)
        {
            player.IchthyologyBestiary().AddToSCList(npc.type);
        }
        public override void ModifyFishingAttempt(ref FishingAttempt attempt)
        {
            if (attempt.playerFishingConditions.PoleItemType == ItemID.BloodFishingRod)
            {
                scChance += 30;
            }
        }
        public override void ModifyCaughtFish(Item fish)
        {
            Player.IchthyologyBestiary().AddToCatchList(fish.type);

            if (fish.maxStack > 1) //Here, depending on item's price, increases the stack.
            {
                int stackRaise = 1;
                double value = costCapForStackRaise - fish.value; //10 silver cap to possibly raise the stack.
                if (value > 0)
                {
                    for (int i = 0; i < FishUtils.Randomizer((int)Math.Round(Math.Log(value * Main.rand.Next(100)) * 100)); i++) //Main.rand here makes it have some random element to it, and won't be as high due to logarithm.
                    {
                        stackRaise++;
                    }
                    fish.stack *= stackRaise;
                }
            }
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (modifiers.DamageSource.TryGetCausingEntity(out Entity entity) && ((entity is Projectile proj && proj.IchthyologyProjectile(out IchthyologyGlobalProj global) && global.fromSC)
                || (entity is NPC npc && npc.IchthyologySeaCreature(out SeaCreature sc) && sc.isASeaCreature)))
            {
                modifiers.FinalDamage *= 1 - scDamageResist;
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.IchthyologySeaCreature(out SeaCreature creature) && creature.isASeaCreature)
            {
                modifiers.FinalDamage *= 1 + scBonusDamage;
            }
        }
        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (item.fishingPole > 0)
            {
                for (int i = 0; i < FishUtils.Randomizer(FishUtils.FloatToIntegerPerc(doubleHookChance)); i++)
                {
                    Vector2 bobberSpeed = velocity + new Vector2(Main.rand.NextFloat(-50f, 50f) * 0.05f, Main.rand.NextFloat(-50f, 50f) * 0.05f);
                    Projectile.NewProjectileDirect(source, position, bobberSpeed, item.shoot, 0, 0f, Player.whoAmI);
                }
            }
            return base.Shoot(item, source, position, velocity, type, damage, knockback);
        }
        public override bool? CanConsumeBait(Item bait)
        {
            if (baitReserveChance > Main.rand.NextFloat())
            {
                return false;
            }
            return null;
        }
    }
    /// <summary>
    /// Used to pass through data that current spawning NPC is an negative ID'd NPC.
    /// </summary>
    public class BobberGlobal : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public bool spawningIntIsNegative = false;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.bobber;
        }
    }
}
