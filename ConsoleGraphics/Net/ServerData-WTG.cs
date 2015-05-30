using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.IO;
using System.Net;

namespace ConsoleGraphics.Net
{
    public enum ServerPacket : byte
    {
        UPDATE = 16,
        PLAYER = 17,
        CLEAR = 20,
        ADD = 32,
        LEADERBOARD = 49,
        BORDER = 64
    }

    public enum ClientPacket : byte
    {
        NICKNAME = 0,
        SPECTATE = 1,
        MOUSEMOVE = 16,
        SPLIT  = 17,
        QPRESSED = 18,
        QRELEASED = 19,
        EJECT = 21,
        RESET = 255
    }

    public static class ServerData
    {
        public static List<Server> Servers = new List<Server>();

        public static void LoadServers()
        {
            using (var client = new WebClient())
            {
                var serverJSON = client.DownloadString("http://m.agar.io/info");

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var servers = serializer.Deserialize<Dictionary<string, object>>(serverJSON);

                foreach (var server in (Dictionary<string, object>)servers["regions"])
                {
                    Server newServer = new Server();
                    newServer.Name = server.Key;

                    var serverData = (Dictionary<string, object>)server.Value;
                    newServer.Players = (int)serverData["numPlayers"];
                    newServer.Realms = (int)serverData["numRealms"];
                    newServer.Servers = (int)serverData["numServers"];
                    Servers.Add(newServer);
                }
            }
        }
        
        public async static Task<string> ResolveServer(Server server)
        {
            string url = "";
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string> { { server.Name, "" } };
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("http://m.agar.io/", content);
                var responseString = await response.Content.ReadAsStringAsync();
                url = responseString.Split('\n')[0];
            }

            return url;
        }
    }
}
