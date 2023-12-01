using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FighterAndFish
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //ToolBar
        private Form1 _myForm;

        // Shader-related fields
        Effect spaceFighterShader;
        Effect postProcessShader;
        //world , view, and projection matrices
        private Vector3 _cameraPosition = new Vector3(0, 40, 40);
        private Matrix _view = Matrix.Identity;
        private Matrix _projection = Matrix.Identity;


        //Skybox
        private SkyBox _skyBox;
        private float _angle = 0;
        private float _distance = 20;

        private Model spaceFighter;
        private List<Models> spaceFighters;
        private Texture2D spaceFighterTexture;
        private Texture2D spaceFighterNormal;
        private SpriteFont infoFont;

        //SwitchScenes
        bool isSpaceFighterMaps=false;
        bool isSpaceScene = false;
        bool PostProcessing = false;

        // Shader-related fields
        bool enableDiffuse;
        bool enableSpecular;
        bool enableNormal;
        bool enableWireframe;
        float time;

        //Space-related fields
        private bool addButtonClicked = false;


        // Post-processing fields
        RenderTarget2D sceneRenderTarget;
        bool enablePostProcessing;
        bool enableBlackAndWhite;
        bool enableUnderwater;
        float frequency;
        float amplitude;
        bool tintBlue;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //initialize the world matrix
            _view = Matrix.CreateLookAt(_cameraPosition, Vector3.Zero, Vector3.UnitY);
            _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.1f, 1000f);
            spaceFighters = new List<Models>();

            _myForm= new Form1(this);
            _myForm.Show();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _skyBox = new SkyBox("SkyBox", Content);

            //load the font
            infoFont = Content.Load<SpriteFont>("MyFont");

            // Load the shader effects
            spaceFighterShader = Content.Load<Effect>("FighterShader");
            //postProcessShader = Content.Load<Effect>("PostProcessShader");

            // Load space fighter model, textures, etc.
            spaceFighter =Content.Load<Model>("Fighter");
            spaceFighterTexture = Content.Load<Texture2D>("FighterDiffuse");
            spaceFighterNormal = Content.Load<Texture2D>("FighterNormal");

            // Initialize RenderTarget for post-processing
            sceneRenderTarget = new RenderTarget2D(GraphicsDevice, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

    
            _angle += 0.01f;
            _cameraPosition = _distance * new Vector3((float)Math.Sin(_angle), 0, (float)Math.Cos(_angle));
            _view = Matrix.CreateLookAt(_cameraPosition, Vector3.Zero, Vector3.UnitY);

            // Handle button clicks and checkbox toggles
            // For example, if a checkbox for the diffuse map is checked, set enableDiffuse to true

            // Handle post-processing effect toggles
            // For example, if a checkbox for black and white effect is checked, set enableBlackAndWhite to true


            if (addButtonClicked)
            {
                // Calculate the position 100 units in front of the camera
                Vector3 fighterPosition = _cameraPosition + Vector3.Forward * 100; // Adjust direction as needed

                // Create a new space fighter instance
                Models newFighter = new Models(spaceFighter, spaceFighterTexture,spaceFighterNormal, fighterPosition, 1.0f);
                spaceFighters.Add(newFighter);
                // Reset the flag
                addButtonClicked = false;
            }

            // Update time for post-processing effects
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;

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
            #region PostProcessing
            // Set the render target to apply post-processing
            GraphicsDevice.SetRenderTarget(sceneRenderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            #endregion PostProcessing
            #region DrawSkyBox
            // Set depth stencil state for 3D rendering
            GraphicsDevice.DepthStencilState = DepthStencilState.None;
            _skyBox.Draw(_view, _projection, _cameraPosition);
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            #endregion DrawSkyBox
            // Render each space fighter
            foreach (var fighter in spaceFighters)
            {

                fighter.Render(_view, _projection, _cameraPosition);
            }


            // Revert to the main back buffer
            GraphicsDevice.SetRenderTarget(null);


            // Apply post-processing if enabled
            if (enablePostProcessing)
            {
                _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
                postProcessShader.Parameters["time"].SetValue(time);
                // Set other post-processing parameters...
                postProcessShader.CurrentTechnique.Passes[0].Apply();
                _spriteBatch.Draw(sceneRenderTarget, GraphicsDevice.Viewport.Bounds, Color.White);
                _spriteBatch.End();
            }


            _spriteBatch.Begin();
            // Only draw the information for the last space fighter if any exist
            if (spaceFighters.Any())
            {
                Models lastFighter = spaceFighters.Last();
                Matrix transformMatrix = lastFighter.GetTransform();
                // Decompose the matrix into scale, rotation (as a quaternion), and translation

                // Decompose the world transformation matrix
                transformMatrix.Decompose(out Vector3 scale, out Quaternion rotationQuat, out Vector3 position);

                // Convert rotation quaternion to Euler angles (in degrees)
                Vector3 rotation = QuaternionToEuler(rotationQuat);


                string infoText = $"Space Fighter\n" +
                                  $"Position: {position}\n" +
                                  $"Rotation: {rotation}\n" + // Assuming you've converted Quaternion to Euler angles
                                  $"Scale: {scale}\n" +
                                  $"Camera Position: {_cameraPosition}";

                // Draw the formatted text
                _spriteBatch.DrawString(infoFont, infoText, new Vector2(10, 10), Color.LimeGreen);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }


        /// <summary>
        /// something to convert quaternion to euler angles
        /// </summary>
        /// <param name="quat"></param>
        /// <returns></returns>
        private Vector3 QuaternionToEuler(Quaternion quat)
        {
            return new Vector3(
                MathHelper.ToDegrees((float)Math.Atan2(2f * (quat.X * quat.W - quat.Y * quat.Z), 1f - 2f * (quat.X * quat.X + quat.Z * quat.Z))),
                MathHelper.ToDegrees((float)Math.Asin(2f * (quat.X * quat.Z + quat.Y * quat.W))),
                MathHelper.ToDegrees((float)Math.Atan2(2f * (quat.Z * quat.W - quat.X * quat.Y), 1f - 2f * (quat.Y * quat.Y + quat.Z * quat.Z)))
            );
        }


        /// <summary>
        /// for switching scenes
        /// </summary>
        /// <param name="option"></param>
        /// <param name="enabled"></param>
        public void SetShaderOption(string option, bool enabled)
        {
            switch (option)
            {
                case "Diffuse":
                    enableDiffuse = enabled;
                    break;
                case "Specular":
                    enableSpecular = enabled;
                    break;
                case "Normal":
                    enableNormal = enabled;
                    break;
                case "Wireframe":
                    GraphicsDevice.RasterizerState = enabled ? new RasterizerState { FillMode = FillMode.WireFrame } : new RasterizerState { FillMode = FillMode.Solid };
                    break;
            }
        }

        /// <summary>
        /// for adding new space fighter
        /// </summary>
        public void AddNewSpaceFighter()
        {
            
            addButtonClicked = true; // You already have this flag in your update method.
        }






    }
}