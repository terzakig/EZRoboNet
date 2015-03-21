
/////////////////// EZRobotNet ///////////////////////////////////
//
//			Arduino .pde file: eznetexample.pde
// This is a simple example on how to use EZRoboNet with an Arduino

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

#include "cppfix.h"



 LMMobileRobot *pcart;

     
void setup() {
	// Serial1 will be the designated communications serial port
	HardwareSerial* pRadioPort = &Serial1;

    // Initializing radio serial port (obviously, it could also be just a RS232/USB cable)
	byte node_id = 2; // giveing an id to the node
	pNetDevice = new EZRoboNetDevice(node_id, // node id = 2
								     t_Node_Robot, // type of node is "robot"
									 pRadioPort    // pointer to the serial port connected to the aerial transceiver
									 );
	// NOTE HERE THAT: The EZRoboNetDevice will start the serial port at 19200 baud and there is no need to initialize it manually
  
      
}

// the Arduino main loop 
void loop() {

	// checking the NetDevice for available packets in order to act
    if (pNetDevice->packetsAvailable()>0) { // packet is available
      EZPacket* pack = pNetDevice->getNextAvailablePacket();
      byte packsender = pack->SenderID;
      // unwrapping the packet
      CommandMsg* inmsg = (CommandMsg*)malloc(sizeof(CommandMsg));
      inmsg->robot = (t_Robot)pack->Message[0];
      inmsg->Cmd = (RobotCmd)pack->Message[1];
      inmsg->ParamsLength = pack->Message[2] + pack->Message[3]*256;
      inmsg->CmdParams = (byte*)malloc(inmsg->ParamsLength);
  
	  
      int i;
      for (i=0; i<msg->ParamsLength; i++)
        inmsg->CmdParams[i] = pack->Message[4+i];
      // packet unwrapped

	  ///////////////// HERE, DO SOMETHING WITH THE MESSAGE //////////////////

	  // sending an answer
	  CommandMsg* outmsg = (CommandMsg*)malloc(sizeof(CommandMsg));
	  outmsg->robot = t_HandmadeCart; // robot types are RSV2 and HandmadeCart (but you can add as many types as you like. It is not really make a difference in data handling...)
	  outmsg->Cmd = rc_SensorData;    // the message supposedly contains sensor data; this is obviosuly just a tage. Data can be anything
  	  outmsg->ParamsLength = 16;
	  // allocating the parameter section. Should be 16 bytes
      outmsg->CmdParams = (byte*)malloc(5); //5-byte message HELLO
	  outmsg->CmdParams[0] = 'H'; msg->CmdParams[1] = 'E'; msg->CmdParams[2] = 'L'; msg->CmdParams[3] = 'L'; msg->CmdParams[4] = 'O';

	  // Send the message!
	  EZPacket* sensorpack = createSensorDataPacket(packsender);
	  // push the sensorpack into the outbound queue for transmission
	  while(!pNetDevice->pushOutboundPacket(sensorpack)); // wait until the packet has entereed the transmission (outbound) queue (it will be destroyed after transmission);
			

	  // disposing the incoming and outbound message structures
      disposeCommandMsg(inmsg);
	  disposeCommandMsg(outmsg);

      
	  // disposing the packet
      EZRoboNetDevice::disposePacket(pack); // remember to always use this method to dispose paccket!
      
	}
 
}
