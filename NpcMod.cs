using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nservermod
{
    public class NpcMod : GlobalNPC
    {
        public override void SetDefaults(NPC npc)
        {
            if (nservermod.IsInSingleplayer)
                return;
            switch (npc.type)
            {
                case NPCID.SkeletronHead:
                case NPCID.SkeletronHand:
                    npc.lifeMax *= 5;
                    npc.damage += 20;
                    npc.defense += 10;
                    break;
                case NPCID.AngryBones:
                case NPCID.AngryBonesBig:
                case NPCID.AngryBonesBigHelmet:
                case NPCID.AngryBonesBigMuscle:
                case NPCID.DarkCaster:
                case NPCID.CursedSkull:
                    npc.lifeMax *= 5;
                    npc.damage += 10;
                    npc.defense += 5;
                    break;
                case NPCID.BlazingWheel:
                case NPCID.SpikeBall:
                    npc.damage += 30;
                    break;
            }
        }

        public override void PostAI(NPC npc)
        {
            if (nservermod.IsInSingleplayer || Main.netMode == 1)
                return;
            switch (npc.type)
            {
                case NPCID.WallofFlesh:
                    {
                        npc.active = false;
                        npc.netUpdate = true;
                        nservermod.WofSpawnMessages = 0;
                    }
                    break;
                case NPCID.SkeletronHead:
                    if (npc.ai[2] == 0)
                    {
                        if (npc.ai[1] == 0) //20, -10
                        {
                            for (int x = -1; x < 2; x += 2)
                            {
                                NPC.NewNPC((int)npc.Center.X + x * 20, (int)npc.Center.Y - 10, NPCID.CursedSkull);
                            }
                        }
                        else if (npc.ai[1] == 1)
                        {
                            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 10, NPCID.DarkCaster);
                        }
                    }
                    else if (npc.ai[1] == 0 && (npc.ai[2] == 200 || npc.ai[2] == 400 || npc.ai[2] == 600))
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 10, NPCID.AngryBones);
                    }
                    break;
                case NPCID.SkeletronHand:
                    if(npc.ai[3] == 100 || npc.ai[3] == 200)
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 10, NPCID.WaterSphere);
                    }
                    break;
            }
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if(Main.moonPhase == 0 && !spawnInfo.playerInTown)
            {
                pool.Add(NPCID.Werewolf, 1f / 30);
            }
            if (!Main.hardMode)
            {
                if (!spawnInfo.playerInTown)
                {
                    if (Main.raining && spawnInfo.player.ZoneSnow)
                    {
                        pool.Add(NPCID.IceGolem, 1f / 300);
                    }
                    if (spawnInfo.player.ZoneSandstorm)
                    {
                        pool.Add(NPCID.SandElemental, 1f / 300);
                    }
                    if (!NPC.AnyNPCs(NPCID.WyvernHead))
                    {
                        Tile tile = Main.tile[(int)(spawnInfo.player.Center.X * (1f / 16)), (int)(spawnInfo.player.Center.Y * (1f / 16))];
                        if (tile.wall == 0)
                            pool.Add(NPCID.WyvernHead, 1f / 1000);
                    }
                    if (spawnInfo.spawnTileType == TileID.Sand)
                    {
                        pool.Add(NPCID.Mummy, 1f / 50);
                    }
                    if (spawnInfo.spawnTileType == TileID.Ebonsand || spawnInfo.spawnTileType == TileID.Crimsand)
                    {
                        pool.Add(NPCID.DarkMummy, 1f / 50);
                    }
                    if (spawnInfo.spawnTileType == TileID.MushroomGrass)
                    {
                        pool.Add(NPCID.TruffleWorm, 1f / 300);
                    }
                    if (spawnInfo.player.ZoneJungle)
                    {
                        pool.Add(NPCID.Lihzahrd, 1f / 250);
                    }
                }
                if (spawnInfo.player.ZoneBeach)
                {
                    pool.Add(NPCID.CreatureFromTheDeep, 1f / 300);
                }
                if (!Main.dayTime)
                {
                    pool.Add(NPCID.Psycho, 1f / 300);
                }
            }
        }

        public override void NPCLoot(NPC npc)
        {
            if(npc.type == Terraria.ID.NPCID.EyeofCthulhu && NPC.downedBoss3 && Main.rand.Next(100) == 0)
            {
                NPC.SpawnOnPlayer(npc.target, NPCID.Spazmatism);
            }
        }

        public bool IsEngagingNpc(int NpcID, Player player)
        {
            if (!NPC.AnyNPCs(NpcID))
                return false;
            NPC npc = Main.npc[NPC.FindFirstNPC(NpcID)];
            return (Math.Abs(player.Center.X - npc.Center.X) < 1000 &&
                Math.Abs(player.Center.Y - npc.Center.Y) < 800);
        }
    }
}
