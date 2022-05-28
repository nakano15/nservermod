
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nservermod
{
    public class ItemMod : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            if (nservermod.IsInSingleplayer)
                return true;
            if(!nservermod.PlayerHasPermissionToBuildAndDestroy(player.whoAmI) && item.createWall > 0)
                return player.position.Y >= Main.worldSurface * 16;
            if(item.createTile == TileID.Containers || item.createTile == TileID.Containers2)
            {
                return false;
            }
            if (item.type == ItemID.WaterBucket || item.type == ItemID.LavaBucket || item.type == ItemID.HoneyBucket || item.type == ItemID.BottomlessBucket || item.type == ItemID.EmptyBucket)
            {
                return player.position.Y >= Main.worldSurface * 16;
            }
            return true;
        }
    }
}
