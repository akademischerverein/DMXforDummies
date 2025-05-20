using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DmxLib;

namespace DMXforDummies.Models
{
    public class AvSink : ISink
    {
        private readonly byte[] _values = new byte[512];
        private readonly IPEndPoint _server;
        private readonly Socket _sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        public AvSink(string ip, int port)
        {
            _server = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        public void Commit()
        {
            _sock.SendTo(_values, _server);
        }
        public void Update(Universe universe, byte[] values)
        {
            for (var i = 0; i < 512; i++)
            {
                _values[i] = values[i];
            }
        }
    }
}
