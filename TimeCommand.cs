using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria.ModLoader;

namespace nservermod
{
    class TimeCommand : ModCommand
    {
        public override string Command => "servertime";

        public override string Description => "Shows the server time.";

        public override string Usage => "Just input the command.";

        public override CommandType Type => CommandType.Chat;

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            NetMod.SendAskServerTime(caller.Player.whoAmI);
            //caller.Reply(nservermod.CurrentTime.ToShortTimeString());
        }
    }
}
