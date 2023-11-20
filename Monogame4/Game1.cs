using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Monogame4
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D BombTexture;
        Texture2D VaultDoorTexture;
        SpriteFont timeFont;
        Texture2D Building1Texture;
        Texture2D Building2Texture;
        Texture2D Building3Texture;
        Texture2D Building4Texture;
        Texture2D ExplosionTexture;
        SoundEffect FallSound;
        SoundEffect BombSound;
        
        MouseState mouseState;

        bool defused = false;
        float seconds;
        float startTime;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ExplosionTexture = Content.Load<Texture2D>("Explosion");
            BombTexture = Content.Load<Texture2D>("bomb");
            VaultDoorTexture = Content.Load<Texture2D>("vaultdoor");
            Building1Texture = Content.Load<Texture2D>("building1");
            Building2Texture = Content.Load<Texture2D>("building2");
            Building3Texture = Content.Load<Texture2D>("building3");
            Building4Texture = Content.Load<Texture2D>("building4");
            BombSound = Content.Load<SoundEffect>("bombSound");
            FallSound = Content.Load<SoundEffect>("fallSound");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            timeFont = Content.Load<SpriteFont>("time");
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }
        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if ((mouseState.LeftButton == ButtonState.Pressed) & (!defused))
            {
                defused = true;
            }
            if (!defused) { seconds = (float)gameTime.TotalGameTime.TotalSeconds; }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (seconds < 15) 
            {
                startTime = 15 - seconds;
            }
            if (Math.Round(seconds, 1) == 11.0) { FallSound.Play(); }
            if (Math.Round(seconds,1) == 15.0) { BombSound.Play(); }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            if (seconds >= 16.5) { _spriteBatch.Draw(Building4Texture, new Rectangle(0, 0, 800, 500), Color.White); }
            else if (seconds >= 16) { _spriteBatch.Draw(Building3Texture, new Rectangle(0, 0, 800, 500), Color.White); ; }
            else if (seconds >= 15.5) { _spriteBatch.Draw(Building2Texture, new Rectangle(0, 0, 800, 500), Color.White); ; }
            else if (seconds >= 15) { _spriteBatch.Draw(Building1Texture, new Rectangle(0, 0, 800, 500), Color.White); }
            else
            {
                _spriteBatch.Draw(ExplosionTexture, new Rectangle(10, 10, 0, 0), Color.White);
                _spriteBatch.Draw(VaultDoorTexture, new Rectangle(0, 0, 800, 500), Color.White);
                _spriteBatch.Draw(BombTexture, new Rectangle(50, 50, 700, 400), Color.White);
                _spriteBatch.DrawString(timeFont, startTime.ToString("00.0"), new Vector2(270, 200), Color.Black);
            }
           
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}