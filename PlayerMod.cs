using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ModLoader;

namespace nservermod
{
    public class PlayerMod : ModPlayer
    {
        public override void PlayerConnect(Player player)
        {
            //NetMod.SendBuildingAllowed(-1, player.whoAmI);
        }

        public override void PreUpdateBuffs()
        {
            //if (!nservermod.SetAllowDestruction)
            //    player.noBuilding = true;
        }

        public override void OnEnterWorld(Player player)
        {

        }
    }
}
