/////////////////// EZRobotNet ///////////////////////////////////
//
//			Circular buffer for serial input
//            SerialBuffer.cs

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
    class SerialBuffer
    {
        // constants
        public int BUFFER_SIZE = 10000;

        // memebrs
        private byte[] buffer;
        private int AvailableBytes;
        private int NextByteIndex;



        // constructor
        public SerialBuffer()
        {
            buffer = new byte[BUFFER_SIZE];
            AvailableBytes = 0;
            NextByteIndex = 0;
        }

        
        // add byte to the buffer
        public void addByte(byte ch)
        {
            AvailableBytes++;

            buffer[NextByteIndex+AvailableBytes-1] = ch;
            
        }

        // read a number of Bytes from the buffer starting at current index position
        public int readBytes(byte[] abuffer, int numBytes)
        {
            int bytestoread;
            if (AvailableBytes >= numBytes) bytestoread = numBytes;
            else bytestoread = AvailableBytes;

            int i;
            for (i = 0; i < bytestoread; i++)
                abuffer[i] = buffer[NextByteIndex + i];
            AvailableBytes -= bytestoread;
            NextByteIndex = (AvailableBytes == 0) ? 0 : NextByteIndex + bytestoread;

            return bytestoread;
        }


        public int bytesAvailable()
        {
            return AvailableBytes;
        }

        public void Clear()
        {
            AvailableBytes = 0;
            NextByteIndex = 0;
        }


    }
}
