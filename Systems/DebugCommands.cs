using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ichthyology.Systems
{
    public class DebugCommands : ModCommand
    {
        public override CommandType Type => CommandType.Chat;
        public override string Command => "sc";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            foreach (int item in caller.Player.GetModPlayer<IchthyologyBestiary>().KilledSeaCreatures)
            {
                Main.NewText(ContentSamples.NpcPersistentIdsByNetIds[item]);
            }
        }
    }
}
