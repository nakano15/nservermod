using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace nservermod
{
	public class nservermod : Mod
	{
        private static bool _SingleplayerMode = false;
        private static nservermod instance;
        public static bool IsInSingleplayer { get { return _SingleplayerMode; } internal set { _SingleplayerMode = value; } }
        public static ModPacket ReturnPacket
        {
            get
            {
                return instance.GetPacket();
            }
        }
        public static DateTime LastTime = new DateTime(), CurrentTime = new DateTime();
        private static byte NewHourVal = 255, NewMinuteVal = 255;
        public static byte GetHourValue { get { return NewHourVal; } }
        public static byte GetMinuteValue { get { return NewMinuteVal; } }
        public static byte WofSpawnMessages = 255;

        public override void Load()
        {
            instance = this;
            _SingleplayerMode = Main.netMode == 0;
            CurrentTime = LastTime = DateTime.Now;
        }

        public override void Unload()
        {
            instance = null;
            WorldMod.OnUnload();
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            NetMod.ReceiveMessage((NetMod.MessageIDs)reader.ReadByte(), reader, whoAmI);
        }

        public override void PreUpdateEntities()
        {
            LastTime = CurrentTime;
            CurrentTime = DateTime.Now;
            if (CurrentTime.Hour != LastTime.Hour)
                NewHourVal = (byte)CurrentTime.Hour;
            else
                NewHourVal = 255;
            if (CurrentTime.Minute != LastTime.Minute)
                NewMinuteVal = (byte)CurrentTime.Minute;
            else
                NewMinuteVal = 255;
            WofSpawnMessageTimes();
        }

        public static void SendMessage(string Message, byte R = 255, byte G = 255, byte B = 255)
        {
            if (Main.netMode == 0)
            {
                Main.NewText(Message, R, G, B);
            }
            else if (Main.netMode == 2)
            {
                NetMessage.BroadcastChatMessage(Terraria.Localization.NetworkText.FromLiteral(Message), new Microsoft.Xna.Framework.Color(R, G, B));
            }
        }

        private void WofSpawnMessageTimes()
        {
            if (WofSpawnMessages < 255)
            {
                WofSpawnMessages++;
                if (WofSpawnMessages == 10)
                {
                    SendMessage("Wall of Flesh: *Nhac!*", 255, 0, 0);
                }
                if (WofSpawnMessages == 70)
                {
                    SendMessage("Wall of Flesh: *Crunch, Munch, Munch, Munch.*", 255, 0, 0);
                }
                if (WofSpawnMessages == 130)
                {
                    SendMessage("Wall of Flesh: *Gulp.*", 255, 0, 0);
                }
                if (WofSpawnMessages == 190)
                {
                    SendMessage("Wall of Flesh: *Buurp!*", 255, 0, 0);
                    for(byte i = 0; i < 255; i++)
                    {
                        if (Main.player[i].active && Main.player[i].ZoneUnderworldHeight)
                        {
                            Main.player[i].AddBuff(Terraria.ID.BuffID.Poisoned, 60 * 60, false);
                        }
                    }
                }
            }
        }
    }
}