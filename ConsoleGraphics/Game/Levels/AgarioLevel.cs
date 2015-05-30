using ConsoleGraphics.Events;
using ConsoleGraphics.Game.GameObjects;
using ConsoleGraphics.Game.UI;
using ConsoleGraphics.Graphics;
using ConsoleGraphics.Net;
using ConsoleGraphics.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Game.Levels
{
    public partial class AgarioLevel : Level
    {
        private WebSocketWrapper _socket;
        private bool _connected = false;

        private Window _menu;
        private ListBox _serverList;
        private Button _playButton;
        private Button _changesButton;
        private Button _siteButton;
        private Label _agarioLogo;
        private bool _direction = true;

        private AgarioMap _map;
        private ListBox _leaderBoard;

        public AgarioLevel()
        {
            Space = new Space(this);
            ServerData.LoadServers();

            _menu = new Window(Space, Style.Default);
            _menu.Size = new Vector2(40, Renderer.Height + 2);
            _menu.Position = new Vector2(-1, -1);
            _menu.Style.Text = "";
            _menu.Style.BackColor = (short)ForeGround.Black | (short)BackGround.Gray;

            _playButton = new Button(_menu, Style.Default);
            _playButton.Style.Text = "Play";
            _playButton.Size = new Vector2(_menu.Width, 10);
            _playButton.Y = _menu.Height - _playButton.Height - 2;

            _serverList = new ListBox(_menu, Style.Default);
            _serverList.Size = new Vector2(_menu.Width, ServerData.Servers.Count + 3);
            _serverList.CenterHorizontal();
            _serverList.Y = _playButton.LocalY - _serverList.Height - 1;
            _serverList.Items.AddRange(ServerData.Servers);
            _serverList.SelectedIndex = 0;

            _siteButton = new Button(_menu, Style.Default);
            _siteButton.Style.Text = "Official Site";
            _siteButton.Size = new Vector2(_menu.Width, 4);
            _siteButton.Y = 1;

            _changesButton = new Button(_menu, Style.Default);
            _changesButton.Style.Text = "Change Log";
            _changesButton.Size = new Vector2(_menu.Width, 4);
            _changesButton.Y = _siteButton.LocalY + _siteButton.Height + 1;

            _agarioLogo = new Label(_menu, "AGAR.STD.IO ");
            _agarioLogo.CenterHorizontal();
            _agarioLogo.CenterVertical();
            _agarioLogo.X += Space.Camera.Width / 2f;

            _map = new AgarioMap(Space);
            _map.Size = new Vector2(200, 200);

            _leaderBoard = new ListBox(Space, Style.Default);
            _leaderBoard.Size = new Vector2(20, 13);
            _leaderBoard.CenterVertical();
            _leaderBoard.X = Renderer.Width - _leaderBoard.Width;
            _leaderBoard.Style.Hidden = true;
        }

        public override void Update()
        {
            _playButton.Style.Enabled = _serverList.SelectedIndex != -1;
            // Logo rotation
            _agarioLogo.Rotation += _direction ? 0.003f : -0.003f;
            if      ( _direction && _agarioLogo.Rotation > 0.3f)  _direction = false;
            else if (!_direction && _agarioLogo.Rotation < -0.3f) _direction = true;
            // Play button
            if (_playButton.MouseDown)
            {
                _menu.Style.Hidden = true;
                _leaderBoard.Style.Hidden = false;
                BeginConnect();
            }

            if (_siteButton.MouseDown) Process.Start("http://agar.io/");
            if (_changesButton.MouseDown) Process.Start("http://agar.io/changelog.txt");

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            //Renderer.FillCircle(_menu, 20, 20, 10, '░');
            //Renderer.DrawCircle(_menu, 20, 20, 10, ' ');
            Renderer.Draw("FPS: " + Renderer.FPS, Renderer.Width - 8, 0, _agarioLogo.Style.BackColor);
            Renderer.Draw(((ServerPacket)_latestMessage).ToString(), Renderer.Width - 8, 1, _agarioLogo.Style.BackColor);
            //Renderer.Draw(_map.Position.ToString() + " : " + _map.Size.ToString(), 0, 0, _agarioLogo.Style.BackColor);
        }
    }
}
