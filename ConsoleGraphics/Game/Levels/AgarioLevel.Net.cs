using ConsoleGraphics.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Game.Levels
{
    public partial class AgarioLevel
    {
        private byte _latestMessage = 0;
        private async void BeginConnect()
        {
            string ip = await ServerData.ResolveServer((Server)_serverList.Items[_serverList.SelectedIndex]);
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

        public void Message(byte[] s, WebSocketWrapper wrapper)
        {
            using (BinaryReader b = new BinaryReader(new MemoryStream(s)))
            {
                byte id = _latestMessage = b.ReadByte();
                switch ((ServerPacket)id)
                {
                    case ServerPacket.PLAYER: break;
                    case ServerPacket.BORDER:
                    {
                        _map.X = (float)b.ReadDouble();
                        _map.Y = (float)b.ReadDouble();
                        _map.Size.X = (float)b.ReadDouble() / 30.0f;
                        _map.Size.Y = (float)b.ReadDouble() / 30.0f;
                        break;
                    }
                    case ServerPacket.TEAMBOARD: break;
                    case ServerPacket.FFABOARD:
                    {
                        if (_leaderBoard.Items.Count > 0) return;
                        _leaderBoard.Items.Clear();
                        uint playerCount = b.ReadUInt32();
                        for (int i = 0; i < playerCount; ++i)
                        {
                            uint playerId = b.ReadUInt32();
                            string playerName = "";
                            
                            while (true)
                            {
                                char character = (char)b.ReadInt16();
                                if (character == 0)
                                    break;
                                playerName += character;
                            }
                            
                            _leaderBoard.Items.Add((i + 1) + ". " + playerName);
                        }
                        break;
                    }
                    case ServerPacket.CLEAR:
                    {
                        _map.Nodes.Clear();
                        break;
                    }
                    case ServerPacket.ADD: break;
                    case ServerPacket.UPDATE:
                    {
                        ushort drops = b.ReadUInt16();
                        for (int i = 0; i < drops; i++)
                        {
                            NodeData dropped = new NodeData(b);
                            for (int n = _map.Nodes.Count - 1; n > 0; n--)
                                if (_map.Nodes[n].Id == dropped.Id)
                                    _map.Nodes.RemoveAt(n);
                        }

                        while (true)
                        {
                            NodeData newNode = new NodeData(b);
                            if (newNode.Id == 0) break;

                            _map.Nodes.Add(newNode);
                        }
                        break;
                    }
                    default: break;
                }
            }
        }

        public void PushMessage(byte id)
        {
            if (_socket == null)
                return;

            byte[] messagePacket = new byte[1];
            messagePacket[0] = id;

            _socket.SendMessage(messagePacket);
        }
    }
}
