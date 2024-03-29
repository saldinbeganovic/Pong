using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.States;
using SharpDX.Direct2D1;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        private State _currentState;
        private State _nextState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Globals.Width;
            _graphics.PreferredBackBufferHeight = Globals.Height;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.Pixel = new Texture2D(GraphicsDevice, 1, 1);
            Globals.Pixel.SetData<Color>(new Color[] { Color.White });
            Globals.Font = Content.Load<SpriteFont>("Score");

            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //if (Keyboard.GetState().IsKeyDown(Keys.P))
            //{
            //}

            if (_nextState != null)
            {
                _currentState = _nextState;

                _nextState = null;
            }

            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Globals.SpriteBatch.Begin();
            // TODO: Add your drawing code here
            _currentState.Draw(gameTime);

            Globals.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}