using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using Ichthyology.Systems;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Ichthyology.IDSets;
using Terraria.ModLoader;
using Ichthyology.Items.Fish;

namespace Ichthyology.Systems
{
    public class ItemCatch
    {
        /// <summary>
        /// Method to add Items to "Rarity Lists" when trying to fishing
        /// </summary>
        /// <param name="player"></param>
        /// <param name="attempt"></param>
        public static int CatchItem(Player player, FishingAttempt attempt)
        {
            bool hardMode = Main.hardMode;
            bool plantDead = NPC.downedPlantBoss;
            bool cavernLayer = player.ZoneRockLayerHeight;
            bool undergroundLayer = player.ZoneDirtLayerHeight;
            int id = attempt.rolledItemDrop;

            List<int> CommonItems = new List<int>();
            List<int> UncommonItems = new List<int>();
            List<int> RareItems = new List<int>();
            List<int> VeryRareItems = new List<int>();
            List<int> LegendaryItems = new List<int>();

            if (attempt.uncommon && player.IchthyologyPlayer().questFishCatchChance > Main.rand.NextFloat())
            {
                foreach (var item in ContentSamples.ItemsByType)
                {
                    if (item.Value.questItem && attempt.questFish == item.Key)
                    {
                        return item.Key;
                    }
                }
            }
            
            if (attempt.crate)
            {
                if (attempt.rare && FishUtils.DungeonBiomeVanillaRules(player))
                {
                    id = (hardMode ? 3984 : 3205);
                }
                else if (attempt.rare && (player.ZoneBeach || (Main.remixWorld && attempt.heightLevel == 1 && (double)attempt.Y >= Main.rockLayer && Main.rand.NextBool(2))))
                {
                    id = (hardMode ? 5003 : 5002);
                }
                else if (attempt.rare && FishUtils.CorruptBiomeVanillaRules(player, attempt.heightLevel))
                {
                    id = (hardMode ? 3982 : 3203);
                }
                else if (attempt.rare && FishUtils.CrimsonBiomeVanillaRules(player, attempt.heightLevel))
                {
                    id = (hardMode ? 3983 : 3204);
                }
                else if (attempt.rare && player.ZoneHallow)
                {
                    id = (hardMode ? 3986 : 3207);
                }
                else if (attempt.rare && FishUtils.JungleBiomeVanillaRules(player))
                {
                    id = (hardMode ? 3987 : 3208);
                }
                else if (attempt.rare && player.ZoneSnow)
                {
                    id = (hardMode ? 4406 : 4405);
                }
                else if (attempt.rare && FishUtils.DesertBiomeVanillaRules(player))
                {
                    id = (hardMode ? 4408 : 4407);
                }
                else if (attempt.rare && attempt.heightLevel == 0)
                {
                    id = (hardMode ? 3985 : 3206);
                }
                else if (attempt.veryrare || attempt.legendary)
                {
                    id = (hardMode ? 3981 : 2336);
                }
                else if (attempt.uncommon)
                {
                    id = (hardMode ? 3980 : 2335);
                }
                else
                {
                    id = (hardMode ? 3979 : 2334);
                }
                return id;
            }
            //Lava Items
            if (attempt.inLava)
            {
                CommonItems.Add(ItemID.Obsidian);
                VeryRareItems.Add(ItemID.LavaCharm);
                //Lava Fishing in Hell
                if (player.ZoneUnderworldHeight)
                {
                    CommonItems.Add(ItemID.AshBlock);
                    UncommonItems.Add(ItemID.LivingFireBlock);
                    FishUtils.AddMultipleToList(RareItems,
                        ItemID.MagmaStone,
                        ItemID.ObsidianRose);
                }
            }
            else if (attempt.inHoney)
            {
                CommonItems.Add(ItemID.HoneyBlock);
                RareItems.Add(ItemID.BottledHoney);
            }
            //All Water Items
            else
            {
                if (player.IchthyologyPlayer().GoldenCage)
                {
                    FishUtils.AddMultipleToList(VeryRareItems,
                        CatchItemIDSets.GoldCritters);
                }
                //Bloodmoon items
                if (Main.bloodMoon)
                {
                    if (!NPC.combatBookWasUsed)
                    {
                        LegendaryItems.Add(ItemID.CombatBook);
                        LegendaryItems.Add(ItemID.DreadoftheRedSea);
                    }
                }

                if (player.IchthyologyPlayer().MagnetBait)
                {
                    FishUtils.AddMultipleToList(UncommonItems, 
                        CatchItemIDSets.OreCatches);
                }

                //Space Items
                if (player.ZoneSkyHeight)
                {
                    FishUtils.AddMultipleToList(CommonItems,
                        ItemID.Feather,
                        ItemID.RainCloud);
                    UncommonItems.Add(ItemID.Damselfish);
                    RareItems.Add(ItemID.FallenStar);
                }

                //Forest Items
                else if (player.ZoneForest)
                {
                    FishUtils.AddMultipleToList(CommonItems,
                        ItemID.Gel);
                    FishUtils.AddMultipleToList(UncommonItems,
                        ItemID.Goldfish);
                    FishUtils.AddMultipleToList(RareItems,
                        ItemID.Apple,
                        ItemID.Peach,
                        ItemID.Apricot,
                        ItemID.Grapefruit,
                        ItemID.Lemon,
                        ItemID.BlueBerries,
                        ItemID.Shackle,
                        ItemID.Lens);
                    if (hardMode)
                    {
                        VeryRareItems.Add(ItemID.AdhesiveBandage);
                    }
                }

                //Underground Items
                else if (player.ZoneNormalUnderground)
                {
                    FishUtils.AddMultipleToList(CommonItems,
                        ItemID.Gel);
                    FishUtils.AddMultipleToList(UncommonItems,
                        ItemID.GemTreeAmberSeed,
                        ItemID.GemTreeDiamondSeed,
                        ItemID.GemTreeAmethystSeed,
                        ItemID.GemTreeEmeraldSeed,
                        ItemID.GemTreeRubySeed,
                        ItemID.GemTreeSapphireSeed,
                        ItemID.GemTreeTopazSeed);
                    RareItems.Add(ModContent.ItemType<Obelfish>());
                    FishUtils.AddMultipleToList(RareItems,
                        ItemID.FairyCritterBlue,
                        ItemID.FairyCritterGreen,
                        ItemID.FairyCritterPink);
                    FishUtils.AddMultipleToList(LegendaryItems,
                        ItemID.EnchantedSword,
                        ItemID.Terragrim,
                        ItemID.Arkhalis);
                    if (hardMode)
                    {
                        FishUtils.AddMultipleToList(VeryRareItems,
                            ItemID.ArmorPolish,
                            ItemID.PocketMirror);
                    }
                }

                //Caverns Items
                else if (player.ZoneNormalCaverns)
                {
                    CommonItems.Add(ItemID.StoneBlock);
                    FishUtils.AddMultipleToList(RareItems,
                        ItemID.FairyCritterBlue,
                        ItemID.FairyCritterGreen,
                        ItemID.FairyCritterPink);
                    VeryRareItems.Add(ItemID.Rally);
                    FishUtils.AddMultipleToList(LegendaryItems,
                        ItemID.EnchantedSword,
                        ItemID.Terragrim,
                        ItemID.Arkhalis);
                }

                //Snow Biome Items
                else if (FishUtils.SnowBiomeVanillaRules(player))
                {
                    if (!cavernLayer)
                    {
                        FishUtils.AddMultipleToList(CommonItems,
                            ItemID.IceBlock,
                            ItemID.AtlanticCod);
                        FishUtils.AddMultipleToList(UncommonItems,
                            ItemID.FrostDaggerfish,
                            ItemID.FrostMinnow);
                        FishUtils.AddMultipleToList(RareItems,
                            ItemID.EskimoCoat,
                            ItemID.EskimoHood,
                            ItemID.EskimoPants);
                        if (hardMode)
                        {
                            VeryRareItems.Add(ItemID.Amarok);
                        }
                        if (plantDead)
                        {
                            LegendaryItems.Add(ItemID.FrozenKey);
                        }
                    }
                    //Underground Ice Biome Items
                    else if (cavernLayer)
                    {
                        CommonItems.Add(ItemID.IceBlock);
                        RareItems.Add(ItemID.FlinxFur);
                        if (hardMode)
                        {
                            VeryRareItems.Add(ItemID.Amarok);
                            if (player.ZoneCrimson || player.ZoneCorrupt)
                            {
                                LegendaryItems.Add(ItemID.ScalyTruffle);
                            }
                        }
                        if (plantDead)
                        {
                            LegendaryItems.Add(ItemID.FrozenKey);
                        }
                    }
                }

                //Desert Items
                else if (FishUtils.DesertBiomeVanillaRules(player))
                {
                    FishUtils.AddMultipleToList(CommonItems,
                        ItemID.SandBlock,
                        ItemID.RockLobster,
                        ItemID.Flounder);
                    UncommonItems.Add(ItemID.Oyster);
                    if (hardMode)
                    {
                        FishUtils.AddMultipleToList(VeryRareItems,
                            ItemID.AncientBattleArmorMaterial,
                            ItemID.TrifoldMap);
                    }
                    if (plantDead)
                    {
                        LegendaryItems.Add(ItemID.DungeonDesertKey);
                    }
                }

                //Underground Desert Items
                else if (player.ZoneUndergroundDesert)
                {
                    CommonItems.Add(ItemID.HardenedSand);
                    if (hardMode)
                    {
                        RareItems.Add(ItemID.AncientCloth);
                        VeryRareItems.Add(ItemID.FastClock);
                    }
                    if (plantDead)
                    {
                        LegendaryItems.Add(ItemID.DungeonDesertKey);
                    }
                }

                //Corruption
                else if (FishUtils.CorruptBiomeVanillaRules(player, attempt.heightLevel))
                {
                    if (plantDead)
                    {
                        LegendaryItems.Add(ItemID.CorruptionKey);
                    }
                    if (!(undergroundLayer || cavernLayer))
                    {
                        FishUtils.AddMultipleToList(CommonItems,
                            ItemID.WormTooth,
                            ItemID.VileMushroom,
                            ItemID.RottenChunk);
                        UncommonItems.Add(ItemID.Ebonkoi);
                        RareItems.Add(ItemID.UnholyWater);
                        RareItems.Add(ModContent.ItemType<WitheredFish>());
                        RareItems.Add(ItemID.PurpleClubberfish);
                        FishUtils.AddMultipleToList(LegendaryItems,
                            ItemID.AncientShadowGreaves,
                            ItemID.AncientShadowHelmet,
                            ItemID.AncientShadowScalemail);
                        if (hardMode)
                        {
                            VeryRareItems.Add(ItemID.Blindfold);
                        }
                    }
                    //Underground Corruption
                    else if (undergroundLayer || cavernLayer)
                    {
                        CommonItems.Add(ItemID.RottenChunk);
                        RareItems.Add(ItemID.UnholyWater);
                        if (hardMode)
                        {
                            UncommonItems.Add(ItemID.SoulofNight);
                            RareItems.Add(ItemID.CursedFlame);
                            VeryRareItems.Add(ItemID.Vitamins);
                        }
                    }
                }

                //Crimson Items
                else if (FishUtils.CrimsonBiomeVanillaRules(player, attempt.heightLevel))
                {
                    LegendaryItems.Add(ItemID.Bladetongue);
                    UncommonItems.Add(ItemID.Hemopiranha);
                    CommonItems.Add(ItemID.CrimsonTigerfish);
                    if (plantDead)
                    {
                        LegendaryItems.Add(ItemID.CrimsonKey);
                    }
                    if (!(undergroundLayer || cavernLayer))
                    {
                        FishUtils.AddMultipleToList(CommonItems,
                            ItemID.Vertebrae,
                            ItemID.ViciousMushroom);
                        RareItems.Add(ModContent.ItemType<WitheredFish>());
                        RareItems.Add(ItemID.BloodWater);
                        if (hardMode)
                        {
                            VeryRareItems.Add(ItemID.Blindfold);

                        }
                    }
                    //Underground Crimson Items
                    else if (undergroundLayer || cavernLayer)
                    {
                        CommonItems.Add(ItemID.Vertebrae);
                        RareItems.Add(ItemID.BloodWater);
                        if (hardMode)
                        {
                            UncommonItems.Add(ItemID.SoulofNight);
                            RareItems.Add(ItemID.Ichor);
                            VeryRareItems.Add(ItemID.Vitamins);
                        }
                    }
                }

                //Jungle Items
                else if (FishUtils.JungleBiomeVanillaRules(player))
                {
                    if (NPC.downedMechBossAny)
                    {
                        VeryRareItems.Add(ItemID.Yelets);
                    }
                    if (plantDead)
                    {
                        LegendaryItems.Add(ItemID.JungleKey);
                    }
                    CommonItems.Add(ItemID.NeonTetra);
                    if (!(undergroundLayer || cavernLayer))
                    {
                        FishUtils.AddMultipleToList(CommonItems,
                            ItemID.Mango,
                            ItemID.Pineapple);
                        UncommonItems.Add(ItemID.DoubleCod);
                        VeryRareItems.Add(ItemID.Bezoar);
                        if (hardMode)
                        {
                            RareItems.Add(ItemID.TurtleShell);
                            LegendaryItems.Add(ItemID.Uzi);
                        }
                    }
                    //Underground Jungle
                    else if (undergroundLayer || cavernLayer)
                    {
                        FishUtils.AddMultipleToList(CommonItems,
                            ItemID.Vine,
                            ItemID.Stinger);
                        FishUtils.AddMultipleToList(UncommonItems,
                            ItemID.VariegatedLardfish,
                            ItemID.JungleSpores);
                        FishUtils.AddMultipleToList(RareItems,
                            ItemID.JungleRose,
                            ItemID.NaturesGift);
                        if (hardMode)
                        {
                            UncommonItems.Add(ItemID.TurtleShell);
                            RareItems.Add(ItemID.LifeFruit);
                        }
                    }
                }

                //Dungeon Items
                else if (FishUtils.DungeonBiomeVanillaRules(player))
                {
                    CommonItems.Add(ItemID.Bone);
                    UncommonItems.Add(ItemID.GoldenKey);
                    if (hardMode)
                    {
                        UncommonItems.Add(ItemID.Ectoplasm);
                        FishUtils.AddMultipleToList(VeryRareItems,
                            ItemID.BoneFeather,
                            ItemID.Keybrand,
                            ItemID.WispinaBottle,
                            ItemID.MagnetSphere,
                            ItemID.Kraken,
                            ItemID.MaceWhip,
                            ItemID.Nazar,
                            ItemID.AlchemyTable);
                    }
                }
                
                //Ocean Items
                else if ((Main.remixWorld && attempt.heightLevel == 1 && (double)attempt.Y >= Main.rockLayer && Main.rand.NextBool(3)) || (attempt.heightLevel <= 1 && (attempt.X < 380 || attempt.X > Main.maxTilesX - 380) && attempt.waterTilesCount > 1000))
                {
                    if (Main.rand.NextBool(12) && attempt.rare)
                    {
                        return ModContent.ItemType<Puffer>();
                    }
                    FishUtils.AddMultipleToList(CommonItems,
                        ItemID.OldShoe,
                        ItemID.JojaCola,
                        ItemID.Seahorse,
                        ItemID.SharkFin,
                        ItemID.Glowstick,
                        ItemID.Coral,
                        ItemID.Seashell,
                        ItemID.Starfish,
                        ItemID.RedSnapper,
                        ItemID.Tuna,
                        ItemID.Trout);
                    FishUtils.AddMultipleToList(UncommonItems,
                        ItemID.BombFish,
                        ItemID.Shrimp,
                        ItemID.Coconut,
                        ItemID.Banana);
                    FishUtils.AddMultipleToList(RareItems,
                        ItemID.TulipShell,
                        ItemID.LightningWhelkShell,
                        ItemID.DivingHelmet,
                        ItemID.PinkJellyfish,
                        ItemID.Swordfish);
                    FishUtils.AddMultipleToList(VeryRareItems,
                        ItemID.JunoniaShell,
                        ItemID.ReaverShark,
                        ItemID.SawtoothShark);
                    FishUtils.AddMultipleToList(LegendaryItems,
                        ItemID.FrogLeg,
                        ItemID.BalloonPufferfish,
                        ItemID.ZephyrFish);
                }

                //Hallow Items
                else if (player.ZoneHallow)
                {
                    if (hardMode)
                    {
                        CommonItems.Add(ItemID.PixieDust);
                        FishUtils.AddMultipleToList(UncommonItems,
                            ItemID.UnicornHorn,
                            ItemID.PrincessFish);
                        FishUtils.AddMultipleToList(RareItems,
                            ItemID.FairyCritterBlue,
                            ItemID.FairyCritterGreen,
                            ItemID.FairyCritterPink,
                            ItemID.Prismite);
                        FishUtils.AddMultipleToList(VeryRareItems,
                            ItemID.BlessedApple,
                            ItemID.Megaphone);
                        FishUtils.AddMultipleToList(LegendaryItems,
                            ItemID.CrystalSerpent,
                            ItemID.LadyOfTheLake);
                        if (cavernLayer)
                        {
                            VeryRareItems.Add(ItemID.ChaosFish);
                        }
                        if (plantDead)
                        {
                            LegendaryItems.Add(ItemID.HallowedKey);
                        }
                        if (FishUtils.SnowBiomeVanillaRules(player))
                        {
                            LegendaryItems.Add(ItemID.ScalyTruffle);
                        }
                    }
                }

                //Mushroom Biome catches
                else if (player.ZoneGlowshroom)
                {
                    FishUtils.AddMultipleToList(CommonItems,
                        ItemID.Mushroom,
                        ItemID.MushroomGrassSeeds);
                    LegendaryItems.Add(ItemID.ToiletMushroom);
                }

                //Generic fish spawns in multiple layers or zones.
                else if (cavernLayer || undergroundLayer || player.ZoneUnderworldHeight)
                {
                    RareItems.Add(ItemID.ArmoredCavefish);
                    if (player.ZoneForest || player.ZoneJungle || player.ZoneSnow)
                    {
                        CommonItems.Add(ItemID.SpecularFish);
                    }
                }
                else if (!player.ZoneUnderworldHeight)
                {
                    RareItems.Add(ItemID.Stinkfish);
                    VeryRareItems.Add(ItemID.Rockfish);
                    LegendaryItems.Add(ItemID.GoldenCarp);
                    if (hardMode)
                    {
                        RareItems.Add(ItemID.GreenJellyfish);
                    }
                    else
                    {
                        RareItems.Add(ItemID.BlueJellyfish);
                    }
                }
                else if (!(cavernLayer || undergroundLayer || player.ZoneUnderworldHeight) && attempt.waterTilesCount > 1000)
                {
                    CommonItems.Add(ItemID.Salmon);
                }
                else
                {
                    CommonItems.Add(ItemID.Bass);
                }
            }

            int catchRarity = -1;
            if(attempt.common && CommonItems.Count > 0)
            {
                catchRarity = Main.rand.NextFromCollection(CommonItems);
            }
            if(attempt.uncommon && UncommonItems.Count > 0)
            {
                catchRarity = Main.rand.NextFromCollection(UncommonItems);
            }
            if (attempt.rare && RareItems.Count > 0)
            {
                catchRarity = Main.rand.NextFromCollection(RareItems);
            }
            if (attempt.veryrare && VeryRareItems.Count > 0)
            {
                catchRarity = Main.rand.NextFromCollection(VeryRareItems);
            }
            if (attempt.legendary && LegendaryItems.Count > 0)
            {
                catchRarity = Main.rand.NextFromCollection(LegendaryItems);
            }

            return catchRarity;
                
        }
    }
}

