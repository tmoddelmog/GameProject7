using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player plane;
        Player[] viruses = new Player[new Random().Next(1,8)];
        Texture2D gameOverTexture;
        bool gameOver;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // city background
            var backgroundTexture = Content.Load<Texture2D>("city");
            var backgroundSprite = new StaticSprite(backgroundTexture);
            var backgroundLayer = new ParallaxLayer(this);
            backgroundLayer.Sprites.Add(backgroundSprite);
            backgroundLayer.DrawOrder = 1;
            Components.Add(backgroundLayer);

            // city midground
            var midTexture = Content.Load<Texture2D>("city2");
            var midSprite = new StaticSprite(midTexture, new Vector2(0, 240));
            var midLayer = new ParallaxLayer(this);
            midLayer.Sprites.Add(midSprite);
            midLayer.ScrollController.Speed = 40f;
            midLayer.DrawOrder = 2;
            Components.Add(midLayer);

            // plane
            plane = new Player(Content.Load<Texture2D>("plane"),
                    new Vector2(0, 240),
                    new Rectangle { X = 0, Y = 0, Width = 200, Height = 109 });
            plane.UpdateDirection = () =>
            {
                Vector2 direction = Vector2.Zero;
                var keyboard = Keyboard.GetState();
                var s = 5;
                if (keyboard.IsKeyDown(Keys.Left))  direction.X -= s;
                if (keyboard.IsKeyDown(Keys.Right)) direction.X += s;
                if (keyboard.IsKeyDown(Keys.Up))    direction.Y -= s;
                if (keyboard.IsKeyDown(Keys.Down))  direction.Y += s;
                return direction;
            };
            var planeLayer = new ParallaxLayer(this);
            planeLayer.Sprites.Add(plane);
            planeLayer.DrawOrder = 3;
            Components.Add(planeLayer);

            // viruses
            var virusLayer = new ParallaxLayer(this);
            var r = new Random();
            for (var i = 0; i < viruses.Length; i++)
            {
                viruses[i] = new Player(Content.Load<Texture2D>("virus"),
                             new Vector2(r.Next(400, 800), r.Next(480)),
                             new Rectangle { X = 0, Y = 0, Width = 34, Height = 34 });
                viruses[i].UpdateDirection = () =>
                {
                    return new Vector2(-2, 0);
                };
                virusLayer.Sprites.Add(viruses[i]);
            }
            virusLayer.DrawOrder = 4;
            Components.Add(virusLayer);

            gameOverTexture = Content.Load<Texture2D>("gameOver");
            gameOver = false;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach (Player v in viruses)
            {
                v.Update(gameTime);
                if (plane.CollidesWith(v.Bounds)) gameOver = true;
            }

            plane.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            base.Draw(gameTime);

            spriteBatch.Begin();
            if (gameOver)
            {
                spriteBatch.Draw(
                    gameOverTexture,
                    new Vector2(250, 200),
                    Color.White);
            }
            spriteBatch.End();
        }
    }
}
