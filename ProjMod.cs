using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ModLoader;

namespace nservermod
{
    public class ProjMod : GlobalProjectile
    {
        public override bool PreAI(Projectile projectile)
        {
            if (projectile.type == Terraria.ID.ProjectileID.Tombstone)
            {
                projectile.Kill();
                return false;
            }
            return base.PreAI(projectile);
        }
    }
}
