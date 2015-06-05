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
        private Window _menu;
        private Checkbox _gridCheck;
        private Checkbox _massCheck;
        private ListBox _serverList;
        private Button _playButton;
        private Button _changesButton;
        private Button _siteButton;
        private Label _agarioLogo;

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

            _gridCheck = new Checkbox(_menu, Style.Default);
            _gridCheck.SetText("Show Grid");
            _gridCheck.X = 2;
            _gridCheck.Y = _changesButton.LocalY + _changesButton.Height + 2;
            _gridCheck.Style.ForeChar = '║';
            _gridCheck.Style.ForeColor = (short)ForeGround.WHITE | (short)BackGround.Gray;
            _gridCheck.Style.BackColor = (short)ForeGround.Black | (short)BackGround.Gray;
            _gridCheck.Checked = true;

            _massCheck = new Checkbox(_menu, Style.Default);
            _massCheck.SetText("Show mass");
            _massCheck.Style.ForeChar = '║';
            _massCheck.Style.ForeColor = (short)ForeGround.WHITE | (short)BackGround.Gray;
            _massCheck.Style.BackColor = (short)ForeGround.Black | (short)BackGround.Gray;
            _massCheck.Checked = true;
            _massCheck.X = _menu.Width - _gridCheck.Width - 1;
            _massCheck.Y = _changesButton.LocalY + _changesButton.Height + 2;

            _agarioLogo = new Label(_menu, Style.Default);
            _agarioLogo.Rendered = true;
            _agarioLogo.SetText("AGAR.STD.IO");
            _agarioLogo.CenterVertical();
            _agarioLogo.X = Space.Camera.Width / 2f - _agarioLogo.Width / 2f + _menu.Width / 2f;

            _map = new AgarioMap(Space);
            _map.Center = Space.Camera.Center;

            _leaderBoard = new ListBox(Space, Style.Default);
            _leaderBoard.Size = new Vector2(20, 13);
            _leaderBoard.CenterVertical();
            _leaderBoard.X = Renderer.Width - _leaderBoard.Width;
            _leaderBoard.Style.Hidden = true;
        }

        private bool _clicked = false;
        public override void Update()
        {
            _playButton.Style.Enabled = _serverList.SelectedIndex != -1;
            if (_playButton.MouseDown && !_clicked)
            {
                _clicked = true;
                _menu.Style.Hidden = true;
                _leaderBoard.Style.Hidden = false;
                BeginConnect();
            }
            _map.DrawGrid = _gridCheck.Checked;
            if (_siteButton.Clicked) Process.Start("http://agar.io/");
            if (_changesButton.Clicked) Process.Start("http://agar.io/changelog.txt");

            base.Update();
        }

        public override void Draw()
        {
            if (Keyboard.IsPressed(System.Windows.Forms.Keys.R)) _map.Rotation = 0;
            if (Keyboard.IsPressed(System.Windows.Forms.Keys.Q)) _map.Rotation -= 0.05f;
            if (Keyboard.IsPressed(System.Windows.Forms.Keys.E)) _map.Rotation += 0.05f;
            base.Draw();
            Renderer.Draw("FPS: " + Renderer.FPS, Renderer.Width - 8, 0, _agarioLogo.Style.BackColor);
            Renderer.Draw(((ServerPacket)_latestMessage).ToString(), Renderer.Width - 8, 1, _agarioLogo.Style.BackColor);
        }
    }
}
