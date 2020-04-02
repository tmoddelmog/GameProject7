using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter
{
    public class StaticSprite : ISprite
    {
        public Vector2 Position = Vector2.Zero;
        Texture2D texture;

        public StaticSprite(Texture2D t)
        {
            this.texture = t;
        }

        public StaticSprite(Texture2D t, Vector2 p)
        {
            this.texture = t;
            this.Position = p;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
