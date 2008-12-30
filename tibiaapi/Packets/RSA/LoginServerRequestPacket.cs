﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class LoginServerRequestPacket
    {
        public static NetworkMessage CreateLoginServerRequestPacket(ushort Version,
    byte[] Signatures, string AccountName, string Password)
        {
            return CreateLoginServerRequestPacket(0x02, Version, Signatures, AccountName, Password);
        }


        public static NetworkMessage CreateLoginServerRequestPacket(byte OS, ushort Version,
            byte[] Signatures, string AccountName, string Password)
        {
            byte[] XteaKey=new byte[16];
            new Random().NextBytes(XteaKey);
            return CreateLoginServerRequestPacket(OS, Version, Signatures, XteaKey, AccountName, Password, false);
        }
        
        public static NetworkMessage CreateLoginServerRequestPacket(byte OS, ushort Version,
            byte[] Signatures, byte[] XteaKey, string AccountName, string Password)
        {
            return CreateLoginServerRequestPacket(OS, Version, Signatures, XteaKey, AccountName, Password, false);
        }

        public static NetworkMessage CreateLoginServerRequestPacket(byte OS, ushort Version,
            byte[] Signatures, byte[] XteaKey, string AccountName, string Password,bool OpenTibia)
        {
            NetworkMessage msg = new NetworkMessage();
            msg.AddByte(0x01);
            msg.AddUInt16(OS);
            msg.AddUInt16(Version);
            msg.AddBytes(Signatures);
            msg.AddByte(0x0);
            msg.AddBytes(XteaKey);
            msg.AddString(AccountName);
            msg.AddString(Password);
            if (OpenTibia) msg.RsaOTEncrypt(23);
            else msg.RsaCipEncrypt(23);
            msg.InsertAdler32();
            msg.InsertPacketHeader();
            return msg;
        }
        
             


    }
}
