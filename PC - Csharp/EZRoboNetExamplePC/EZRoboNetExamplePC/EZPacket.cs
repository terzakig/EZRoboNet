/////////////////// EZRobotNet ///////////////////////////////////
//
//			A Packet structure: EZPacket.cs

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

namespace EZRoboNetExamplePC
{
    struct EZPacket
    {
        public byte SenderID;     // 1-255. 
        public byte ReceiverID;   // 1-255. 0 broadcast
        public byte SenderNodeType;    // type of sender node (robot or station) 
        public ushort MessageLength;
        public byte[] Message;     // maximum 1024 bytes  
    }
}
