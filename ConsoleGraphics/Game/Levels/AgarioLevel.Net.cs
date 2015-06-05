using ConsoleGraphics.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace ConsoleGraphics.Game.Levels
{
    public partial class AgarioLevel
    {
        private WebSocket _socket;
        private byte _latestMessage = 0;

        private async void BeginConnect()
        {
            string ip = await ServerData.ResolveServer((Server)_serverList.Items[_serverList.SelectedIndex]);
            _socket = new WebSocket(new Uri(string.Format("ws://{0}", ip)).ToString());
            _socket.OnMessage += _socket_OnMessage;
            _socket.OnOpen += _socket_OnOpen;
            _socket.OnError += _socket_OnError;
            _socket.OnClose += _socket_OnClose;
            _socket.Connect();
        }

        void _socket_OnClose(object sender, CloseEventArgs e)
        {
            throw new NotImplementedException();
        }

        void _socket_OnError(object sender, WebSocketSharp.ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        void _socket_OnOpen(object sender, EventArgs e)
        {
            byte[] handshake = new byte[5];
            handshake[0] = 255;
            handshake.SetUInt32(1, 1);
            _socket.Send(handshake);
        }

        void _socket_OnMessage(object sender, MessageEventArgs e)
        {
            using (BinaryReader r = new BinaryReader(new MemoryStream(e.RawData)))
            {
                byte id = _latestMessage = r.ReadByte();

                switch ((ServerPacket)id)
                {
                    case ServerPacket.PLAYER: break;
                    case ServerPacket.BORDER: break;
                    case ServerPacket.TEAMBOARD:
                    case ServerPacket.FFABOARD:
                    {
                        if (_leaderBoard.Items.Count > 0) return;
                        _leaderBoard.Items.Clear();
                        uint playerCount = r.ReadUInt32();
                        for (int i = 0; i < playerCount; ++i)
                        {
                            uint playerId = r.ReadUInt32();
                            string playerName = "";

                            while (true)
                            {
                                char character = (char)r.ReadInt16();
                                if (character == 0)
                                    break;
                                playerName += character;
                            }

                            _leaderBoard.Items.Add((i + 1) + ". " + playerName);
                        }
                        break;
                    }
                    case ServerPacket.CLEAR: break;
                    case ServerPacket.ADD: break;
                    case ServerPacket.UPDATE:
                    {
                        break;
                    }
                    default: break;
                }
            }
        }
    }
}
