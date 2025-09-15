using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace dvd_screen_thing
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _logo;
        private Texture2D _item;
        private int _logoXPos;
        private int _logoYPos;
        private bool _hitEdgeRight = true;
        private bool _hitEdgeTop = false;
        private int _diamondXPos = 500;
        private int _diamondYPos = 500;
        private bool _isDiamondShown = true;
        private int _diamondHitCount = 0;
        private Random _diamondRandX = new Random();
        private Random _diamondRandY = new Random();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;

            _logoXPos = _graphics.PreferredBackBufferWidth / 2;
            _logoYPos = _graphics.PreferredBackBufferHeight / 2;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _logo = Content.Load<Texture2D>("1200px-DVD_VIDEO_logo");
            _item = Content.Load<Texture2D>("Diamond");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _isDiamondShown = true;

            //X axis
            if(Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if(_logoXPos + 120 <= _graphics.PreferredBackBufferWidth)
                {
                    _logoXPos += 8;

                }
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (_logoXPos >= 0)
                {
                    _logoXPos -= 8;

                }
            }

            //Y axis
            if((Keyboard.GetState().IsKeyDown(Keys.Up)) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (_logoYPos >= 0)
                {
                    _logoYPos -= 8;

                }
            }
            if((Keyboard.GetState().IsKeyDown(Keys.Down)) || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (_logoYPos + 80 <= _graphics.PreferredBackBufferHeight)
                {
                    _logoYPos += 8;

                }
            }

            //diamond hit
            if(_logoXPos < _diamondXPos + 50 &&
               _logoXPos + 120 > _diamondXPos &&
               _logoYPos < _diamondYPos + 50 &&
               _logoYPos + 80 > _diamondYPos)
            {
                _isDiamondShown = false;
                _diamondXPos = _diamondRandX.Next(0, _graphics.PreferredBackBufferWidth);
                _diamondYPos = _diamondRandY.Next(0, _graphics.PreferredBackBufferHeight);
                _diamondHitCount = _diamondHitCount + 50;
                Debug.WriteLine($"Your score is: {_diamondHitCount}");
            }

           
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_logo, new Rectangle(_logoXPos, _logoYPos, 120, 80), Color.White);
            if(_isDiamondShown)
            {
                _spriteBatch.Draw(_item, new Rectangle(_diamondXPos, _diamondYPos, 50, 50), Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
