﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace nservermod
{
    public class WorldMod : ModWorld
    {
        private static bool? EnableChestRespawn = null, EnableOreRespawn = null, EnableLifeCrystalRespawn = null, EnableDungeonReset = null, EnableWallOfFlesh = null,
            EnableSpiderWebRespawn = null, EnableShadowOrbRespawn = null, EnablePotsRespawn = null, EnableEnchantedSwordRespawn = null, EnableSurfaceProtection = null,
            EnableCustomModSpawns = null, EnableHarderDungeonAndSkeletron = null;
        public static bool? IsChestRespawnEnabled
        {
            get { if (EnableChestRespawn.HasValue) return EnableChestRespawn.Value; return Main.netMode == 2; }
            set { EnableChestRespawn = value; }
        }
        public static bool? IsOreRespawnEnabled
        {
            get { if (EnableOreRespawn.HasValue) return EnableOreRespawn.Value; return Main.netMode == 2; }
            set { EnableOreRespawn = value; }
        }
        public static bool? IsLCRespawnEnabled
        {
            get { if (EnableLifeCrystalRespawn.HasValue) return EnableLifeCrystalRespawn.Value; return Main.netMode == 2; }
            set { EnableLifeCrystalRespawn = value; }
        }
        public static bool? IsDungeonResetEnabled
        {
            get { if (EnableDungeonReset.HasValue) return EnableDungeonReset.Value; return Main.netMode == 2; }
            set { EnableDungeonReset = value; }
        }
        public static bool? IsWofEnabled
        {
            get { if (EnableWallOfFlesh.HasValue) return EnableWallOfFlesh.Value; return Main.netMode == 0; }
            set { EnableWallOfFlesh = value; }
        }
        public static bool? IsSpiderWebRespawnEnabled
        {
            get { if (EnableSpiderWebRespawn.HasValue) return EnableSpiderWebRespawn.Value; return Main.netMode == 2; }
            set { EnableSpiderWebRespawn = value; }
        }
        public static bool? IsShadowOrbRespawnEnabled
        {
            get { if (EnableShadowOrbRespawn.HasValue) return EnableShadowOrbRespawn.Value; return Main.netMode == 2; }
            set { EnableShadowOrbRespawn = value; }
        }
        public static bool? IsPotsRespawnEnabled
        {
            get { if (EnablePotsRespawn.HasValue) return EnablePotsRespawn.Value; return Main.netMode == 2; }
            set { EnablePotsRespawn = value; }
        }
        public static bool? IsEnchantedSwordRespawnEnabled
        {
            get { if (EnableEnchantedSwordRespawn.HasValue) return EnableEnchantedSwordRespawn.Value; return Main.netMode == 2; }
            set { EnableEnchantedSwordRespawn = value; }
        }
        public static bool? IsSurfaceProtectionEnabled
        {
            get { if (EnableSurfaceProtection.HasValue) return EnableSurfaceProtection.Value; return Main.netMode == 2; }
            set { EnableSurfaceProtection = value; }
        }
        public static bool? IsHarderDungeonAndSkeleEnabled
        {
            get { if (EnableHarderDungeonAndSkeletron.HasValue) return EnableHarderDungeonAndSkeletron.Value; return Main.netMode == 2; }
            set { EnableHarderDungeonAndSkeletron = value; }
        }
        public static bool? IsCustomMobSpawnsEnabled
        {
            get { if (EnableCustomModSpawns.HasValue) return EnableCustomModSpawns.Value; return Main.netMode == 2; }
            set { EnableCustomModSpawns = value; }
        }

        private static List<Point> TreePlantingPosition = new List<Point>();
        private static List<int[]> DresserLoot = new List<int[]>();

        public static void InitializeWorldMod()
        {
            TreePlantingPosition = new List<Point>();
            InitializeDresserLoots();
        }

        public static void InitializeDresserLoots()
        {
            DresserLoot = new List<int[]>();
            AddDresserLoots(ItemID.SunMask);
            AddDresserLoots(ItemID.MoonMask);
            AddDresserLoots(ItemID.BunnyHood);
            AddDresserLoots(ItemID.BallaHat);
            AddDresserLoots(ItemID.Beanie);
            AddDresserLoots(ItemID.GiantBow);
            AddDresserLoots(ItemID.GreenCap);
            AddDresserLoots(ItemID.JackOLanternMask);
            AddDresserLoots(ItemID.PeddlersHat);
            AddDresserLoots(ItemID.PartyHat);
            AddDresserLoots(ItemID.ReindeerAntlers);
            AddDresserLoots(ItemID.Skelehead);
            AddDresserLoots(ItemID.WizardsHat);
            AddDresserLoots(ItemID.UglySweater);
            AddDresserLoots(ItemID.RobotHat);
            AddDresserLoots(ItemID.SWATHelmet);
            AddDresserLoots(ItemID.CatEars);
            AddDresserLoots(ItemID.Kimono);
            AddDresserLoots(ItemID.GoldCrown, ItemID.PlatinumCrown);
            //Halloween
            AddDresserLoots(ItemID.BrideofFrankensteinDress, ItemID.BrideofFrankensteinMask);
            AddDresserLoots(ItemID.CatMask, ItemID.CatPants, ItemID.CatShirt);
            AddDresserLoots(ItemID.ClothierJacket, ItemID.ClothierPants, ItemID.ClothierVoodooDoll);
            AddDresserLoots(ItemID.CreeperMask, ItemID.CreeperPants, ItemID.CreeperShirt);
            AddDresserLoots(ItemID.CyborgHelmet, ItemID.CyborgPants, ItemID.CyborgShirt);
            AddDresserLoots(ItemID.DryadCoverings, ItemID.DryadLoincloth);
            AddDresserLoots(ItemID.DyeTraderRobe, ItemID.DyeTraderTurban);
            AddDresserLoots(ItemID.FoxMask, ItemID.FoxPants, ItemID.FoxShirt);
            AddDresserLoots(ItemID.GhostMask, ItemID.GhostShirt);
            AddDresserLoots(ItemID.KarateTortoiseMask, ItemID.KarateTortoisePants, ItemID.KarateTortoiseShirt);
            AddDresserLoots(ItemID.LeprechaunHat, ItemID.LeprechaunPants, ItemID.LeprechaunShirt);
            AddDresserLoots(ItemID.NurseHat, ItemID.NursePants, ItemID.NurseShirt);
            AddDresserLoots(ItemID.PixiePants, ItemID.PixieShirt);
            AddDresserLoots(ItemID.PrincessDress, ItemID.PrincessHat);
            AddDresserLoots(ItemID.PumpkinMask, ItemID.PumpkinPants, ItemID.PumpkinShirt);
            AddDresserLoots(ItemID.ReaperHood, ItemID.ReaperRobe);
            AddDresserLoots(ItemID.RobotShirt, ItemID.RobotMask, ItemID.RobotPants);
            AddDresserLoots(ItemID.SpaceCreatureMask, ItemID.SpaceCreaturePants, ItemID.SpaceCreatureShirt);
            AddDresserLoots(ItemID.TreasureHunterPants, ItemID.TreasureHunterShirt);
            AddDresserLoots(ItemID.UnicornMask, ItemID.UnicornPants, ItemID.UnicornShirt);
            AddDresserLoots(ItemID.VampireMask, ItemID.VampirePants, ItemID.VampireShirt);
            AddDresserLoots(ItemID.WitchBoots, ItemID.WitchDress, ItemID.WitchHat);
            AddDresserLoots(ItemID.WolfMask, ItemID.WolfPants, ItemID.WolfShirt);
            //Christmas
            AddDresserLoots(ItemID.MrsClauseHat, ItemID.MrsClauseHeels, ItemID.MrsClauseShirt);
            AddDresserLoots(ItemID.ParkaCoat, ItemID.ParkaHood, ItemID.ParkaPants);
            AddDresserLoots(ItemID.SantaHat, ItemID.SantaPants, ItemID.SantaShirt);
            AddDresserLoots(ItemID.TreeMask, ItemID.TreeShirt, ItemID.TreeTrunks);
            //
            AddDresserLoots(ItemID.PharaohsMask, ItemID.PharaohsRobe);
            AddDresserLoots(ItemID.MartianCostumeMask, ItemID.MartianCostumePants, ItemID.MartianCostumeShirt);
            AddDresserLoots(ItemID.MartianUniformHelmet, ItemID.MartianUniformPants, ItemID.MartianUniformTorso);
            AddDresserLoots(ItemID.MummyMask, ItemID.MummyPants, ItemID.MummyShirt);
            AddDresserLoots(ItemID.PedguinHat, ItemID.PedguinPants, ItemID.PedguinShirt);
            AddDresserLoots(ItemID.PlumbersHat, ItemID.PlumbersPants, ItemID.PlumbersShirt);
            AddDresserLoots(ItemID.RainHat, ItemID.RainCoat);
            AddDresserLoots(ItemID.TheDoctorsPants, ItemID.TheDoctorsShirt);
            AddDresserLoots(ItemID.TuxedoPants, ItemID.TuxedoShirt, ItemID.TopHat);
            AddDresserLoots(3478, 3479); //Wedding set
            AddDresserLoots(ItemID.TaxCollectorHat, ItemID.TaxCollectorPants, ItemID.TaxCollectorSuit);
            AddDresserLoots(ItemID.SteampunkHat, ItemID.SteampunkShirt, ItemID.SteampunkPants);
        }

        public static void AddDresserLoots(params int[] Loots)
        {
            DresserLoot.Add(Loots);
        }

        public static void OnUnload()
        {
            if (TreePlantingPosition != null) TreePlantingPosition.Clear();
            TreePlantingPosition = null;
            if (DresserLoot != null) DresserLoot.Clear();
            DresserLoot = null;
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
            //if (Main.netMode < 2)
            //    return;
            bool Server = Main.netMode == 2;
            UpdateHourlyRespawn();
            if (nservermod.GetMinuteValue < 255 && Main.rand.Next(3) == 0)
            {
                if (IsSpiderWebRespawnEnabled.Value && nservermod.GetMinuteValue % 10 == 4)
                {
                    RespawnSpiderWebs();
                }
                if (IsOreRespawnEnabled.Value && nservermod.GetMinuteValue % 15 == 7)
                {
                    RespawnOres();
                }
                if (IsPotsRespawnEnabled.Value && nservermod.GetMinuteValue % 5 == 3)
                {
                    PlaceNewPots();
                }
                if (IsShadowOrbRespawnEnabled.Value && nservermod.GetMinuteValue % 15 == 11)
                {
                    TryPlacingShadowOrb();
                }
                if (IsEnchantedSwordRespawnEnabled.Value && nservermod.GetMinuteValue % 30 == 0)
                {
                    TryPlacingEnchantedSword();
                }
            }
            if (Main.rand.Next(250) == 0 && IsLCRespawnEnabled.Value)
                PlaceLifeCrystal();
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

        public static void UpdateHourlyRespawn()
        {
            if (nservermod.GetHourValue < 255 && IsChestRespawnEnabled.Value)
            {
                RefillChests();
            }
            if (NPC.downedBoss3 && IsDungeonResetEnabled.Value)
            {
                if (nservermod.GetMinuteValue == 55 && nservermod.GetHourValue < 255 && nservermod.GetHourValue % 2 == 1)
                {
                    nservermod.SendMessage("The dungeon will be resetted in 5 minutes. Everyone leave it.", 255, 128, 128);
                }
                else if (nservermod.GetMinuteValue == 0 && nservermod.GetHourValue % 2 == 0)
                {
                    NPC.downedBoss3 = false;
                    if (NPC.AnyNPCs(NPCID.Clothier))
                    {
                        Main.npc[NPC.FindFirstNPC(NPCID.Clothier)].Transform(NPCID.OldMan);
                        nservermod.SendMessage("The dungeon has been resetted. The clothier was cursed again.", 255, 0, 0);
                    }
                    else
                    {
                        nservermod.SendMessage("The dungeon has been resetted. Skeletron resurged.", 255, 128, 128);
                    }
                    for (int i = 0; i < 255; i++)
                    {
                        if (Main.player[i].active && Main.player[i].ZoneDungeon)
                        {
                            nservermod.SendMessage("<Skeletron> Some fools are eager to join the dead. So be it.", 255, 0, 0);
                            break;
                        }
                    }
                }
            }
        }

        private void TryPlacingShadowOrb()
        {
            int x = Main.rand.Next(40, Main.maxTilesX - 40), y = Main.rand.Next((int)Main.worldSurface, Main.maxTilesY - 130);
            Tile tile = Main.tile[x, y];
            if (tile.wall == WallID.EbonstoneUnsafe || tile.wall == WallID.CrimstoneUnsafe)
            {
                Vector2 Center = new Vector2(x, y) * 16;
                for (int i = 0; i < 255; i++)
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
                if (tile.wall == 0 || Main.wallHouse[tile.wall])
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
            for (int i = 0; i < 10; i++)
            {
                int x = Main.rand.Next(5, Main.maxTilesX - 5), yStart = Main.rand.Next((int)Main.worldSurface, Main.maxTilesY - 10);
                bool LastTileWasFree = false;
                for (byte Attempts = 0; Attempts < 30; Attempts++)
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
                        if (WorldGen.PlacePot(x, y, style: style))
                        {
                            LastTileWasFree = true;
                            break;
                        }
                    }
                }
            }
        }

        private static void RefillChests()
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
            byte CurrentHellChest = 0, CurrentWaterLoot = (byte)Main.rand.Next(3), CurrentDungeonLoot = (byte)Main.rand.Next(8);
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
                    if (Type != TileID.Containers && Type != TileID.Containers2 && Type != TileID.Dressers)
                        continue;
                    bool IsDungeonWall = t.wall == WallID.BlueDungeonTileUnsafe || t.wall == WallID.BlueDungeonSlabUnsafe || t.wall == WallID.BlueDungeonUnsafe ||
                        t.wall == WallID.GreenDungeonSlabUnsafe || t.wall == WallID.GreenDungeonSlabUnsafe || t.wall == WallID.GreenDungeonUnsafe ||
                        t.wall == WallID.BlueDungeonSlabUnsafe || t.wall == WallID.BlueDungeonSlabUnsafe || t.wall == WallID.BlueDungeonUnsafe;
                    bool IsDresser = Type == TileID.Dressers;
                    int Style = t.frameX / 36;
                    int PrimaryLoot = 0;
                    switch (Style)
                    {
                        case 0:
                            if (IsDungeonWall)
                            {
                                PrimaryLoot = ItemID.GoldenKey;
                            }
                            break;
                        case 1:
                        case 2:
                            if (IsDungeonWall)
                            {
                                switch (CurrentDungeonLoot)
                                {
                                    case 0:
                                        PrimaryLoot = 329;
                                        break;
                                    case 1:
                                        PrimaryLoot = 155;
                                        break;
                                    case 2:
                                        PrimaryLoot = 156;
                                        break;
                                    case 3:
                                        PrimaryLoot = 157;
                                        break;
                                    case 4:
                                        PrimaryLoot = 163;
                                        break;
                                    case 5:
                                        PrimaryLoot = 113;
                                        break;
                                    case 6:
                                        PrimaryLoot = 3317;
                                        break;
                                    case 7:
                                        PrimaryLoot = 164;
                                        break;
                                }
                                CurrentDungeonLoot++;
                                if (CurrentDungeonLoot >= 8)
                                    CurrentDungeonLoot = 0;
                            }
                            break;
                        case 10: //Jungle chest
                            PrimaryLoot = WorldGen.GetNextJungleChestItem();
                            break;
                        case 13:
                            switch (Main.rand.Next(3))
                            {
                                default:
                                    PrimaryLoot = 159;
                                    break;
                                case 1:
                                    PrimaryLoot = 65;
                                    break;
                                case 2:
                                    PrimaryLoot = 158;
                                    break;
                            }
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
                    if (IsDresser)
                    {
                        int[] Clothes = DresserLoot[Main.rand.Next(DresserLoot.Count)];
                        foreach (int l in Clothes)
                        {
                            items[Pos].SetDefaults(l, true);
                            Pos++;
                        }
                        if(Main.rand.Next(5) == 0)
                        {
                            switch (Main.rand.Next(7))
                            {
                                case 0:
                                    items[Pos].SetDefaults(ItemID.Robe, true);
                                    break;
                                case 1:
                                    items[Pos].SetDefaults(ItemID.TopazRobe, true);
                                    break;
                                case 2:
                                    items[Pos].SetDefaults(ItemID.SapphireRobe, true);
                                    break;
                                case 3:
                                    items[Pos].SetDefaults(ItemID.EmeraldRobe, true);
                                    break;
                                case 4:
                                    items[Pos].SetDefaults(ItemID.RubyRobe, true);
                                    break;
                                case 5:
                                    items[Pos].SetDefaults(ItemID.DiamondRobe, true);
                                    break;
                                case 6:
                                    items[Pos].SetDefaults(ItemID.AmethystRobe, true);
                                    break;
                            }
                            Pos++;
                        }
                    }
                    if ((PrimaryLoot == 0 && y < Main.worldSurface + 25) || PrimaryLoot == 848)
                    {
                        if (PrimaryLoot > 0)
                        {
                            items[Pos].SetDefaults(PrimaryLoot, true);
                            items[Pos].Prefix(-1);
                            Pos++;
                            switch (PrimaryLoot)
                            {
                                case 848:
                                    items[Pos].SetDefaults(866, true);
                                    Pos++;
                                    break;
                                case 832:
                                    items[Pos].SetDefaults(933, true);
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
                    if (Main.rand.Next(100) == 0)
                    {
                        items[Pos].SetDefaults(ItemID.MoonCharm, true);
                    }
                    if (Main.rand.Next(100) == 0)
                    {
                        items[Pos].SetDefaults(ItemID.NeptunesShell, true);
                    }
                }
            }
        }

        private void RespawnSpiderWebs()
        {
            int x = Main.rand.Next(20, Main.maxTilesX - 20),
                y = Main.rand.Next((int)WorldGen.worldSurfaceHigh, Main.maxTilesY - 20);
            Tile tile = Main.tile[x, y];
            if(!tile.active() && (y > Main.worldSurface || (tile.wall > 0 && !Main.wallHouse[tile.wall])))
            {
                byte MaxAttempts = 20;
                while(!tile.active() && y > WorldGen.worldSurfaceLow)
                {
                    y--;
                    MaxAttempts--;
                    if (MaxAttempts < 0)
                        return;
                    tile = Main.tile[x, y];
                }
                y++;
                int Direction = Main.rand.Next(2) == 0 ? -1 : 1;
                MaxAttempts = 20;
                tile = Main.tile[x, y];
                while (!tile.active())
                {
                    x += Direction;
                    if (x <= 10 || x >= Main.maxTilesX - 10)
                        return;
                    tile = Main.tile[x, y];
                    MaxAttempts--;
                    if (MaxAttempts < 0)
                        return;
                }
                x -= Direction;
                if (y > Main.worldSurface || (tile.wall > 0 && !Main.wallHouse[tile.wall]))
                {
                    WorldGen.TileRunner(x, y, Main.rand.Next(4, 11), Main.rand.Next(2, 4), 51, true, Direction, -1, false, false);
                }
            }
        }

        private void RespawnOres()
        {
            const byte CopperSpawn = 0,
                IronSpawn = 1,
                SilverSpawn = 2,
                GoldSpawn = 3,
                Total = 4;
            for(byte i = 0; i < Total; i++)
            {
                int x = Main.rand.Next(20, Main.maxTilesX - 20), y = 0;
                Tile tile = Main.tile[x, y];
                if (tile.active() || tile.wall > 0)
                    continue;
                byte Direction = (byte)Main.rand.Next(8);
                int TileID = 0;
                int Strength = 0, Steps = 0;
                switch (i)
                {
                    case CopperSpawn:
                        y = Main.rand.Next((int)Main.worldSurface + 30, Main.maxTilesY - 130);
                        TileID = WorldGen.CopperTierOre;
                        Strength = Main.rand.Next(3, 7);
                        Steps = Main.rand.Next(3, 7);
                        break;
                    case IronSpawn:
                        y = Main.rand.Next((int)Main.worldSurface + 30, Main.maxTilesY - 130);
                        TileID = WorldGen.IronTierOre;
                        Strength = Main.rand.Next(3, 6);
                        Steps = Main.rand.Next(3, 6);
                        break;
                    case SilverSpawn:
                        y = Main.rand.Next((int)Main.worldSurface + 30, Main.maxTilesY - 130);
                        TileID = WorldGen.SilverTierOre;
                        Strength = Main.rand.Next(3, 6);
                        Steps = Main.rand.Next(3, 6);
                        break;
                    case GoldSpawn:
                        y = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 130);
                        TileID = WorldGen.goldBar;
                        Strength = Main.rand.Next(3, 6);
                        Steps = Main.rand.Next(3, 6);
                        break;
                }
                {
                    bool PlayerCloseBy = false;
                    Vector2 TilePosition = new Vector2(x, y) * 16;
                    for (byte p = 0; p < 255; p++)
                    {
                        if (Main.player[p].active && (Main.player[p].Center - TilePosition).Length() < 500)
                        {
                            PlayerCloseBy = true;
                            break;
                        }
                    }
                    if (PlayerCloseBy)
                        continue;
                }
                if (TileID != 0)
                {
                    int MaxAttempts = 20;
                    int mx = 0, my = 0;
                    switch (Direction)
                    {
                        case 0:
                        case 1:
                        case 7:
                            my = -1;
                            break;
                        case 2:
                        case 3:
                        case 4:
                            my = 1;
                            break;
                    }
                    switch (Direction)
                    {
                        case 1:
                        case 2:
                        case 3:
                            mx = 1;
                            break;
                        case 5:
                        case 6:
                        case 7:
                            mx = -1;
                            break;
                    }
                    while (!tile.active())
                    {
                        x += mx;
                        y += my;
                        tile = Main.tile[x, y];
                        if(x < 20 || x > Main.maxTilesX - 20 || 
                            y < Main.worldSurface + 30 || y > Main.maxTilesY - 130)
                        {
                            MaxAttempts = -1;
                            break;
                        }
                        if(tile.wall > 0 && Main.wallHouse[tile.wall])
                        {
                            MaxAttempts = -1;
                            break;
                        }
                        MaxAttempts--;
                        if (MaxAttempts < 0)
                            break;
                    }
                    if (MaxAttempts < 0)
                        continue;
                    if (Terraria.ID.TileID.Sets.Ore[tile.type])
                        continue;
                    x -= mx;
                    y -= my;
                    WorldGen.TileRunner(x, y, Strength, Steps, TileID, true, overRide: false);
                }
            }
        }
    }
}
