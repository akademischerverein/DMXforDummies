using DmxLib;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DMXforDummies.Models
{
    public class SacnSink : ISink
    {
        private readonly IPEndPoint _server;
        private readonly IPEndPoint _discovery = new IPEndPoint(new IPAddress([239, 255, 250, 214]), 5568);
        private readonly Socket _sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private readonly byte[] dataPacket = new byte[638];
        private readonly byte[] discoveryPacket = new byte[122];
        private byte seqNo = 0; // idx=111

        private DateTimeOffset lastDiscovery = DateTimeOffset.MinValue;

        private static readonly Guid CID = Guid.NewGuid();

        public SacnSink(ushort universe)
        {
            _server = new IPEndPoint(new IPAddress([239, 255, (byte)(universe / 256), (byte)(universe % 256)]), 5568);
            _sock.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 16);

#if DEBUG
            var sourceName = Encoding.UTF8.GetBytes($"DMX for Dummies {ThisAssembly.AssemblyInformationalVersion}");
#else
            var sourceName = Encoding.UTF8.GetBytes($"DMX for Dummies {ThisAssembly.AssemblyInformationalVersion.Substring(0, ThisAssembly.AssemblyInformationalVersion.IndexOf("+"))}");
#endif

            dataPacket[1] = discoveryPacket[1] = 0x10;
            dataPacket[4] = discoveryPacket[4] = 0x41;
            dataPacket[5] = discoveryPacket[5] = 0x53;
            dataPacket[6] = discoveryPacket[6] = 0x43;
            dataPacket[7] = discoveryPacket[7] = 0x2d;
            dataPacket[8] = discoveryPacket[8] = 0x45;
            dataPacket[9] = discoveryPacket[9] = 0x31;
            dataPacket[10] = discoveryPacket[10] = 0x2e;
            dataPacket[11] = discoveryPacket[11] = 0x31;
            dataPacket[12] = discoveryPacket[12] = 0x37;
            dataPacket[16] = 0x70 | 622 >> 8;
            dataPacket[17] = 622 & 0xff;
            discoveryPacket[16] = 0x70;
            discoveryPacket[17] = 106;
            dataPacket[21] = 4;
            discoveryPacket[21] = 8;

            CID.ToByteArray().CopyTo(dataPacket, 22);
            CID.ToByteArray().CopyTo(discoveryPacket, 22);

            dataPacket[38] = 0x70 | 600 >> 8;
            dataPacket[39] = 600 & 0xff;
            dataPacket[43] = 2;
            sourceName.CopyTo(dataPacket, 44);
            dataPacket[108] = 100;
            dataPacket[113] = (byte)(universe / 256);
            dataPacket[114] = (byte)(universe % 256);
            dataPacket[115] = 0x70 | 523 >> 8;
            dataPacket[116] = 523 & 0xff;
            dataPacket[117] = 0x02;
            dataPacket[118] = 0xa1;
            dataPacket[122] = 0x1;
            dataPacket[123] = 0x2;
            dataPacket[124] = 0x1;

            discoveryPacket[38] = 0x70;
            discoveryPacket[39] = 84 & 0xff;
            discoveryPacket[43] = 2;
            sourceName.CopyTo(discoveryPacket, 44);
            discoveryPacket[112] = 0x70;
            discoveryPacket[113] = 10;
            discoveryPacket[117] = 1;
            discoveryPacket[120] = (byte)(universe / 256);
            discoveryPacket[121] = (byte)(universe % 256);
        }

        public void Commit()
        {
            dataPacket[111] = seqNo++;
            _sock.SendTo(dataPacket, _server);

            if ((DateTimeOffset.UtcNow - lastDiscovery).TotalSeconds > 9.9)
            {
                lastDiscovery = DateTimeOffset.UtcNow;
                _sock.SendTo(discoveryPacket, _discovery);
            }
        }
        public void Update(Universe universe, byte[] values)
        {
            for (var i = 0; i < 512; i++)
            {
                values.CopyTo(dataPacket, 126);
            }
        }
    }
}
