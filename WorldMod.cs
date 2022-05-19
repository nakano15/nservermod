using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ModLoader;

namespace nservermod
{
    public class WorldMod : ModWorld
    {
        private static List<Point> TreePlantingPosition = new List<Point>();

        public static void OnUnload()
        {
            if(TreePlantingPosition != null) TreePlantingPosition.Clear();
            TreePlantingPosition = null;
        }

        public override void Initialize()
        {
            nservermod.IsInSingleplayer = Main.netMode == 0;
        }

        internal static void AddTreePlantingPosition(int X, int Y)
        {
            TreePlantingPosition.Add(new Point(X, Y));
        }

        public override void PreUpdate()
        {
            if (Main.netMode < 2)
                return;
            if(nservermod.GetHourValue < 255)
            {
                RefillChests();
            }
            if (nservermod.GetMinuteValue < 255)
            {
                if (nservermod.GetMinuteValue % 5 == 0)
                {
                    PlaceNewPots();
                }
                if (nservermod.GetMinuteValue % 15 == 0)
                {
                    TryPlacingShadowOrb();
                }
                if (nservermod.GetMinuteValue == 0 || nservermod.GetMinuteValue == 30)
                {
                    TryPlacingEnchantedSword();
                }
            }
            if (Main.rand.Next(100) == 0)
                PlaceLifeCrystal();
            if (NPC.downedBoss3)
            {
                if (nservermod.GetMinuteValue == 55 && nservermod.GetHourValue < 255 && nservermod.GetHourValue % 2 == 1)
                {
                    nservermod.SendMessage("The dungeon will be resetted in 5 minutes. Everyone leave it.", 255, 128, 128);
                }
                else if (nservermod.GetMinuteValue == 0 && nservermod.GetHourValue % 2 == 0)
                {
                    NPC.downedBoss3 = false;
                    if (NPC.AnyNPCs(Terraria.ID.NPCID.Clothier))
                    {
                        Main.npc[NPC.FindFirstNPC(Terraria.ID.NPCID.Clothier)].Transform(Terraria.ID.NPCID.OldMan);
                        nservermod.SendMessage("The dungeon has been resetted. The clothier was cursed again.", 255, 0, 0);
                    }
                    else
                    {
                        nservermod.SendMessage("The dungeon has been resetted. Skeletron resurged.", 255, 128, 128);
                    }
                    for(int i = 0; i < 255; i++)
                    {
                        if(Main.player[i].active && Main.player[i].ZoneDungeon)
                        {
                            nservermod.SendMessage("Skeletron: Some fools are eager to join the dead. So be it.", 255, 0, 0);
                            break;
                        }
                    }
                }
            }
            while (TreePlantingPosition.Count > 0)
            {
                int x = TreePlantingPosition[0].X, y = TreePlantingPosition[0].Y;
                Tile tile = Main.tile[x, y + 1];
                int Type = 20, Style = 0;
                if (tile.active())
                {
                    TileLoader.SaplingGrowthType(tile.type, ref Type, ref Style);
                }
                TreePlantingPosition.RemoveAt(0);
                TileObject to;
                if (TileObject.CanPlace(x, y, Type, Style, 1, out to) && TileObject.Place(to) && Main.netMode > 0)
                {
                    NetMessage.SendTileRange(Main.myPlayer, x, y, 1, 1);
                }
            }
        }

        private void TryPlacingShadowOrb()
        {
            int x = Main.rand.Next(40, Main.maxTilesX - 40), y = Main.rand.Next((int)Main.worldSurface, Main.maxTilesY - 130);
            Tile tile = Main.tile[x, y];
            if(tile.wall == Terraria.ID.WallID.EbonstoneUnsafe || tile.wall == Terraria.ID.WallID.CrimstoneUnsafe)
            {
                Vector2 Center = new Vector2(x, y) * 16;
                for(int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active && (Center - Main.player[i].Center).Length() < 400)
                        return;
                }
                WorldGen.AddShadowOrb(x, y);
            }
        }

        private void TryPlacingEnchantedSword()
        {
            int x = Main.rand.Next(40, Main.maxTilesX - 40), y = Main.rand.Next(5, Main.maxTilesY - 5);
            WorldGen.PlaceTile(x, y, 187, style: 17);
        }

        private void PlaceLifeCrystal()
        {
            int x = Main.rand.Next(40, Main.maxTilesX - 40), y = Main.rand.Next((int)(WorldGen.worldSurfaceHigh + 20), Main.maxTilesY - 300);
            Tile tile = Main.tile[x, y];
            if (y < Main.worldSurface)
            {
                if(tile.wall == 0 || Main.wallHouse[tile.wall])
                {
                    return;
                }
            }
            else
            {
                if (Main.wallHouse[tile.wall])
                    return;
            }
            WorldGen.AddLifeCrystal(x, y);
        }

        private void PlaceNewPots()
        {
            for(int i = 0; i < 10; i++)
            {
                int x = Main.rand.Next(5, Main.maxTilesX - 5), yStart = Main.rand.Next((int)Main.worldSurface, Main.maxTilesY - 10);
                bool LastTileWasFree = false;
                for(byte Attempts = 0; Attempts < 30; Attempts++)
                {
                    int y = yStart + Attempts;
                    if (y >= Main.maxTilesY - 5)
                        break;
                    if (!LastTileWasFree)
                    {
                        if (Main.tile[x, y].active() && Main.tileSolid[Main.tile[x, y].type] && !Main.tile[x, y - 1].lava())
                            LastTileWasFree = true;
                    }
                    else
                    {
                        int style = Main.rand.Next(4);
                        int UpperTileType = y < Main.maxTilesY - 5 ? Main.tile[x, y + 1].type : 0;
                        switch (UpperTileType)
                        {
                            case 147: case 161: case 162:
                                style = Main.rand.Next(4, 7);
                                break;
                            case 60:
                                style = Main.rand.Next(7, 10);
                                break;
                            case 41: case 43:
                            case 44:
                            style = Main.rand.Next(10, 13);
                                break;
                            case 22: case 23: case 25:
                                style = Main.rand.Next(16, 19);
                                break;
                            case 199: case 203: case 204: case 200:
                                style = Main.rand.Next(22, 25);
                                break;
                            case 367:
                                style = Main.rand.Next(31, 34);
                                break;
                            case 226:
                                style = Main.rand.Next(28, 31);
                                break;
                        }
                        if (Main.wallDungeon[Main.tile[x, y].wall])
                            style = Main.rand.Next(10, 13);
                        if (y > Main.maxTilesY - 200)
                            style = Main.rand.Next(13, 16);
                        if(WorldGen.PlacePot(x, y, style: style))
                        {
                            LastTileWasFree = true;
                            break;
                        }
                    }
                }
            }
        }

        private void RefillChests()
        {
            List<byte> HellChestIDs = new List<byte>();
            {
                for(byte c = 0; c < 5; c++)
                {
                    if(HellChestIDs.Count > 0 && Main.rand.NextFloat() >= 0.5f)
                    {
                        HellChestIDs.Insert(0, c);
                    }
                    else
                    {
                        HellChestIDs.Add(c);
                    }
                }
            }
            byte CurrentHellChest = 0, CurrentWaterLoot = (byte)Main.rand.Next(3);
            for (int i = 0; i < Main.maxChests; i++)
            {
                if (Main.chest[i] == null || Main.chest[i].x == -1 || Main.chest[i].y == -1)
                    continue;
                {
                    int x = Main.chest[i].x, y = Main.chest[i].y;
                    int TotalItems = Main.chest[i].item.Length;
                    for (int j = 0; j < TotalItems; j++)
                    {
                        Main.chest[i].item[j].SetDefaults(0, true);
                    }
                    Item[] items = Main.chest[i].item;
                    Tile t = Framing.GetTileSafely(x, y);
                    bool SpawnIceMirror = false, SpawnMahoganyTreeStuff = false;
                    ushort Type = t.type;
                    int Style = t.frameX / 36;
                    int PrimaryLoot = 0;
                    switch (Style)
                    {
                        case 0:
                            break;
                        case 10: //Jungle chest
                            PrimaryLoot = WorldGen.GetNextJungleChestItem();
                            break;
                        case 17:
                            if (Main.rand.Next(15) == 0)
                                PrimaryLoot = 863;
                            else
                            {
                                switch (CurrentWaterLoot)
                                {
                                    case 0:
                                        PrimaryLoot = 187;
                                        break;
                                    case 1:
                                        PrimaryLoot = 186;
                                        break;
                                    case 2:
                                        PrimaryLoot = 277;
                                        break;
                                }
                                CurrentWaterLoot++;
                                if (CurrentWaterLoot > 2)
                                    CurrentWaterLoot = 0;
                            }
                            break;
                        case 18: //Jungle
                            PrimaryLoot = 1156;
                            break;
                        case 19: //Corruption
                            PrimaryLoot = 1571;
                            break;
                        case 20: //Crimson
                            PrimaryLoot = 1569;
                            break;
                        case 21: //Hallowed
                            PrimaryLoot = 1260;
                            break;
                        case 22: //Ice
                            PrimaryLoot = 1572;
                            break;
                    }
                    if (Style == 11 || PrimaryLoot == 0 && y >= Main.worldSurface + 25 && y <= Main.maxTilesY - 205 && (Type == 147 || Type == 161 || Type == 162)) //Ice Biome Loot
                    {
                        SpawnIceMirror = true;
                        Style = 11;
                        switch (Main.rand.Next(6))
                        {
                            case 0:
                                PrimaryLoot = 670;
                                break;
                            case 1:
                                PrimaryLoot = 724;
                                break;
                            case 2:
                                PrimaryLoot = 950;
                                break;
                            case 3:
                                PrimaryLoot = 1319;
                                break;
                            case 4:
                                PrimaryLoot = 987;
                                break;
                            default:
                                PrimaryLoot = 1579;
                                break;
                        }
                        if (Main.rand.Next(20) == 0) PrimaryLoot = 997;
                        if (Main.rand.Next(50) == 0) PrimaryLoot = 669;
                    }
                    if(y > Main.maxTilesY - 205 && PrimaryLoot == 0)
                    {
                        if (CurrentHellChest == HellChestIDs[0])
                        {
                            PrimaryLoot = 274;
                            Style = 4;
                        }
                        else if (CurrentHellChest == HellChestIDs[1])
                        {
                            PrimaryLoot = 220;
                            Style = 4;
                        }
                        else if (CurrentHellChest == HellChestIDs[2])
                        {
                            PrimaryLoot = 112;
                            Style = 4;
                        }
                        else if (CurrentHellChest == HellChestIDs[3])
                        {
                            PrimaryLoot = 218;
                            Style = 4;
                        }
                        else if (CurrentHellChest == HellChestIDs[4])
                        {
                            PrimaryLoot = 3019;
                            Style = 4;
                        }
                        CurrentHellChest++;
                        if (CurrentHellChest > 4)
                            CurrentHellChest = 0;
                    }
                    byte Pos = 0;
                    if((PrimaryLoot == 0 && y < Main.worldSurface + 25) || PrimaryLoot == 848)
                    {
                        if (PrimaryLoot > 0)
                        {
                            items[Pos].SetDefaults(PrimaryLoot, false);
                            items[Pos].Prefix(-1);
                            Pos++;
                            switch (PrimaryLoot)
                            {
                                case 848:
                                    items[Pos].SetDefaults(866, false);
                                    Pos++;
                                    break;
                                case 832:
                                    items[Pos].SetDefaults(933, false);
                                    Pos++;
                                    break;
                            }
                        }
                        else
                        {
                            switch (Main.rand.Next(11))
                            {
                                case 0:
                                    items[Pos].SetDefaults(280, true);
                                    items[Pos].Prefix(-1);
                                    Pos++;
                                    break;
                                case 1:
                                    items[Pos].SetDefaults(281, true);
                                    items[Pos].Prefix(-1);
                                    Pos++;
                                    break;
                                case 2:
                                    items[Pos].SetDefaults(284, true);
                                    items[Pos].Prefix(-1);
                                    Pos++;
                                    break;
                                case 3:
                                    items[Pos].SetDefaults(282, true);
                                    items[Pos].stack = Main.rand.Next(40, 76);
                                    Pos++;
                                    break;
                                case 4:
                                    items[Pos].SetDefaults(279, true);
                                    items[Pos].stack = Main.rand.Next(70, 151);
                                    Pos++;
                                    break;
                                case 5:
                                    items[Pos].SetDefaults(285, true);
                                    items[Pos].Prefix(-1);
                                    Pos++;
                                    break;
                                case 6:
                                    items[Pos].SetDefaults(953, true);
                                    items[Pos].Prefix(-1);
                                    Pos++;
                                    break;
                                case 7:
                                    items[Pos].SetDefaults(946, true);
                                    items[Pos].Prefix(-1);
                                    Pos++;
                                    break;
                                case 8:
                                    items[Pos].SetDefaults(3068, true);
                                    items[Pos].Prefix(-1);
                                    Pos++;
                                    break;
                                case 9:
                                    items[Pos].SetDefaults(3069, true);
                                    items[Pos].Prefix(-1);
                                    Pos++;
                                    break;
                                case 10:
                                    items[Pos].SetDefaults(3084, true);
                                    items[Pos].Prefix(-1);
                                    Pos++;
                                    break;
                            }
                        }
                        if (Main.rand.Next(6) == 0)
                        {
                            items[Pos].SetDefaults(3093, true);
                            items[Pos].stack = 1;
                            if (Main.rand.Next(5) == 0)
                                items[Pos].stack += Main.rand.Next(2);
                            if (Main.rand.Next(10) == 0)
                                items[Pos].stack += Main.rand.Next(3);
                            Pos++;
                        }
                        if (Main.rand.Next(3) == 0)
                        {
                            items[Pos].SetDefaults(168);
                            items[Pos].stack = Main.rand.Next(3, 6);
                            Pos++;
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                items[Pos].SetDefaults(WorldGen.copperBar, true);
                            }
                            else
                            {
                                items[Pos].SetDefaults(WorldGen.ironBar, true);
                            }
                            items[Pos].stack = Main.rand.Next(8) + 3;
                            Pos++;
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            items[Pos].SetDefaults(965, true);
                            items[Pos].stack = Main.rand.Next(50, 101);
                            Pos++;
                        }
                        if (Main.rand.Next(3) != 0)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                items[Pos].SetDefaults(40, true);
                            }
                            else
                            {
                                items[Pos].SetDefaults(42, true);
                            }
                            items[Pos].stack = Main.rand.Next(26) + 25;
                            Pos++;
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            items[Pos].SetDefaults(28, true);
                            items[Pos].stack = Main.rand.Next(3) + 3;
                            Pos++;
                        }
                        if (Main.rand.Next(3) != 0)
                        {
                            items[Pos].SetDefaults(2350, true);
                            items[Pos].stack = Main.rand.Next(2, 5);
                            Pos++;
                        }
                        if (Main.rand.Next(3) > 0)
                        {
                            switch (Main.rand.Next(6))
                            {
                                case 0:
                                    items[Pos].SetDefaults(292, false);
                                    break;
                                case 1:
                                    items[Pos].SetDefaults(298, false);
                                    break;
                                case 2:
                                    items[Pos].SetDefaults(299, false);
                                    break;
                                case 3:
                                    items[Pos].SetDefaults(290, false);
                                    break;
                                case 4:
                                    items[Pos].SetDefaults(2322, false);
                                    break;
                                case 5:
                                    items[Pos].SetDefaults(2325, false);
                                    break;
                            }
                            items[Pos].stack = Main.rand.Next(1, 3);
                            Pos++;
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                items[Pos].SetDefaults(8, true);
                            }
                            else
                            {
                                items[Pos].SetDefaults(31, true);
                            }
                            items[Pos].stack = Main.rand.Next(11) + 10;
                            Pos++;
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            items[Pos].SetDefaults(72, true);
                            items[Pos].stack = Main.rand.Next(10, 31);
                            Pos++;
                        }
                    }
                    else if(y < Main.rockLayer)
                    {
                        if(PrimaryLoot > 0)
                        {
                            if(PrimaryLoot == 832)
                            {
                                items[Pos].SetDefaults(933, true);
                                Pos++;
                            }
                            items[Pos].SetDefaults(PrimaryLoot, true);
                            items[Pos].Prefix(-1);
                            Pos++;
                        }
                        else
                        {
                            if(Main.rand.Next(20) == 0)
                            {
                                items[Pos].SetDefaults(997, true);
                                items[Pos].Prefix(-1);
                            }
                            else
                            {
                                switch (Main.rand.Next(7))
                                {
                                    case 0:
                                        items[Pos].SetDefaults(49, true);
                                        items[Pos].Prefix(-1);
                                        break;
                                    case 1:
                                        items[Pos].SetDefaults(50, true);
                                        items[Pos].Prefix(-1);
                                        break;
                                    case 2:
                                        items[Pos].SetDefaults(53, true);
                                        items[Pos].Prefix(-1);
                                        break;
                                    case 3:
                                        items[Pos].SetDefaults(54, true);
                                        items[Pos].Prefix(-1);
                                        break;
                                    case 4:
                                        items[Pos].SetDefaults(55, true);
                                        items[Pos].Prefix(-1);
                                        break;
                                    case 5:
                                        items[Pos].SetDefaults(975, true);
                                        items[Pos].Prefix(-1);
                                        break;
                                    case 6:
                                        items[Pos].SetDefaults(930, true);
                                        items[Pos].Prefix(-1);
                                        Pos++;
                                        items[Pos].SetDefaults(931, true);
                                        items[Pos].stack = Main.rand.Next(26) + 25;
                                        break;
                                }
                            }
                            Pos++;
                        }
                        if(Main.rand.Next(3) == 0)
                        {
                            items[Pos].SetDefaults(166, true);
                            items[Pos].stack = Main.rand.Next(10, 21);
                            Pos++;
                        }
                        if(Main.rand.Next(5) == 0)
                        {
                            items[Pos].SetDefaults(52, true);
                            Pos++;
                        }
                        if(Main.rand.Next(3) == 0)
                        {
                            items[Pos].SetDefaults(965, true);
                            items[Pos].stack = Main.rand.Next(50, 101);
                            Pos++;
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                items[Pos].SetDefaults(WorldGen.ironBar, true);
                            }
                            else
                            {
                                items[Pos].SetDefaults(WorldGen.silverBar, true);
                            }
                            items[Pos].stack = Main.rand.Next(11) + 5;
                            Pos++;
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                items[Pos].SetDefaults(40, true);
                            }
                            else
                            {
                                items[Pos].SetDefaults(42, true);
                            }
                            items[Pos].stack = Main.rand.Next(26) + 25;
                            Pos++;
                        }
                        if(Main.rand.Next(2) == 0)
                        {
                            items[Pos].SetDefaults(28, true);
                            items[Pos].stack = Main.rand.Next(3) + 3;
                            Pos++;
                        }
                        if(Main.rand.Next(3) > 0)
                        {
                            switch (Main.rand.Next(9))
                            {
                                case 0:
                                    items[Pos].SetDefaults(289, true);
                                    break;
                                case 1:
                                    items[Pos].SetDefaults(298, true);
                                    break;
                                case 2:
                                    items[Pos].SetDefaults(299, true);
                                    break;
                                case 3:
                                    items[Pos].SetDefaults(290, true);
                                    break;
                                case 4:
                                    items[Pos].SetDefaults(303, true);
                                    break;
                                case 5:
                                    items[Pos].SetDefaults(291, true);
                                    break;
                                case 6:
                                    items[Pos].SetDefaults(304, true);
                                    break;
                                case 7:
                                    items[Pos].SetDefaults(2322, true);
                                    break;
                                case 8:
                                    items[Pos].SetDefaults(2329, true);
                                    break;
                            }
                            items[Pos].stack = Main.rand.Next(1, 3);
                            Pos++;
                        }
                        if(Main.rand.Next(3) != 0)
                        {
                            items[Pos].SetDefaults(2350, true);
                            items[Pos].stack = Main.rand.Next(1, 3);
                            Pos++;
                        }
                        if(Main.rand.Next(2) == 0)
                        {
                            if(Style == 11)
                            {
                                items[Pos].SetDefaults(974, true);
                            }
                            else
                            {
                                items[Pos].SetDefaults(8, true);
                            }
                            items[Pos].stack = Main.rand.Next(11) + 10;
                            Pos++;
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            items[Pos].SetDefaults(72, true);
                            items[Pos].stack = Main.rand.Next(50, 91);
                            Pos++;
                        }
                    }
                    else if(y < Main.maxTilesY - 250)
                    {
                        if(PrimaryLoot > 0)
                        {
                            items[Pos].SetDefaults(PrimaryLoot, true);
                            items[Pos].Prefix(-1);
                            Pos++;
                            if (SpawnIceMirror && Main.rand.Next(5) == 0)
                            {
                                items[Pos].SetDefaults(3199, true);
                                Pos++;
                            }
                            if (SpawnMahoganyTreeStuff && Main.rand.Next(6) == 0)
                            {
                                items[Pos++].SetDefaults(3360, true);
                                items[Pos++].SetDefaults(3361, true);
                            }
                        }
                        else
                        {
                            if (Main.rand.Next(40) == 0)
                            {
                                items[Pos].SetDefaults(906, true);
                                items[Pos].Prefix(-1);
                                Pos++;
                            }
                            else if (Main.rand.Next(15) == 0)
                            {
                                items[Pos].SetDefaults(997, true);
                                items[Pos].Prefix(-1);
                                Pos++;
                            }
                            else
                            {
                                switch (Main.rand.Next(7))
                                {
                                    case 0:
                                        items[Pos].SetDefaults(49, true);
                                        items[Pos].Prefix(-1);
                                        Pos++;
                                        break;
                                    case 1:
                                        items[Pos].SetDefaults(50, true);
                                        items[Pos].Prefix(-1);
                                        Pos++;
                                        break;
                                    case 2:
                                        items[Pos].SetDefaults(53, true);
                                        items[Pos].Prefix(-1);
                                        Pos++;
                                        break;
                                    case 3:
                                        items[Pos].SetDefaults(54, true);
                                        items[Pos].Prefix(-1);
                                        Pos++;
                                        break;
                                    case 4:
                                        items[Pos].SetDefaults(55, true);
                                        items[Pos].Prefix(-1);
                                        Pos++;
                                        break;
                                    case 5:
                                        items[Pos].SetDefaults(975, true);
                                        items[Pos].Prefix(-1);
                                        Pos++;
                                        break;
                                    case 6:
                                        items[Pos].SetDefaults(930, true);
                                        items[Pos].Prefix(-1);
                                        Pos++;
                                        items[Pos].SetDefaults(931, true);
                                        items[Pos].stack = Main.rand.Next(26) + 25;
                                        break;
                                }
                            }
                        }
                        if (Main.rand.Next(5) == 0)
                        {
                            items[Pos].SetDefaults(43, true);
                            Pos++;
                        }
                        if (Main.rand.Next(3) == 0)
                        {
                            items[Pos].SetDefaults(167, true);
                            Pos++;
                        }
                        if (Main.rand.Next(4) == 0)
                        {
                            items[Pos].SetDefaults(51, true);
                            items[Pos].stack = Main.rand.Next(26) + 25;
                            Pos++;
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                items[Pos].SetDefaults(WorldGen.goldBar, true);
                            }
                            else
                            {
                                items[Pos].SetDefaults(WorldGen.silverBar, true);
                            }
                            items[Pos].stack = Main.rand.Next(8) + 3;
                            Pos++;
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                items[Pos].SetDefaults(41, true);
                            }
                            else
                            {
                                items[Pos].SetDefaults(279, true);
                            }
                            items[Pos].stack = Main.rand.Next(26) + 25;
                            Pos++;
                        }
                        if(Main.rand.Next(2) == 0)
                        {
                            items[Pos].SetDefaults(188, true);
                            items[Pos].stack = Main.rand.Next(3) + 3;
                            Pos++;
                        }
                        if(Main.rand.Next(3) > 0)
                        {
                            switch (Main.rand.Next(6))
                            {
                                case 0:
                                    items[Pos].SetDefaults(296, true);
                                    break;
                                case 1:
                                    items[Pos].SetDefaults(295, true);
                                    break;
                                case 2:
                                    items[Pos].SetDefaults(299, true);
                                    break;
                                case 3:
                                    items[Pos].SetDefaults(302, true);
                                    break;
                                case 4:
                                    items[Pos].SetDefaults(303, true);
                                    break;
                                case 5:
                                    items[Pos].SetDefaults(305, true);
                                    break;
                            }
                            items[Pos].stack = Main.rand.Next(1, 3);
                            Pos++;
                        }
                        if(Main.rand.Next(3) > 1)
                        {
                            switch (Main.rand.Next(7))
                            {
                                case 0:
                                    items[Pos].SetDefaults(301, true);
                                    break;
                                case 1:
                                    items[Pos].SetDefaults(302, true);
                                    break;
                                case 2:
                                    items[Pos].SetDefaults(297, true);
                                    break;
                                case 3:
                                    items[Pos].SetDefaults(304, true);
                                    break;
                                case 4:
                                    items[Pos].SetDefaults(2329, true);
                                    break;
                                case 5:
                                    items[Pos].SetDefaults(2351, true);
                                    break;
                                case 6:
                                    items[Pos].SetDefaults(2329, true);
                                    break;
                            }
                            items[Pos].stack = Main.rand.Next(1, 3);
                            Pos++;
                        }
                        if(Main.rand.Next(2) == 0)
                        {
                            items[Pos].SetDefaults(2350, true);
                            items[Pos].stack = Main.rand.Next(1, 3);
                            Pos++;
                        }
                        if(Main.rand.Next(2) == 0)
                        {
                            if(Main.rand.Next(2) == 0)
                            {
                                if(Style == 11)
                                {
                                    items[Pos].SetDefaults(974, true);
                                }
                                else
                                {
                                    items[Pos].SetDefaults(8, true);
                                }
                            }
                            else
                            {
                                items[Pos].SetDefaults(282, true);
                            }
                            items[Pos].stack = Main.rand.Next(16) + 15;
                            Pos++;
                        }
                        if(Main.rand.Next(2) == 0)
                        {
                            items[Pos].SetDefaults(73, true);
                            items[Pos].stack = Main.rand.Next(1, 3);
                            Pos++;
                        }
                    }
                    else
                    {
                        if(PrimaryLoot > 0)
                        {
                            items[Pos].SetDefaults(PrimaryLoot, true);
                            items[Pos].Prefix(-1);
                            Pos++;
                        }
                        else
                        {
                            switch (Main.rand.Next(4))
                            {
                                case 0:
                                    items[Pos].SetDefaults(49, true);
                                    items[Pos].Prefix(-1);
                                    break;
                                case 1:
                                    items[Pos].SetDefaults(50, true);
                                    items[Pos].Prefix(-1);
                                    break;
                                case 2:
                                    items[Pos].SetDefaults(53, true);
                                    items[Pos].Prefix(-1);
                                    break;
                                case 3:
                                    items[Pos].SetDefaults(54, true);
                                    items[Pos].Prefix(-1);
                                    break;
                            }
                            Pos++;
                        }
                        if (Main.rand.Next(3) == 0)
                        {
                            items[Pos].SetDefaults(167, true);
                            Pos++;
                        }
                        if (Main.rand.Next(3) == 0)
                        {
                            if (Main.rand.Next(2) == 0)
                                items[Pos].SetDefaults(117, true);
                            else
                                items[Pos].SetDefaults(WorldGen.goldBar, true);
                            items[Pos].stack = Main.rand.Next(16) + 15;
                            Pos++;
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            if (Main.rand.Next(2) == 0)
                                items[Pos].SetDefaults(265, true);
                            else
                                items[Pos].SetDefaults(278, true);
                            items[Pos].stack = Main.rand.Next(26) + 50;
                            Pos++;
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            if (Main.rand.Next(2) == 0)
                                items[Pos].SetDefaults(226, true);
                            else
                                items[Pos].SetDefaults(227, true);
                            items[Pos].stack = Main.rand.Next(6) + 15;
                            Pos++;
                        }
                        if(Main.rand.Next(4) > 0)
                        {
                            switch (Main.rand.Next(8))
                            {
                                case 0:
                                    items[Pos].SetDefaults(296, true);
                                    break;
                                case 1:
                                    items[Pos].SetDefaults(295, true);
                                    break;
                                case 2:
                                    items[Pos].SetDefaults(293, true);
                                    break;
                                case 3:
                                    items[Pos].SetDefaults(288, true);
                                    break;
                                case 4:
                                    items[Pos].SetDefaults(294, true);
                                    break;
                                case 5:
                                    items[Pos].SetDefaults(297, true);
                                    break;
                                case 6:
                                    items[Pos].SetDefaults(304, true);
                                    break;
                                case 7:
                                    items[Pos].SetDefaults(2323, true);
                                    break;
                            }
                            items[Pos].stack = Main.rand.Next(1, 3);
                            Pos++;
                        }
                        if(Main.rand.Next(3) > 0)
                        {
                            switch (Main.rand.Next(8))
                            {
                                case 0:
                                    items[Pos].SetDefaults(305, true);
                                    break;
                                case 1:
                                    items[Pos].SetDefaults(301, true);
                                    break;
                                case 2:
                                    items[Pos].SetDefaults(302, true);
                                    break;
                                case 3:
                                    items[Pos].SetDefaults(288, true);
                                    break;
                                case 4:
                                    items[Pos].SetDefaults(300, true);
                                    break;
                                case 5:
                                    items[Pos].SetDefaults(2351, true);
                                    break;
                                case 6:
                                    items[Pos].SetDefaults(2348, true);
                                    break;
                                case 7:
                                    items[Pos].SetDefaults(2345, true);
                                    break;
                            }
                            items[Pos].stack = Main.rand.Next(1, 3);
                            Pos++;
                        }
                        if (Main.rand.Next(3) == 0)
                        {
                            items[Pos].SetDefaults(2350, true);
                            items[Pos].stack = Main.rand.Next(1, 3);
                            Pos++;
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            if(Main.rand.Next(2) == 0)
                                items[Pos].SetDefaults(8, true);
                            else
                                items[Pos].SetDefaults(282, true);
                            items[Pos].stack = Main.rand.Next(16) + 15;
                            Pos++;
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            items[Pos].SetDefaults(73, true);
                            items[Pos].stack = Main.rand.Next(2, 5);
                            Pos++;
                        }
                    }
                    if (Pos == 0)
                        continue;
                    if (Style == 10 && Main.rand.Next(4) == 0)
                    {
                        items[Pos].SetDefaults(2204, true);
                        Pos++;
                    }
                    if (Style == 11 && Main.rand.Next(7) == 0)
                    {
                        items[Pos].SetDefaults(2198, true);
                        Pos++;
                    }
                    if (Style == 12 && Main.rand.Next(2) == 0)
                    {
                        items[Pos].SetDefaults(2196, true);
                        Pos++;
                    }
                    if (Style == 13 && Main.rand.Next(3) == 0)
                    {
                        items[Pos].SetDefaults(2197, true);
                        Pos++;
                    }
                    if (Style == 16)
                    {
                        items[Pos].SetDefaults(2195, true);
                        Pos++;
                    }
                    if (Main.wallDungeon[t.wall] && Main.rand.Next(8) == 0)
                    {
                        items[Pos].SetDefaults(2192, true);
                        Pos++;
                    }
                    if(Style == 16)
                    {
                        if(Main.rand.Next(5) == 0)
                        {
                            items[Pos].SetDefaults(2767, true);
                        }
                        else
                        {
                            items[Pos].SetDefaults(2766, true);
                            items[Pos].stack = Main.rand.Next(3, 8);
                        }
                        Pos++;
                    }
                    //Add stuff here.
                }
            }
        }
    }
}
