/////////////////// EZRobotNet ///////////////////////////////////
//
//			MyRobot class inherits RoboEntoty and overrides the incoming data handling function 
//          this a simple example. You can make your roboentity as elaborate as you like
//                      MyRobot.cs

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
    class MyRobot : RoboEntity
    {


        public MyRobot(EZRoboNetDevice netdevice, // the robonet device
                       byte avatarnodeid,         // an id for the robot
                       Random randgen             // and a random number generator (not necessary. residual parameter from early implementations)
                       )
            : base(netdevice, avatarnodeid, randgen)
        {
            // initialize your robot here
        }

        // the message handling function (called by an internal timer)
        public override void handleMessages(object sender, EventArgs e)
        {
            int packetsavailable = packetsAvailable();
            EZPacket pack;
            // 1. checking pakcets available and acting

            if (packetsavailable > 0)
            {
                pack = getNextAvailablePacket();

                // unwrapping the packet
                CommandMsg msg = RobotMessenger.convertBytes2CommandMsg(pack.Message);

                ////////////////// Handle the message here //////////////////

                // sending a sample answer
                CommandMsg outmsg = new CommandMsg();
                outmsg.robot = t_Robot.t_Station;
                outmsg.Cmd = RobotCmd.rc_ManualCommand;
                outmsg.CmdParams = new byte[] { (byte)'H', (byte)'E', (byte)'L', (byte)'L', (byte)'O' };
                outmsg.ParamsLength = (byte)outmsg.CmdParams.Length; // one byte - ability index

                // send it
                sendCommandMessage(outmsg);




            }

        }


        



    }
}
