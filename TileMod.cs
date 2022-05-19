using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nservermod
{
    public class TileMod : GlobalTile
    { //Add a way of respawning shadow orbs on the corruption
        public override bool CanPlace(int i, int j, int type)
        {
            return nservermod.IsInSingleplayer || type == 13 || (j >= Main.worldSurface - 20 && (type != TileID.Containers && type != TileID.Containers2) || ((type == TileID.Torches || type == TileID.Platforms || type == TileID.Rope || type == TileID.SilkRope || type == TileID.VineRope || type == TileID.WebRope || type == 29 || type == TileID.Campfire) && !Main.wallHouse[Main.tile[i, j].wall]));
        }

        public override bool CanExplode(int i, int j, int type)
        {
            return nservermod.IsInSingleplayer || (j >= Main.worldSurface - 20 && !TileID.Sets.HousingWalls[type]);
        }

        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        {
            if (nservermod.IsInSingleplayer)
                return true;
            switch (type)
            {
                default:
                    if (j >= Main.worldSurface - 20)
                    {
                        switch (type)
                        {
                            default: return true;
                            case TileID.BlueDungeonBrick:
                            case TileID.GreenDungeonBrick:
                            case TileID.PinkDungeonBrick:
                            case TileID.LihzahrdBrick:
                            case TileID.LihzahrdAltar:
                            case TileID.Traps:
                            case TileID.GeyserTrap:
                            case TileID.Containers:
                            case TileID.Containers2:
                                break;
                        }
                    }
                    blockDamaged = false;
                    return false;
                case TileID.Torches:
                case TileID.Platforms:
                case TileID.Rope:
                case TileID.SilkRope:
                case TileID.VineRope:
                case TileID.WebRope:
                case TileID.PiggyBank:
                    return !Main.wallHouse[Main.tile[i, j].wall];
                case TileID.Tombstones:
                case TileID.Campfire:
                case TileID.Heart:
                case TileID.Vines:
                case TileID.CrimsonVines:
                case TileID.HallowedVines:
                case TileID.JungleVines:
                case 3: //Tall Grasses
                case TileID.Trees:
                case TileID.MushroomTrees:
                case TileID.PalmTree:
                case TileID.PineTree:
                case TileID.Pots:
                case TileID.CorruptThorns:
                case TileID.CrimtaneThorns:
                case TileID.JungleThorns:
                case TileID.Cobweb:
                case 61: //Shortened Grass
                case 71: //Mushrooms
                case 73: //Tall Plants
                case TileID.Cactus:
                case 82: //Alchemy Plants
                case 83:
                case 84:
                case TileID.DyePlants:
                case 185: //Decorative stones
                case 186:
                case 187:
                    return true;
            }
        }

        public override void KillTile(int x, int y, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (nservermod.IsInSingleplayer)
                return;
                switch (type)
            {
                case 5:
                    if (!fail && Main.netMode != 1)
                    {
                        Tile tile = Framing.GetTileSafely(x, y + 1);
                        if (tile != null && tile.active() && Main.tileSolid[tile.type]) //Has ground
                        {
                            tile = Framing.GetTileSafely(x, y);
                            bool RootTile = true;
                            if (tile.frameX == 66 && tile.frameY <= 45)
                            {
                                RootTile = false;
                            }
                            if (tile.frameX == 88 && tile.frameY >= 66 && tile.frameY <= 110)
                            {
                                RootTile = false;
                            }
                            if (tile.frameX == 22 && tile.frameY >= 132 && tile.frameY <= 176)
                            {
                                RootTile = false;
                            }
                            if (tile.frameX == 44 && tile.frameY >= 132 && tile.frameY <= 176)
                            {
                                RootTile = false;
                            }
                            if (tile.frameX == 44 && tile.frameY >= 198)
                            {
                                RootTile = false;
                            }
                            if (tile.frameX == 66 && tile.frameY >= 198)
                            {
                                RootTile = false;
                            }
                            if (RootTile)
                                WorldMod.AddTreePlantingPosition(x, y);
                        }
                    }
                    break;
            }
        }
    }
}
