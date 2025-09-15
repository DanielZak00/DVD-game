using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace dvd_screen_thing
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _logo;
        private Texture2D _item;
        private int _logoXPos = 0;
        private int _logoYPos = 0;
        private bool _hitEdgeRight = true;
        private bool _hitEdgeTop = false;
        private int _diamondXPos = 500;
        private int _diamondYPos = 500;
        private bool _isDiamondShown = true;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;

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

            //X axis
            if (_logoXPos >= 1480)
            {
                _logoXPos -= 8;
            }
            else if(_logoXPos <= 0)
            {
                _logoXPos += 8;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _logoXPos += 8; 
            }

            else if(Keyboard.GetState().IsKeyDown(Keys.Left)) //does that
            {
                _logoXPos -= 8;
            }

            //Y axis
            if (_logoYPos >= 800)
            {
                _logoYPos -= 8;
            }
            else if(_logoYPos <= 0)
            {
                _logoYPos += 8;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.Up)))
            {
                _logoYPos -= 8;
            }
            else if ((Keyboard.GetState().IsKeyDown(Keys.Down)))
            {
                _logoYPos += 8;
            }
            if (_logoXPos < _diamondXPos + 80 &&
                _logoXPos + 120 > _diamondXPos &&
                _logoYPos < _diamondYPos + 80 &&
                _logoYPos + 80 > _diamondYPos)
            {
                _isDiamondShown = false; 
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
                _spriteBatch.Draw(_item, new Rectangle(_diamondXPos, _diamondYPos, 80, 80), Color.White);

            }
            //hello

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
