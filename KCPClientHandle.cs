﻿using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Net.Sockets.Kcp;

namespace KCPTransportLayer
{
    public class KCPClientHandle : IKcpCallback
    {
        private Socket socket;
        private EndPoint endPoint;
        private byte[] tempBytes;

        public KCPClientHandle(Socket socket, EndPoint endPoint)
        {
            this.socket = socket;
            this.endPoint = endPoint;
        }

        public IMemoryOwner<byte> RentBuffer(int length)
        {
            return null;
        }

        public void Output(IMemoryOwner<byte> buffer, int avalidLength)
        {
            using (buffer)
            {
                tempBytes = buffer.Memory.Slice(0, avalidLength).ToArray();
                // TODO: Handle errors
                socket.SendTo(tempBytes, SocketFlags.None, endPoint);
            }
        }
    }
}
