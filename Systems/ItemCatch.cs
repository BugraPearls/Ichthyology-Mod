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
            List<int> CommonItems = new List<int>();
            List<int> UncommonItems = new List<int>();
            List<int> RareItems = new List<int>();
            List<int> VeryRareItems = new List<int>();
            List<int> LegendaryItems = new List<int>();
            //Space Items
            if (player.ZoneSkyHeight)
            {
                FishUtils.AddMultipleToList(CommonItems,
                    ItemID.Feather,
                    ItemID.RainCloud);
                RareItems.Add(ItemID.FallenStar);
            }

            //Forest Items
            else if (player.ZoneForest)
            {
                FishUtils.AddMultipleToList(CommonItems,
                    ItemID.Apple,
                    ItemID.Peach,
                    ItemID.Apricot,
                    ItemID.Grapefruit,
                    ItemID.Lemon,
                    ItemID.BlueBerries,
                    ItemID.Gel);
                FishUtils.AddMultipleToList(UncommonItems,
                    ItemID.Shackle,
                    ItemID.Lens,
                    ItemID.Goldfish);
                if(Main.hardMode)
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
                FishUtils.AddMultipleToList(RareItems,
                    ItemID.FairyCritterBlue,
                    ItemID.FairyCritterGreen,
                    ItemID.FairyCritterPink);
                FishUtils.AddMultipleToList(LegendaryItems,
                    ItemID.EnchantedSword,
                    ItemID.Terragrim,
                    ItemID.Arkhalis);
                if (Main.hardMode)
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
            else if (player.ZoneSnow)
            {
                if (!player.ZoneRockLayerHeight)
                {
                    CommonItems.Add(ItemID.IceBlock);
                    FishUtils.AddMultipleToList(RareItems,
                        ItemID.EskimoCoat,
                        ItemID.EskimoHood,
                        ItemID.EskimoPants);
                    if (Main.hardMode)
                    {
                        VeryRareItems.Add(ItemID.Amarok);
                    }
                    if (NPC.downedPlantBoss)
                    {
                        LegendaryItems.Add(ItemID.FrozenKey);
                    }
                }
                //Underground Ice Biome Items
                else if (player.ZoneRockLayerHeight)
                {
                    CommonItems.Add(ItemID.IceBlock);
                    RareItems.Add(ItemID.FlinxFur);
                    if (Main.hardMode)
                    {
                        VeryRareItems.Add(ItemID.Amarok);
                    }
                    if (NPC.downedPlantBoss)
                    {
                        LegendaryItems.Add(ItemID.FrozenKey);
                    }
                }
            }

            //Desert Items
            else if (player.ZoneDesert)
            {
                CommonItems.Add(ItemID.SandBlock);
                if (Main.hardMode)
                {
                    FishUtils.AddMultipleToList(VeryRareItems,
                        ItemID.AncientBattleArmorMaterial,
                        ItemID.TrifoldMap);
                }
                if (NPC.downedPlantBoss)
                {
                    LegendaryItems.Add(ItemID.DungeonDesertKey);
                }
            }

            //Underground Desert Items
            else if (player.ZoneUndergroundDesert)
            {
                CommonItems.Add(ItemID.HardenedSand);
                if (Main.hardMode)
                {
                    RareItems.Add(ItemID.AncientCloth);
                    VeryRareItems.Add(ItemID.FastClock);
                }
                if (NPC.downedPlantBoss)
                {
                    LegendaryItems.Add(ItemID.DungeonDesertKey);
                }
            }

            //Corruption
            else if (player.ZoneCorrupt)
            {
                if (NPC.downedPlantBoss)
                {
                    LegendaryItems.Add(ItemID.CorruptionKey);
                }
                if (!(player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight))
                {
                    FishUtils.AddMultipleToList(CommonItems,
                        ItemID.WormTooth,
                        ItemID.VileMushroom,
                        ItemID.RottenChunk);
                    RareItems.Add(ItemID.UnholyWater);
                    FishUtils.AddMultipleToList(LegendaryItems,
                        ItemID.AncientShadowGreaves,
                        ItemID.AncientShadowHelmet,
                        ItemID.AncientShadowScalemail);
                    if (Main.hardMode)
                    {
                        VeryRareItems.Add(ItemID.Blindfold);
                    }
                }
                //Underground Corruption
                else if (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight)
                {
                    CommonItems.Add(ItemID.RottenChunk);
                    RareItems.Add(ItemID.UnholyWater);
                    if (Main.hardMode)
                    {
                        UncommonItems.Add(ItemID.SoulofNight);
                        RareItems.Add(ItemID.CursedFlame);
                        VeryRareItems.Add(ItemID.Vitamins);
                    }
                }
            }

            //Crimson Items
            else if (player.ZoneCrimson)
            {
                if (NPC.downedPlantBoss)
                {
                    LegendaryItems.Add(ItemID.CrimsonKey);
                }
                if (!(player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight))
                {
                    FishUtils.AddMultipleToList(CommonItems,
                        ItemID.Vertebrae,
                        ItemID.ViciousMushroom);
                    RareItems.Add(ItemID.BloodWater);
                    if (Main.hardMode)
                    {
                        VeryRareItems.Add(ItemID.Blindfold);
                    }
                }
                //Underground Crimson Items
                else if (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight)
                {
                    CommonItems.Add(ItemID.Vertebrae);
                    RareItems.Add(ItemID.BloodWater);
                    if (Main.hardMode)
                    {
                        UncommonItems.Add(ItemID.SoulofNight);
                        RareItems.Add(ItemID.Ichor);
                        VeryRareItems.Add(ItemID.Vitamins);
                    }
                }
            }

            //Jungle Items
            else if (player.ZoneJungle)
            {
                if (NPC.downedMechBossAny)
                {
                    VeryRareItems.Add(ItemID.Yelets);
                }
                if (NPC.downedPlantBoss)
                {
                    LegendaryItems.Add(ItemID.JungleKey);
                }
                if (!(player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight))
                {
                    FishUtils.AddMultipleToList(CommonItems,
                        ItemID.Mango,
                        ItemID.Pineapple);
                    VeryRareItems.Add(ItemID.Bezoar);
                    if (Main.hardMode)
                    {
                        RareItems.Add(ItemID.TurtleShell);
                        LegendaryItems.Add(ItemID.Uzi);
                    }
                }
                //Underground Jungle
                else if (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight)
                {
                    FishUtils.AddMultipleToList(CommonItems,
                        ItemID.Vine,
                        ItemID.Stinger);
                    UncommonItems.Add(ItemID.JungleSpores);
                    FishUtils.AddMultipleToList(RareItems,
                        ItemID.JungleRose,
                        ItemID.NaturesGift);
                    if (Main.hardMode)
                    {
                        UncommonItems.Add(ItemID.TurtleShell);
                        RareItems.Add(ItemID.LifeFruit);
                    }
                }
            }

            //Dungeon Items
            else if (player.ZoneDungeon)
            {
                CommonItems.Add(ItemID.Bone);
                UncommonItems.Add(ItemID.GoldenKey);
                if (Main.hardMode)
                {
                    UncommonItems.Add(ItemID.Ectoplasm);
                    FishUtils.AddMultipleToList(VeryRareItems,
                        ItemID.BoneFeather,
                        ItemID.Keybrand,
                        ItemID.WispinaBottle,
                        ItemID.MagnetSphere,
                        ItemID.Kraken,
                        ItemID.MaceWhip,
                        ItemID.Nazar);
                }
            }

            //Ocean Items
            else if (player.ZoneBeach)
            {
                FishUtils.AddMultipleToList(CommonItems,
                    ItemID.Seahorse,
                    ItemID.SharkFin,
                    ItemID.Glowstick,
                    ItemID.Coral,
                    ItemID.Seashell,
                    ItemID.Starfish,
                    ItemID.Coconut,
                    ItemID.Banana);
                FishUtils.AddMultipleToList(RareItems,
                    ItemID.TulipShell,
                    ItemID.LightningWhelkShell,
                    ItemID.DivingHelmet);
                VeryRareItems.Add(ItemID.JunoniaShell);
            }

            //Hallow Items
            else if (player.ZoneHallow)
            {
                CommonItems.Add(ItemID.PixieDust);
                UncommonItems.Add(ItemID.UnicornHorn);
                FishUtils.AddMultipleToList(RareItems,
                    ItemID.FairyCritterBlue,
                    ItemID.FairyCritterGreen,
                    ItemID.FairyCritterPink);
                FishUtils.AddMultipleToList(VeryRareItems,
                    ItemID.BlessedApple,
                    ItemID.Megaphone);
                if(NPC.downedPlantBoss)
                {
                    LegendaryItems.Add(ItemID.HallowedKey);
                }
            }

            //Lava Items
            else if (attempt.inLava)
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

