using Ichthyology.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Initializers;
using Terraria.ModLoader;
using Terraria.UI;
using static Ichthyology.UI.IchthyologyBestiaryUI;

namespace Ichthyology.UI
{

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
        public static bool IsActive = false;
        public int TotalAnglerQuest => Main.LocalPlayer.anglerQuestsFinished;
        public int TotalUniqueSCKills => Main.LocalPlayer.IchthyologyBestiary().KilledSeaCreatures.Count;
        public int TotalUniqueFishingCatches => Main.LocalPlayer.IchthyologyBestiary().CaughtFishingDrops.Count;
        public override void OnInitialize()
        {
            ClickPreventedPanel panel = new();
            panel.Width.Set(550, 0);
            panel.Height.Set(240, 0);
            panel.HAlign = 0.5f;
            panel.VAlign = 0.5f;
            Append(panel);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                base.Draw(spriteBatch);
            }
        }
    }
    public class IchthyologyBestiaryShowButton : UIState
    {
        public override void OnInitialize()
        {
            UIImageButton panel = new(ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonPlay"));
            panel.Width.Set(550, 0);
            panel.Height.Set(240, 0);
            panel.HAlign = 0.5f;
            panel.VAlign = 0.5f;
            panel.OnLeftClick += Panel_OnLeftClick;
            Append(panel);
        }

        private static void Panel_OnLeftClick(UIMouseEvent evt, UIElement listeningElement)
        {
            IsActive = !IsActive;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Main.playerInventory)
            {
                base.Draw(spriteBatch);
            }
        }
    }
    [Autoload(Side = ModSide.Client)]
    public class IchthyologyBestiaryDisplaySystem : ModSystem
    {
        internal IchthyologyBestiaryUI UIPanel;
        internal IchthyologyBestiaryShowButton ButtonToShowUI;
        private UserInterface _UIPanel;
        private UserInterface _ButtonToShowUI;
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
        }
        public override void UpdateUI(GameTime gameTime)
        {
            _UIPanel?.Update(gameTime);
            _ButtonToShowUI?.Update(gameTime);
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
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
