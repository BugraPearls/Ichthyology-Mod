using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ichthyology.IDSets
{
    [ReinitializeDuringResizeArrays]
    public static class CatchItemIDSets
    {
        public static bool[] GoldCritters = ItemID.Sets.Factory.CreateNamedSet("AllGoldenCritters").Description("Set of all Golden Critters").RegisterBoolSet(false,
            ItemID.GoldBird,
            ItemID.GoldBunny,
            ItemID.GoldButterfly,
            ItemID.GoldDragonfly,
            ItemID.GoldFrog,
            ItemID.GoldGoldfish,
            ItemID.GoldGrasshopper,
            ItemID.GoldLadyBug,
            ItemID.GoldMouse,
            ItemID.GoldSeahorse,
            ItemID.SquirrelGold,
            ItemID.GoldWaterStrider,
            ItemID.GoldWorm
            );

        public static bool[] SpaceCatches = ItemID.Sets.Factory.CreateNamedSet("SpaceCatches").Description("Set of all the possible item catches in Space").RegisterBoolSet(false,
            ItemID.FallenStar,
            ItemID.Feather,
            ItemID.RainCloud);

        public static bool[] ForestCatches = ItemID.Sets.Factory.CreateNamedSet("ForestCatches").Description("Set of all the possible item catches in the Forest").RegisterBoolSet(false,
            ItemID.Goldfish,
            ItemID.Gel,
            ItemID.Shackle,
            ItemID.Lens,
            ItemID.Apple,
            ItemID.Peach,
            ItemID.Apricot,
            ItemID.Grapefruit,
            ItemID.Lemon,
            ItemID.BlueBerries,
            ItemID.AdhesiveBandage);

        public static bool[] UndergroundCatches = ItemID.Sets.Factory.CreateNamedSet("UndergroundCatches").Description("Set of all the possible item catches in the Underground").RegisterBoolSet(false,
            ItemID.Gel,
            ItemID.FairyCritterBlue,
            ItemID.FairyCritterGreen,
            ItemID.FairyCritterPink,
            ItemID.GemTreeAmberSeed,
            ItemID.GemTreeDiamondSeed,
            ItemID.GemTreeAmethystSeed,
            ItemID.GemTreeEmeraldSeed,
            ItemID.GemTreeRubySeed,
            ItemID.GemTreeSapphireSeed,
            ItemID.GemTreeTopazSeed,
            ItemID.EnchantedSword,
            ItemID.Terragrim,
            ItemID.Arkhalis,
            ItemID.ArmorPolish,
            ItemID.PocketMirror);

        public static bool[] CavernCatches = ItemID.Sets.Factory.CreateNamedSet("CavernCatches").Description("Set of all the possible item catches in the Caverns").RegisterBoolSet(false,
            ItemID.FairyCritterBlue,
            ItemID.FairyCritterGreen,
            ItemID.FairyCritterPink,
            ItemID.EnchantedSword,
            ItemID.Terragrim,
            ItemID.Arkhalis,
            ItemID.Rally,
            ItemID.StoneBlock,
            ItemID.LavaCharm);

        public static bool[] SnowCatches = ItemID.Sets.Factory.CreateNamedSet("SnowCatches").Description("Set of all the possible item catches in the Snow Biome").RegisterBoolSet(false,
            ItemID.FrozenKey,
            ItemID.Amarok,
            ItemID.EskimoCoat,
            ItemID.EskimoHood,
            ItemID.EskimoPants,
            ItemID.IceBlock);

        public static bool[] IceCatches = ItemID.Sets.Factory.CreateNamedSet("IceCatches").Description("Set of all the possible item catches in the Underground Ice Biome").RegisterBoolSet(false,
            ItemID.FrozenKey,
            ItemID.Amarok,
            ItemID.FlinxFur,
            ItemID.IceBlock);

        public static bool[] DesertCatches = ItemID.Sets.Factory.CreateNamedSet("DesertCatches").Description("Set of all the possible item catches in the Desert Biome").RegisterBoolSet(false,
            ItemID.SandBlock,
            ItemID.DungeonDesertKey,
            ItemID.AncientBattleArmorMaterial,
            ItemID.TrifoldMap);

        public static bool[] UndergroundDesertCatches = ItemID.Sets.Factory.CreateNamedSet("UndergroundDesertCatches").Description("Set of all the possible item catches in the Underground Desert Biome").RegisterBoolSet(false,
            ItemID.DungeonDesertKey,
            ItemID.HardenedSand,
            ItemID.AntlionMandible,
            ItemID.AncientCloth,
            ItemID.FastClock);

        public static bool[] CorruptionCatches = ItemID.Sets.Factory.CreateNamedSet("CorruptionCatches").Description("Set of all the possible item catches in the Corruption Biome").RegisterBoolSet(false,
            ItemID.AncientShadowGreaves,
            ItemID.AncientShadowHelmet,
            ItemID.AncientShadowScalemail,
            ItemID.CorruptionKey,
            ItemID.WormTooth,
            ItemID.VileMushroom,
            ItemID.RottenChunk,
            ItemID.UnholyWater,
            ItemID.Blindfold);

        public static bool[] UndergroundCorruptionCatches = ItemID.Sets.Factory.CreateNamedSet("UndergroundCorruptionCatches").Description("Set of all the possible item catches in the Underground Corruption Biome").RegisterBoolSet(false,
            ItemID.CursedFlame,
            ItemID.CorruptionKey,
            ItemID.SoulofNight,
            ItemID.UnholyWater,
            ItemID.RottenChunk,
            ItemID.Vitamins);

        public static bool[] CrimsonCatches = ItemID.Sets.Factory.CreateNamedSet("CrimsonCatches").Description("Set of all the possible item catches in the Crimson Biome").RegisterBoolSet(false,
            ItemID.Vertebrae,
            ItemID.ViciousMushroom,
            ItemID.CrimsonKey,
            ItemID.BloodWater,
            ItemID.Blindfold);

        public static bool[] UndergroundCrimsonCatches = ItemID.Sets.Factory.CreateNamedSet("UndergroundCrimsonCatches").Description("Set of all the possible item catches in the Underground Crimson Biome").RegisterBoolSet(false,
            ItemID.Vertebrae,
            ItemID.Ichor,
            ItemID.CrimsonKey,
            ItemID.SoulofNight,
            ItemID.BloodWater,
            ItemID.Vitamins);

        public static bool[] JungleCatches = ItemID.Sets.Factory.CreateNamedSet("JungleCatches").Description("Set of all the possible item catches in the Jungle Biome").RegisterBoolSet(false,
            ItemID.Uzi,
            ItemID.TurtleShell,
            ItemID.JungleKey,
            ItemID.Yelets,
            ItemID.Mango,
            ItemID.Pineapple,
            ItemID.Bezoar);

        public static bool[] UndergroundJungleCatces = ItemID.Sets.Factory.CreateNamedSet("UndergroundJungleCatches").Description("Set of all the possible item catches in the Underground Jungle Biome").RegisterBoolSet(false,
            ItemID.JungleKey,
            ItemID.LifeFruit,
            ItemID.Vine,
            ItemID.Stinger,
            ItemID.TurtleShell,
            ItemID.JungleSpores,
            ItemID.NaturesGift,
            ItemID.JungleRose);

        public static bool[] DungeonCatches = ItemID.Sets.Factory.CreateNamedSet("DungeonCatches").Description("Set of all the possible item catches in the Dungeon Biome").RegisterBoolSet(false,
            ItemID.GoldenKey,
            ItemID.Bone,
            ItemID.Ectoplasm,
            ItemID.BoneFeather,
            ItemID.Keybrand,
            ItemID.WispinaBottle,
            ItemID.MagnetSphere,
            ItemID.Kraken,
            ItemID.MaceWhip,
            ItemID.Nazar);

        public static bool[] OceanCatches = ItemID.Sets.Factory.CreateNamedSet("OceanCatches").Description("Set of all the possible item catches in the Ocean Biome").RegisterBoolSet(false,
            ItemID.Seahorse,
            ItemID.SharkFin,
            ItemID.Glowstick,
            ItemID.Coral,
            ItemID.Seashell,
            ItemID.Starfish,
            ItemID.TulipShell,
            ItemID.LightningWhelkShell,
            ItemID.JunoniaShell,
            ItemID.DivingHelmet,
            ItemID.Coconut,
            ItemID.Banana);

        public static bool[] HallowCatches = ItemID.Sets.Factory.CreateNamedSet("HallowCatches").Description("Set of all the possible item catches in the Hallowed Biome").RegisterBoolSet(false,
            ItemID.PixieDust,
            ItemID.UnicornHorn,
            ItemID.FairyCritterBlue,
            ItemID.FairyCritterGreen,
            ItemID.FairyCritterPink,
            ItemID.BlessedApple,
            ItemID.HallowedKey,
            ItemID.Megaphone);
    }
}
