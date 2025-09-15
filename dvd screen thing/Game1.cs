using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DirectWrite;
using System;
using System.Diagnostics;

namespace dvd_screen_thing
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font1;
        private SpriteFont _font2;
        private Texture2D _logo;
        private Texture2D _diamond;
        private Texture2D _gold;

        private bool _hitEdgeRight = true;
        private bool _hitEdgeTop = false;
        private bool _isDiamondShown = true;
        private bool _isGoldShown = false;
        private bool _gameOver = false;
        private bool _isEndShown = false;
        private bool _isInputLocked = false;

        private int _logoXPos;
        private int _logoYPos;
        private int _diamondXPos = 500;
        private int _diamondYPos = 500;
        private int _goldXPos;
        private int _goldYPos;
        private int _scoreCount = 0;

        private double _gameTimeRemaining = 65;

        private Random _diamondLocationX = new Random();
        private Random _diamondLocationY = new Random();
        private Random _goldLocationX = new Random();
        private Random _goldLocationY = new Random();

        Vector2 _scorePos;
        Vector2 _endScreenPos = new Vector2(650, 300);
        Vector2 _timerPos = new Vector2(0,0);
        

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

            _font1 = Content.Load<SpriteFont>("ScoreFont");
            _font2 = Content.Load<SpriteFont>("TimerFont");

            _logo = Content.Load<Texture2D>("1200px-DVD_VIDEO_logo");
            _diamond = Content.Load<Texture2D>("Diamond");
            _gold = Content.Load<Texture2D>("Gold");

            Viewport viewport = _graphics.GraphicsDevice.Viewport;

            _scorePos = new Vector2(1400, 100);
        }

        protected override void Update(GameTime gameTime)
        {
           

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _gameTimeRemaining -= gameTime.ElapsedGameTime.TotalSeconds;

            if(_gameTimeRemaining < 5)
            {
                _isInputLocked = true;
                _isEndShown = true;
            }
            if(_gameTimeRemaining < -5)
            {
                Exit();
            }

            if(_isInputLocked == true)
            {
                return;
            }

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

            if(_gameTimeRemaining < 30 && _gameTimeRemaining > 29)
            {
                _isGoldShown = true;
            }
            //diamond hit
            if(_logoXPos < _diamondXPos + 50 &&
               _logoXPos + 120 > _diamondXPos &&
               _logoYPos < _diamondYPos + 50 &&
               _logoYPos + 80 > _diamondYPos)
            {
                _isDiamondShown = false;
                _diamondXPos = _diamondLocationX.Next(0, _graphics.PreferredBackBufferWidth - 50);
                _diamondYPos = _diamondLocationY.Next(0, _graphics.PreferredBackBufferHeight - 50);
                _scoreCount = _scoreCount + 50;
            }

            //gold hit
            if(_logoXPos < _goldXPos + 50 &&
               _logoXPos + 120 > _goldXPos &&
               _logoYPos < _goldYPos + 50 &&
               _logoYPos + 80 > _goldYPos)
            {
                _isGoldShown = false;
                _goldXPos = _goldLocationX.Next(0, _graphics.PreferredBackBufferWidth - 50);
                _goldYPos = _goldLocationY.Next(0, _graphics.PreferredBackBufferHeight - 50);
                _scoreCount = _scoreCount + 25;
                
                _isGoldShown = true;
            }

           
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_logo, new Rectangle(_logoXPos, _logoYPos, 120, 80), Color.White);
            if(_isDiamondShown)
            {
                _spriteBatch.Draw(_diamond, new Rectangle(_diamondXPos, _diamondYPos, 50, 50), Color.White);
            }
            if (_isGoldShown)
            {
                _spriteBatch.Draw(_gold, new Rectangle(_goldXPos, _goldYPos, 50, 50), Color.White);
            }
            string score = $"Score: {_scoreCount}";
            string theEnd = "Game Over";

            int timer = (int)Math.Ceiling(_gameTimeRemaining);
            timer = timer - 5;
            string time = $"{timer}";
            

            Vector2 FontOrigin = _font1.MeasureString(score) / 2;

            _spriteBatch.DrawString(_font2, time, _timerPos, Color.White);

            _spriteBatch.DrawString(_font1, score, _scorePos, Color.Blue, 0, FontOrigin, 1f, SpriteEffects.None, 0.5f);
            if (_isEndShown)
            {
                _spriteBatch.DrawString(_font1, theEnd, _endScreenPos, Color.Red);
            }
            

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
