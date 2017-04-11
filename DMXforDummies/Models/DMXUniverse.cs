using System;
using System.Net;
using System.Net.Sockets;

namespace DMXforDummies.Models
{
    public class DMXUniverse
    {
        private readonly byte[] _out = new byte[512];
        private readonly IPEndPoint _server;
        private readonly Socket _sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        public DMXUniverse(string ip, int port)
        {
            _server = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        public void Set(int ch, byte val)
        {
            _out[ch - 1] = val;
        }

        public void Commit()
        {
            _sock.SendTo(_out, _server);
        }
    }
}
