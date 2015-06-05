using ConsoleGraphics.Graphics;
using ConsoleGraphics.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Game.GameObjects
{
    class Cell : GameObject
    {
        private static float AI_VIEW_DIST = 100f;
        private static float OVERLAP_THRESHOLD = 2f;
        private static Random _rand = new Random();

        public string Name;
        public short Color;
        public float Radius;
        public bool Virus;
        public bool Food;
        public bool Player;
        public bool Dead = false;

        private AgarioMap _map;
        private Vector2 _target;
        private Vector2 _directionBias;

        public Cell(AgarioMap map) : base(map.Space)
        {
            Parent = map;
            _map = map;
            _directionBias = Vector2.Empty;
        }

        public void Update(List<Cell> cells)
        {
            if (Virus || Food)
            {

            }
            else
            {
                List<Cell> drops = new List<Cell>();
                Cell target = null;
                Cell danger = null;

                foreach (Cell c in cells)
                {
                    float distance = c.Position.Distance(Position) - c.Radius - Radius;
                    bool overlap = distance <= -OVERLAP_THRESHOLD;
                    
                    // Collision
                    if (overlap && c.Virus)
                    {
                        // Split from virus
                    }
                    else if (overlap && c.Food)
                    {
                        // Eat food
                        c.Dead = true;
                        Radius += c.Radius;
                    }
                    else if (overlap)
                    {
                        // Eat or be eaten
                        if (c.Radius > Radius)
                        {
                            Dead = true;
                            c.Radius += Radius;
                        }
                        else
                        {
                            c.Dead = true;
                            Radius += c.Radius; // TODO: Smoothly grow
                        }
                    }
                    else if (!Player && distance < AI_VIEW_DIST && c.Radius < Radius)
                    {
                        // Targeting
                        if (target == null || target.Radius < c.Radius)
                            target = c;
                    }
                    else if (!Player && distance < AI_VIEW_DIST && c.Radius > Radius)
                    {
                        // Danger
                        if (danger == null || danger.Radius < c.Radius)
                            danger = c;
                    }
                }

                if (!Player && !Dead)
                {
                    // Clear the internal target if we've reached it
                    if (_target.Distance(Position) < 4) _target = Vector2.Empty;

                    if (target == null && danger == null && _target == Vector2.Empty)
                    {
                        // Linger
                        _target = Vector2.Random(0, _map.Width, 0, _map.Height);
                    }
                    else if (target == null || (target != null && danger != null))
                    {
                        // Run away
                        if (_rand.Next(0, 100) == 0) // Fluctuate direction
                            _directionBias = Vector2.Random(0.5f, 1.5f, 0.5f, 1.5f);
                        _target = Position + ((Position - danger.Position) + _directionBias).Normalize();
                    }
                    else if (danger == null)
                    {
                        // Attack
                        _target = target.Position;
                    }
                    Transform(_target, 0.01f);
                }
            }
        }

        public override void Draw()
        {
            Renderer.FillCircle(this, X, Y, Radius - 1f, '░', Color);
            Renderer.DrawCircle(this, X, Y, Radius, ' ', Color);
        }
    }
}
