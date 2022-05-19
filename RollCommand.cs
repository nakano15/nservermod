using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ModLoader;

namespace nservermod
{
    public class RollCommand : ModCommand
    {
        public override string Command => "roll";

        public override string Description => "Rolls a dice between 0 and 100.";

        public override string Usage => "Just input the command.";

        public override CommandType Type => CommandType.Chat;

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (Main.netMode == 0) Main.NewText(caller.Player.name + " rolled a " + Main.rand.Next(101) + " our of 100.", 255, 128, 0);
            else NetMod.SendRollCommand(caller.Player.whoAmI);
        }
    }
}
