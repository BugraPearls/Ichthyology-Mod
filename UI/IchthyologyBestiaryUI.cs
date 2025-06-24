using Ichthyology.IDSets;
using Ichthyology.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Initializers;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;
using static Ichthyology.UI.IchthyologyBestiaryUI;

namespace Ichthyology.UI
{
    public enum CurrentUIState
    {
        TurnedOff,
        MainMenu,
        SCBestiary,
        CatchBestiary,
    }
    public enum CurrentStatPanel
    {
        SC,
        Catch,
        Angler,
    }
    public class ClickPreventedPanel : UIPanel
    {
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }
    }
    public class IchthyologyBestiaryUI : UIState
    {

        public static CurrentUIState currentState = CurrentUIState.TurnedOff;
        public static int TotalAnglerQuest => Main.LocalPlayer.anglerQuestsFinished;
        public static int TotalUniqueSCKills => Main.LocalPlayer.IchthyologyBestiary().KilledSeaCreatures.Count;
        public static int TotalUniqueFishingCatches => Main.LocalPlayer.IchthyologyBestiary().CaughtFishingDrops.Count;

        public static bool SummaryPanelOpen = false;
        public static CurrentStatPanel currentStatUIState = CurrentStatPanel.SC;

        public UIText SeaCreatureText;
        public UIText FishCathcesText;
        public UIText AnglerQuestText;
        public UIText SeaCreatureMilestoneText;
        public UIText FishCatchesMilestoneText;
        public UIText AnglerQuestMilestoneText;
        public UIButton<string> SeaCreatureButton;
        public UIButton<string> FishCatchesButton;
        public override void OnInitialize()
        {
            ClickPreventedPanel panel = new();
            panel.Width.Set(550, 0);
            panel.Height.Set(300, 0);
            panel.HAlign = 0.5f;
            panel.VAlign = 0.5f;
            Append(panel);

            SeaCreatureText = new("", 1.3f)
            {
                HAlign = 0.1f,
                VAlign = 0.1f
            };
            panel.Append(SeaCreatureText);

            SeaCreatureMilestoneText = new("Hover over to view bonuses gained by unique kills.", 0.9f);
            SeaCreatureMilestoneText.HAlign = 0.1f;
            SeaCreatureMilestoneText.VAlign = 0.25f;
            SeaCreatureMilestoneText.OnMouseOver += HoverSCM;
            SeaCreatureMilestoneText.OnMouseOut += OutM;

            panel.Append(SeaCreatureMilestoneText);

            FishCathcesText = new("", 1.3f)
            {
                HAlign = 0.1f,
                VAlign = 0.40f
            };
            panel.Append(FishCathcesText);

            FishCatchesMilestoneText = new("Hover over to view bonuses gained by unique catches.", 0.9f);
            FishCatchesMilestoneText.HAlign = 0.1f;
            FishCatchesMilestoneText.VAlign = 0.55f;
            FishCatchesMilestoneText.OnMouseOver += HoverFCM;
            FishCatchesMilestoneText.OnMouseOut += OutM;

            panel.Append(FishCatchesMilestoneText);

            AnglerQuestText = new("", 1.3f)
            {
                HAlign = 0.1f,
                VAlign = 0.70f
            };
            panel.Append(AnglerQuestText);

            AnglerQuestMilestoneText = new("Hover over to view bonuses gained by completed quests.", 0.9f);
            AnglerQuestMilestoneText.HAlign = 0.1f;
            AnglerQuestMilestoneText.VAlign = 0.85f;
            AnglerQuestMilestoneText.OnMouseOver += HoverAGM;
            AnglerQuestMilestoneText.OnMouseOut += OutM;

            panel.Append(AnglerQuestMilestoneText);

            SeaCreatureButton = new("Show Kills");
            SeaCreatureButton.Width.Set(90, 0);
            SeaCreatureButton.Height.Set(40, 0);
            SeaCreatureButton.HAlign = 0.9f;
            SeaCreatureButton.VAlign = 0.15f;
            SeaCreatureButton.OnLeftClick += SeaCreatureButtonClick;
            panel.Append(SeaCreatureButton);

            FishCatchesButton = new("Show Catches");
            FishCatchesButton.Width.Set(90, 0);
            FishCatchesButton.Height.Set(40, 0);
            FishCatchesButton.HAlign = 0.9f;
            FishCatchesButton.VAlign = 0.45f;
            FishCatchesButton.OnLeftClick += FishCatchesButtonClick;
            panel.Append(FishCatchesButton);
        }

        private void SeaCreatureButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.NewText("hi");
        }

        private static void FishCatchesButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            currentState = CurrentUIState.CatchBestiary;
        }

        private static void OutM(UIMouseEvent evt, UIElement listeningElement)
        {
            SummaryPanelOpen = false;
        }

        private static void HoverSCM(UIMouseEvent evt, UIElement listeningElement)
        {
            if (currentState == CurrentUIState.MainMenu)
            {
                SummaryPanelOpen = true;
                currentStatUIState = CurrentStatPanel.SC;
            }
        }
        private static void HoverFCM(UIMouseEvent evt, UIElement listeningElement)
        {
            if (currentState == CurrentUIState.MainMenu)
            {
                SummaryPanelOpen = true;
                currentStatUIState = CurrentStatPanel.Catch;
            }
        }
        private static void HoverAGM(UIMouseEvent evt, UIElement listeningElement)
        {
            if (currentState == CurrentUIState.MainMenu)
            {
                SummaryPanelOpen = true;
                currentStatUIState = CurrentStatPanel.Angler;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (currentState == CurrentUIState.MainMenu)
            {
                SeaCreatureText.SetText("Total unique Sea Creature Kills: " + TotalUniqueSCKills);
                FishCathcesText.SetText("Total unique Fishing Catches: " + TotalUniqueFishingCatches);
                AnglerQuestText.SetText("Total completed Angler quests: " + TotalAnglerQuest);
                base.Draw(spriteBatch);
            }
        }
    }
    public class IchthyologyStatSummary : UIState
    {
        public UIPanel StatPanel;
        public UIText StatSumText;
        public UIText StatSumText2;
        public UIText StatSumText3;
        public override void OnInitialize()
        {
            StatPanel = new();
            Append(StatPanel);

            StatSumText = new("", 0.9f);
            StatPanel.Append(StatSumText);
            StatSumText2 = new("", 0.9f);
            StatPanel.Append(StatSumText2);
            StatSumText3 = new("", 0.9f);
            StatPanel.Append(StatSumText3);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (SummaryPanelOpen)
            {
                StatPanel.Top.Set(Main.MouseScreen.Y, 0);
                StatPanel.Left.Set(Main.MouseScreen.X, 0);
                StatPanel.Width.Set(300, 0);
                StatPanel.Height.Set(250, 0);

                StatSumText.HAlign = 0.1f;
                StatSumText.VAlign = 0.1f;
                string setText = currentStatUIState switch
                {
                    CurrentStatPanel.SC => $"{(float)Math.Round(TotalUniqueSCKills * 0.3f, 2)}% more Enemy Catching chance.\n",
                    CurrentStatPanel.Catch => $"{(float)Math.Round(TotalUniqueFishingCatches * 0.25f, 2)} more Fishing Power.\n",
                    CurrentStatPanel.Angler => $"{(float)Math.Round(TotalAnglerQuest * 0.2f, 2)}% more Double Hook chance.\n",
                    _ => "Error",
                };
                StatSumText.SetText(setText);

                StatSumText2.HAlign = 0.1f;
                StatSumText2.VAlign = 0.3f;
                string setText2;
                switch (currentStatUIState)
                {
                    case CurrentStatPanel.SC:
                        setText2 = $"{TotalUniqueSCKills} out of 50 unique kills\nIncreases dmg and dmg resist\ntowards Fished up enemies by 10%.";
                        if (TotalUniqueSCKills < 50)
                        {
                            StatSumText2.TextColor = Color.DarkGray;
                        }
                        else
                        {
                            StatSumText2.TextColor = Main.mouseColor;
                        }
                        break;
                    case CurrentStatPanel.Catch:
                        setText2 = $"{TotalUniqueFishingCatches} out of 50 unique catches\nGrants you a 20% chance to\nnot consume your bait.";
                        if (TotalUniqueFishingCatches < 50)
                        {
                            StatSumText2.TextColor = Color.DarkGray;
                        }
                        else
                        {
                            StatSumText2.TextColor = Main.mouseColor;
                        }
                        break;
                    case CurrentStatPanel.Angler:
                        setText2 = $"{TotalAnglerQuest} out of 50 completed quests\nIncreases chance to catch\nQuest Fishes by 25%.";
                        if (TotalAnglerQuest < 50)
                        {
                            StatSumText2.TextColor = Color.DarkGray;
                        }
                        else
                        {
                            StatSumText2.TextColor = Main.mouseColor;
                        }
                        break;
                    default:
                        setText2 = "Error";
                        break;
                }
                ;
                StatSumText2.SetText(setText2);

                StatSumText3.HAlign = 0.1f;
                StatSumText3.VAlign = 0.65f;
                string setText3;
                switch (currentStatUIState)
                {
                    case CurrentStatPanel.SC:
                        setText3 = $"{TotalUniqueSCKills} out of 100 unique kills\nIncreases loot of\nfished up enemies by 50%.";
                        if (TotalUniqueSCKills < 100)
                        {
                            StatSumText3.TextColor = Color.DarkGray;
                        }
                        else
                        {
                            StatSumText3.TextColor = Main.mouseColor;
                        }
                        break;
                    case CurrentStatPanel.Catch:
                        setText3 = $"{TotalUniqueFishingCatches} out of 100 unique catches\nIncreases fishing power by 10 and\nDouble hook chance by 15%";
                        if (TotalUniqueFishingCatches < 100)
                        {
                            StatSumText3.TextColor = Color.DarkGray;
                        }
                        else
                        {
                            StatSumText3.TextColor = Main.mouseColor;
                        }
                        break;
                    case CurrentStatPanel.Angler:
                        setText3 = $"{TotalAnglerQuest} out of 100 completed quests\nIncreases how many fishes caught\nfor stackable, lower valued items.";
                        if (TotalAnglerQuest < 100)
                        {
                            StatSumText3.TextColor = Color.DarkGray;
                        }
                        else
                        {
                            StatSumText3.TextColor = Main.mouseColor;
                        }
                        break;
                    default:
                        setText3 = "Error";
                        break;
                }
                ;
                StatSumText3.SetText(setText3);
                base.Draw(spriteBatch);
            }
        }
    }
    public class IchthyologyBestiaryShowButton : UIState
    {
        public UIImageButton button;
        public override void OnInitialize()
        {
            button = new(ModContent.Request<Texture2D>("Ichthyology/UI/IchthyologyBestiaryUI"));
            button.Width.Set(30, 0);
            button.Height.Set(30, 0);
            button.HAlign = 0.05f;
            button.VAlign = 0.28f;
            button.OnLeftClick += Panel_OnLeftClick;
            Append(button);
        }

        private static void Panel_OnLeftClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (currentState is CurrentUIState.TurnedOff)
            {
                currentState = CurrentUIState.MainMenu;
            }
            else
            {
                currentState = CurrentUIState.TurnedOff;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Main.playerInventory)
            {
                if (Main.GameMode == GameModeID.Creative)
                {
                    button.HAlign = 0.05f;
                }
                else
                {
                    button.HAlign = 0.02f;
                }
                base.Draw(spriteBatch);
            }
        }
    }

    public class IchthyologyItemUniques : UIState
    {
        public UIList AllCaughtList;
        public UIList SpaceCatches;
        public UIList ForestCatches;
        public UIList UndergroundCatches; //Cavern combined
        public UIList SnowCatches; //Ice combined
        public UIList DesertCatches; //Underground desert combined
        public UIList CorruptionCatches; //Underground Corruption combined
        public UIList CrimsonCatches; //Underground Crimson combined
        public UIList JungleCatches; //Underground Jungle and Honey combined
        public UIList DungeonCatches;
        public UIList OceanCatches;
        public UIList HallowCatches;
        public UIList LavaCatches;
        public UIList OreCatches;
        public UIList GoldCritters;
        public UIList AllPossibleCatches;
        public override void OnInitialize()
        {
            ClickPreventedPanel panel = new();
            panel.Width.Set(950, 0);
            panel.Height.Set(420, 0);
            panel.HAlign = 0.5f;
            panel.VAlign = 0.5f;
            Append(panel);

            void AddAList(float Halign, UIList UIlist)
            {
                UIlist.Width.Set(50, 0);
                UIlist.Height.Set(320, 0);
                UIlist.HAlign = Halign;
                UIlist.VAlign = 0.8f;
                UIlist.SetScrollbar(new UIScrollbar());
                panel.Append(UIlist);
            }

            AddAList(0.02f, AllCaughtList = new());
            AddAList(0.085f, SpaceCatches = new());
            AddAList(0.15f, ForestCatches = new());
            AddAList(0.215f, UndergroundCatches = new());
            AddAList(0.28f, SnowCatches = new());
            AddAList(0.345f, DesertCatches = new());
            AddAList(0.41f, CorruptionCatches = new());
            AddAList(0.475f, CrimsonCatches = new());
            AddAList(0.54f, JungleCatches = new());
            AddAList(0.605f, DungeonCatches = new());
            AddAList(0.67f, OceanCatches = new());
            AddAList(0.735f, HallowCatches = new());
            AddAList(0.80f, LavaCatches = new());
            AddAList(0.865f, OreCatches = new());
            AddAList(0.92f, GoldCritters = new());
            AddAList(0.985f, AllPossibleCatches = new());

            void AddAIcon(float Halign, string Name)
            {
                UIImage icon = new(ModContent.Request<Texture2D>("Ichthyology/UI/Icons/Bestiary_"+Name));
                icon.Width.Set(30, 0);
                icon.Height.Set(30, 0);
                icon.HAlign = Halign;
                icon.VAlign = 0.02f;

                panel.Append(icon);
            }

            AddAIcon(0.02f, "Item_Spawn");
            AddAIcon(0.085f, "Sky");
            AddAIcon(0.15f, "Surface");
            AddAIcon(0.215f, "Caverns");
            AddAIcon(0.28f, "Snow");
            AddAIcon(0.345f, "Desert");
            AddAIcon(0.41f, "The_Corruption");
            AddAIcon(0.475f, "The_Crimson");
            AddAIcon(0.54f, "The_Jungle");
            AddAIcon(0.605f, "The_Dungeon");
            AddAIcon(0.67f, "Ocean");
            AddAIcon(0.735f, "The_Hallow");
            AddAIcon(0.80f, "The_Underworld");
            AddAIcon(0.865f, "Ore");
            AddAIcon(0.92f, "Golden");
            AddAIcon(0.985f, "Reach_Hardmode");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            List<int> currentDrops = Main.LocalPlayer.IchthyologyBestiary().CaughtFishingDrops;
            if (currentState == CurrentUIState.CatchBestiary)
            {
                void ShowTheList(UIList uiList, int[] itemIDs)
                {
                    uiList.Clear();
                    foreach (var item in itemIDs)
                    {
                        uiList.Add(new UIItemIcon(ContentSamples.ItemsByType[item], !currentDrops.Contains(item)));
                    }
                }

                AllCaughtList.Clear();
                foreach (var item in currentDrops)
                {
                    AllCaughtList.Add(new UIItemIcon(ContentSamples.ItemsByType[item], false));
                }

                ShowTheList(SpaceCatches, CatchItemIDSets.SpaceCatches);
                ShowTheList(ForestCatches, CatchItemIDSets.ForestCatches);
                ShowTheList(UndergroundCatches, [.. CatchItemIDSets.UndergroundCatches, .. CatchItemIDSets.CavernCatches]);
                ShowTheList(SnowCatches, [.. CatchItemIDSets.SnowCatches, .. CatchItemIDSets.IceCatches]);
                ShowTheList(DesertCatches, [.. CatchItemIDSets.DesertCatches, .. CatchItemIDSets.UndergroundDesertCatches]);
                ShowTheList(CorruptionCatches, [.. CatchItemIDSets.CorruptionCatches, .. CatchItemIDSets.UndergroundCorruptionCatches]);
                ShowTheList(CrimsonCatches, [.. CatchItemIDSets.CrimsonCatches, .. CatchItemIDSets.UndergroundCrimsonCatches]);
                ShowTheList(JungleCatches, [.. CatchItemIDSets.JungleCatches, .. CatchItemIDSets.UndergroundJungleCatces, .. CatchItemIDSets.HoneyCatches]);
                ShowTheList(DungeonCatches, CatchItemIDSets.DungeonCatches);
                ShowTheList(OceanCatches, CatchItemIDSets.OceanCatches);
                ShowTheList(HallowCatches, CatchItemIDSets.HallowCatches);
                ShowTheList(LavaCatches, CatchItemIDSets.LavaCatches);
                ShowTheList(OreCatches, CatchItemIDSets.OreCatches);
                ShowTheList(GoldCritters, CatchItemIDSets.GoldCritters);

                AllPossibleCatches.Clear();
                foreach (var item in CatchItemIDSets.GetAllPossibleCatchesWithQuestFish())
                {
                    AllPossibleCatches.Add(new UIItemIcon(ContentSamples.ItemsByType[item], !currentDrops.Contains(item)));
                }

                base.Draw(spriteBatch);
            }
        }
    }

    [Autoload(Side = ModSide.Client)]
    public class IchthyologyBestiaryDisplaySystem : ModSystem
    {
        internal IchthyologyBestiaryUI UIPanel;
        internal IchthyologyBestiaryShowButton ButtonToShowUI;
        internal IchthyologyStatSummary UIStatPanel;
        internal IchthyologyItemUniques UIItems;
        private UserInterface _UIPanel;
        private UserInterface _ButtonToShowUI;
        private UserInterface _UIStatPanel;
        private UserInterface _UIItems;
        public override void Load()
        {
            UIPanel = new IchthyologyBestiaryUI();
            UIPanel.Activate();
            _UIPanel = new UserInterface();
            _UIPanel.SetState(UIPanel);

            ButtonToShowUI = new IchthyologyBestiaryShowButton();
            ButtonToShowUI.Activate();
            _ButtonToShowUI = new UserInterface();
            _ButtonToShowUI.SetState(ButtonToShowUI);

            UIStatPanel = new IchthyologyStatSummary();
            UIStatPanel.Activate();
            _UIStatPanel = new UserInterface();
            _UIStatPanel.SetState(UIStatPanel);

            UIItems = new IchthyologyItemUniques();
            UIItems.Activate();
            _UIItems = new UserInterface();
            _UIItems.SetState(UIItems);
        }
        public override void UpdateUI(GameTime gameTime)
        {
            if (currentState == CurrentUIState.MainMenu)
            {
                _UIPanel?.Update(gameTime);
                _UIStatPanel?.Update(gameTime);
            }
            if (Main.playerInventory)
            { 
                _ButtonToShowUI?.Update(gameTime);
            }
            if (currentState == CurrentUIState.CatchBestiary)
            {
                _UIItems?.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Ichthyology: Fish Bestiary Elements",
                    delegate
                    {
                        _UIPanel.Draw(Main.spriteBatch, new GameTime());
                        _ButtonToShowUI.Draw(Main.spriteBatch, new GameTime());
                        _UIStatPanel.Draw(Main.spriteBatch, new GameTime());
                        _UIItems.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
