﻿/////////////////// EZRobotNet ///////////////////////////////////
//
//			A RoboEntity class: This a class that implements a remote controller of a robot (hence the "avatar id" corresponds to ther espective drone robot in the field)
//          The robo entity is essentially the "brain" of an MCU controlled drone:
//                 RoboEntity.cs

// A Network Protocol for Wireless/Wired Serial Communications for Cost Effective Robotic applications
// 
// Copyright (C) 2010 George Terzakis

//	This program is free software; you can redistribute it and/or modify
//	it under the terms of the GNU General Public License as published by
//	the Free Software Foundation; either version 2 of the License, or
//	(at your option) any later version.

//	This program is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//	GNU General Public License for more details.
//	You should have received a copy of the GNU General Public License along
//	with this program; if not, write to the Free Software Foundation, Inc.,
//	51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Timers;

namespace EZRoboNetExamplePC
{
    class RoboEntity
    {
        // EZRoboNet Packet Queue
        public EZPacket[] ReceivedPackets;
        public int ReceivedPacketsNum;
        public const int MAX_RECEIVED_PACKETS_NUM = 100;
        // the NetDevice to which the entity belongs
        public EZRoboNetDevice NetDevice;
        // the ID by which the robot is identified by the NetDevice (assigned by the NetDevice)
        public int EntityID; 
        // The network node ID of the robot coresponding to this station's entity
        public byte AvatarNodeID;

        // random generator
        public static Random RandGen;

        // A Timer to Clock the Entity
        Timer SyncTimer;

        public RoboEntity(EZRoboNetDevice netdevice, byte avatarnodeid, Random randgen)
        {
            NetDevice = netdevice;
            AvatarNodeID = avatarnodeid;

            // assigning random generator
            RandGen = randgen;

            // initializing the timer
            SyncTimer = new Timer();
            SyncTimer.Interval = 500; // 0.5 s interval
            SyncTimer.Elapsed += handleMessages;

            // initializing the incoming packet queue
            ReceivedPackets = new EZPacket[MAX_RECEIVED_PACKETS_NUM];
            ReceivedPacketsNum = 0;

            // registering the entity to the NetDevice server
            EntityID = NetDevice.registerEntity(this);

            // starting the Timer
            SyncTimer.Start();
        }

        // adds an incoming packet to the PacketsReceived queue
        public void addIncomingPacket(EZPacket pack)
        {
            ReceivedPacketsNum = (ReceivedPacketsNum < MAX_RECEIVED_PACKETS_NUM - 1) ? ReceivedPacketsNum + 1 : 1;
            ReceivedPackets[ReceivedPacketsNum - 1] = pack;
        }

        // wraps a packet given a raw message (payload) for the avatar robot
        private EZPacket createEZPacket(byte[] payload, int payloadsize)
        {
            EZPacket pack = new EZPacket();
            pack.ReceiverID = AvatarNodeID;
            pack.SenderID = NetDevice.NodeID;
            pack.SenderNodeType = NetDevice.DevNodeType;
            pack.MessageLength = (ushort)payloadsize;
            int i;
            pack.Message = new byte[pack.MessageLength];
            for (i = 0; i < payloadsize; i++)
                pack.Message[i] = payload[i];

            return pack;
        }


        // sends a command message to the avatar
        public void sendCommandMessage(CommandMsg cmdmsg)
        {
            int paramLength = cmdmsg.ParamsLength;
            int rawmsgLength = paramLength + 4;
            byte[] rawcmdmsg = new byte[rawmsgLength];

            rawcmdmsg[0] = (byte)cmdmsg.robot;
            rawcmdmsg[1] = (byte)cmdmsg.Cmd;
            rawcmdmsg[2] = (byte)(paramLength & 0x0FF);
            rawcmdmsg[3] = (byte)((paramLength >> 8) & 0x0FF);
            int i;
            for (i = 0; i < paramLength; i++)
                rawcmdmsg[4 + i] = cmdmsg.CmdParams[i];
            // creating the packet now
            EZPacket pack = createEZPacket(rawcmdmsg, rawmsgLength);
            // sending
            NetDevice.pushOutboundPacket(pack);
        }
        
        // returns the number of available packets in the received queue
        public int packetsAvailable()
        {
            return ReceivedPacketsNum;
        }

        // gets a packet from the queue
        public EZPacket getNextAvailablePacket()
        {
            EZPacket pack;
            pack = ReceivedPackets[ReceivedPacketsNum - 1];
            ReceivedPacketsNum--;
            return pack;


            
        }

        // Entity Events Handler (triggered by the Timer)
        public virtual void handleMessages(object sender, EventArgs e)
        {
        }
        
        //public EZPacket retrieveAvailablePacket() 

    }
}
