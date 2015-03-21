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
