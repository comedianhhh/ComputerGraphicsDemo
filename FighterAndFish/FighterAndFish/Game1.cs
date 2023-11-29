using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FighterAndFish
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Vector3 _cameraPosition = new Vector3(0, 40, 40);
        private Matrix _view = Matrix.Identity;
        private Matrix _projection = Matrix.Identity;

        private SkyBox _skyBox;
        private float _angle = 0;
        private float _distance = 20;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _view = Matrix.CreateLookAt(_cameraPosition, Vector3.Zero, Vector3.UnitY);
            _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.1f, 1000f);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _skyBox = new SkyBox("SkyBox", Content);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

    
            _angle += 0.01f;
            _cameraPosition = _distance * new Vector3((float)Math.Sin(_angle), 0, (float)Math.Cos(_angle));
            _view = Matrix.CreateLookAt(_cameraPosition, Vector3.Zero, Vector3.UnitY);

      
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            #region ConfigureDevice
            GraphicsDevice.Clear(Color.Black);
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;
            #endregion ConfigureDevice

            GraphicsDevice.DepthStencilState = DepthStencilState.None;
            _skyBox.Draw(_view, _projection, _cameraPosition);
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;




            base.Draw(gameTime);
        }
    }
}