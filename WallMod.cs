using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nservermod
{
    public class WallMod : GlobalWall
    {
        public override bool CanExplode(int i, int j, int type)
        {
            return nservermod.IsInSingleplayer || nservermod.LocalPlayerHasPermissionToBuild();
        }

        public override void KillWall(int i, int j, int type, ref bool fail)
        {
            fail = nservermod.IsInSingleplayer || nservermod.LocalPlayerHasPermissionToBuild() || j < Main.worldSurface - 20;
        }
    }
}
