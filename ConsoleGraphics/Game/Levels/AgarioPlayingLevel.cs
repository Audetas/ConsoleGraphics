using ConsoleGraphics.Events;
using ConsoleGraphics.Graphics;
using ConsoleGraphics.Net;
using ConsoleGraphics.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Game.Levels
{
    public class AgarioPlayingLevel : Level
    {
        private WebSocketWrapper _socket;
        private bool _connected = false;

        public AgarioPlayingLevel()
        {
            Space = new Space(this);
            BeginConnect();
        }

        private async void BeginConnect()
        {
            string ip = await ServerData.ResolveServer(ServerData.Servers[0]);
            Uri serverURI = new Uri(string.Format("ws://{0}", ip));
            _socket = new WebSocketWrapper(string.Format("ws://{0}", ip));
            _socket.OnConnect(Connect);
            _socket.OnMessage(Message);
            _socket.OnDisconnect(Disconnect);

            _socket.Connect();
        }

        public void Connect(WebSocketWrapper wrapper)
        {
            byte[] handshake = new byte[5];
            handshake[0] = 255;
            handshake.SetUInt32(1, 1);

            wrapper.SendMessage(handshake);

            _connected = true;
        }

        public void Disconnect(WebSocketWrapper wrapper)
        {
            _connected = false;
        }

        public static void Message(byte[] s, WebSocketWrapper wrapper)
        {
            using (BinaryReader b = new BinaryReader(new MemoryStream(s)))
            {
                byte id = b.ReadByte();
            }
        }
    }
}
