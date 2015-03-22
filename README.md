# EZRoboNet
A network protocol for wireless serial communications - Arduino (C++) and PC (C#) implementations.

Description
-----------
EZRoboNet is a network protocol for robotics applications in which the robot acts more like a drone and 
most of the processing is done remotely (most probably in PC). Each node in the network can be characterized as either station or robot. Stations are to the computers which control the drones; robot node types typically refer to drones.

This protrocol offers essentially 3 things: 1) transmission of data of arbitrary length (fragmentation and reassembly of
packets), 2) Allows for the sharing of the same serial by managing multiple threads controlling separate robots in the 
field and, 3) Does all the necessary handshaking and checking for reliable data transmission.

Packets and Messages
--------------------
Transmission concerns packets of arbitrary length which may contain raw messages or command messages. It is generally 
up to the developer on how to construct messages, although there is a list of such messages and the respective methods
to construct them (see files ./Arduino - Cpp/ezrobonetcmds.cpp and ./PC - Csharp/EZRoboNetExamplePC/EZRoboNetExamplePC/EZRoboNetCmds.cs


RoboEntities
------------
A RoboEntity is a class that corresponds to the processing of one (of many) drones running in a station. It is a separate
thread (actually, a timer but this can easily change). All robo-entities share the same serial port through the local 
EZRoboNet device. The entity itself is unaware of the fact that other entities also transmit and receive through the same 
port; the station's EZRoboNetDevice does all the handling for them. The entities simply register to the device and 
afterwards operate independently (in terms of communications), although they can always exchange data directly (since they 
are essentially threads running under the same application; for example, they may share a map of the environment).

Examples
--------
Sample Arduino and PC code is given for an exchange of simple "Hello" messages between the station (PC) and the drone 
(Arduino).
