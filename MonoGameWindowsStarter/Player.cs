using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter
{
    public delegate Vector2 Updater();

    public class Player : ISprite
    {
        Texture2D spritesheet;
        Rectangle sourceRect;
        public Vector2 Position { get; set; }
        public float Speed { get; set; } = 100;
        public BoundingRectangle Bounds => new BoundingRectangle(
                Position.X,
                Position.Y,
                sourceRect.Width,
                sourceRect.Height
            );
        public Updater UpdateDirection { get; set; }

        public Player(Texture2D spritesheet, Vector2 p, Rectangle sr)
        {
            this.spritesheet = spritesheet;
            this.Position = p;
            this.sourceRect = sr;
        }

        public void Update(GameTime gameTime)
        {
            Position += (float)gameTime.ElapsedGameTime.TotalSeconds * Speed * UpdateDirection();

            if (Position.X < 0) Position = new Vector2(800 - sourceRect.X, Position.Y);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(spritesheet, Position, sourceRect, Color.White);
        }

        public bool CollidesWith(BoundingRectangle other)
        {
            return !(this.Bounds.X > other.X + other.Width
                    || this.Bounds.X + this.Bounds.Width < other.X
                    || this.Bounds.Y > other.Y + other.Height
                    || this.Bounds.Y + this.Bounds.Height < other.Y);
        }
    }
}
