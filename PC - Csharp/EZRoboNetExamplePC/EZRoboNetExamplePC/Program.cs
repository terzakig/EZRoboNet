using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace EZRoboNetExamplePC
{
    class Program
    {
        static Random rnd;

        [STAThread]
        static void Main(string[] args)
        {
            EZRoboNetDevice netdev = new EZRoboNetDevice(1, (byte)t_Robot.t_Station); // creating a network device as a station

            rnd = new Random((int)DateTime.Now.Ticks);
            
            MyRobot myrobot = new MyRobot(netdev, 
                                            2,   // this is the ID of the robot which MyRobot controls (in this case it is the arduino, and its assigned id is 2) 
                                            rnd // this is an unused/useless parameter for now...
                                            ); 
            
            // send something out
            // sending a sample answer
            CommandMsg outmsg = new CommandMsg();
            outmsg.robot = t_Robot.t_Station;
            outmsg.Cmd = RobotCmd.rc_ManualCommand;
            outmsg.CmdParams = new byte[] { (byte)'H', (byte)'E', (byte)'L', (byte)'L', (byte)'O' };
            outmsg.ParamsLength = (byte)outmsg.CmdParams.Length; // 5 bytes

            // send it
            myrobot.sendCommandMessage(outmsg);

            while (true) Application.DoEvents();
        }
    }
}
