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
        Effect BlackAndWhiteShader;
        Effect UnderwaterShader;
        //world , view, and projection matrices
        private Vector3 _cameraPosition = new Vector3(0, 20,20);
        private Matrix _view = Matrix.Identity;
        private Matrix _projection = Matrix.Identity;


        //Skybox0
        private SkyBox _skyBox;
        private float _angle = 0;
        private float _distance = 30;

        private Model spaceFighter;
        private Models MapsSpaceFighter;
        private List<Models> spaceFighters;
        private Texture2D spaceFighterTexture;
        private Texture2D spaceFighterNormal;
        private SpriteFont infoFont;

        //SwitchScenes
        bool isSpaceFighterMaps=true;
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
        private Model FishModel;
        private Texture2D FishTexture;
        private Texture2D FishNormal;
        private Models Fish;


        RenderTarget2D sceneRenderTarget;
        //bool enablePostProcessing;
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

            // Set the window size
            _graphics.PreferredBackBufferWidth = 1280; // Set this value to the desired width
            _graphics.PreferredBackBufferHeight = 960; // Set this value to the desired height

        }

        protected override void Initialize()
        {
            //initialize the world matrix
            _view = Matrix.CreateLookAt(_cameraPosition, Vector3.Zero, Vector3.UnitY);
            _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                GraphicsDevice.Viewport.AspectRatio, 0.1f, 1000f);
            spaceFighters = new List<Models>();
            sceneRenderTarget = new RenderTarget2D(GraphicsDevice, GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight,false, GraphicsDevice.PresentationParameters.BackBufferFormat,
                                                    DepthFormat.Depth24);

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
            FishModel = Content.Load<Model>("Fish");
            FishTexture = Content.Load<Texture2D>("FishDiffuse");
            FishNormal = Content.Load<Texture2D>("FishNormal");
            BlackAndWhiteShader = Content.Load<Effect>("BWPost");
            UnderwaterShader = Content.Load<Effect>("UnderWaterPost");
   


            Fish = new Models(FishModel, FishTexture, FishNormal, Vector3.Zero, 0.3f);
            Fish.SetShader(spaceFighterShader);


            MapsSpaceFighter = new Models(spaceFighter, spaceFighterTexture, spaceFighterNormal, Vector3.Zero, 0.008f);
            MapsSpaceFighter.SetShader(spaceFighterShader);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(isSpaceFighterMaps)
            {
                MapsSpaceFighter.Rotation *= Matrix.CreateRotationX(0.01f);
            }
            else if (isSpaceScene)
            {
                _angle += 0.01f;
                _cameraPosition = _distance * new Vector3((float)Math.Sin(_angle), 0, (float)Math.Cos(_angle));
                _view = Matrix.CreateLookAt(_cameraPosition, Vector3.Zero, Vector3.UnitY);
            }
            else if (PostProcessing)
            {
                time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                Fish.time=time;
          
            }
        

            if (addButtonClicked&&isSpaceScene)
            {
                // This code calculates the forward vector from the view matrix
                Matrix invertedView = Matrix.Invert(_view);
                Vector3 cameraForward = invertedView.Forward;

                // Use the camera's forward vector to position the new fighter in front of the camera
                Vector3 fighterPosition = _cameraPosition + cameraForward * 100;

                // Create a new space fighter instance
                Models newFighter = new Models(spaceFighter, spaceFighterTexture,spaceFighterNormal, fighterPosition, 0.03f);
                
                newFighter.SetShader(spaceFighterShader);
                newFighter.Rotation = Matrix.CreateRotationX(34);
                newFighter.UseDiffuseMap = true;
                newFighter.UseSpecularHighlights = true;
                newFighter.UseNormalMap = true;

                spaceFighters.Add(newFighter);
                // Reset the flag
                addButtonClicked = false;
            }

            
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


            #region DrawSkyBox
            // Set depth stencil state for 3D rendering
            GraphicsDevice.DepthStencilState = DepthStencilState.None;
            _skyBox.Draw(_view, _projection, _cameraPosition);
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            #endregion DrawSkyBox
            //MapsSpaceFighter.Render(_view, _projection, _cameraPosition);
            if (isSpaceFighterMaps)
            {

                MapsSpaceFighter.UseDiffuseMap = enableDiffuse;
                MapsSpaceFighter.UseSpecularHighlights = enableSpecular;
                MapsSpaceFighter.UseNormalMap = enableNormal;
                GraphicsDevice.RasterizerState = enableWireframe ? new RasterizerState { FillMode = FillMode.WireFrame } : new RasterizerState { FillMode = FillMode.Solid };
                MapsSpaceFighter.Render(_view, _projection, _cameraPosition);
                spaceFighters.Add(MapsSpaceFighter);

            }

            // Render each space fighter
            if(isSpaceScene)
            {
                foreach (var fighter in spaceFighters)
                {

                    fighter.Render(_view, _projection, _cameraPosition);
                }
            }
   

            #region PostProcessing
            // Apply post-processing if enabled
            if (PostProcessing)
            {
                if(!enableBlackAndWhite&&!enableUnderwater)
                {
                    Fish.UseDiffuseMap = true;
                    Fish.UseSpecularHighlights = true;
                    Fish.UseNormalMap = true;

                    Fish.Render(_view, _projection, _cameraPosition);
                }
     
                else if (enableBlackAndWhite)
                {
                    DrawSceneToTexture(sceneRenderTarget);
                    BlackAndWhiteShader.Parameters["useBlackandWite"].SetValue(true);
                    BlackAndWhiteShader.Parameters["useUnderWater"].SetValue(false);
              
                    _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                                    SamplerState.LinearClamp, DepthStencilState.Default,
                                    RasterizerState.CullNone, BlackAndWhiteShader);
                    _spriteBatch.Draw(sceneRenderTarget, Vector2.Zero, Color.White);
                    _spriteBatch.End();
                }
                else if (enableUnderwater)
                {
                    BlackAndWhiteShader.Parameters["useUnderWater"].SetValue(true);
                    BlackAndWhiteShader.Parameters["useBlackandWite"].SetValue(false);
                    BlackAndWhiteShader.Parameters["frequency"].SetValue(frequency);
                    BlackAndWhiteShader.Parameters["amplitude"].SetValue(amplitude);
                    BlackAndWhiteShader.Parameters["time"].SetValue(time);
                    BlackAndWhiteShader.Parameters["tintBlue"].SetValue(tintBlue);
                    DrawSceneToTexture(sceneRenderTarget);

                    _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                                           SamplerState.LinearClamp, DepthStencilState.Default,
                                           RasterizerState.CullNone, BlackAndWhiteShader);
                    _spriteBatch.Draw(sceneRenderTarget, Vector2.Zero, Color.White);
                    _spriteBatch.End();
                }

            }
            

            #endregion PostProcessing


            //info text
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
                                  $"Fighter Count: {spaceFighters.Count}\n" +
                                  $"Camera Position: {_cameraPosition}";

                // Draw the formatted text
                _spriteBatch.DrawString(infoFont, infoText, new Vector2(10, 10), Color.LightGoldenrodYellow);
            }
            if (PostProcessing)
            {

                Matrix transformMatrix = Fish.GetTransform();
                // Decompose the matrix into scale, rotation (as a quaternion), and translation

                // Decompose the world transformation matrix
                transformMatrix.Decompose(out Vector3 scale, out Quaternion rotationQuat, out Vector3 position);

                // Convert rotation quaternion to Euler angles (in degrees)
                Vector3 rotation = QuaternionToEuler(rotationQuat);


                string infoText = $"Magic Fish\n" +
                                  $"Position: {position}\n" +
                                  $"Rotation: {rotation}\n" + // Assuming you've converted Quaternion to Euler angles
                                  $"Scale: {scale}\n" +
                                  $"time: {Fish.time}\n" +
                                  $"amplitude: {amplitude}\n" +
                                  $"frequency: {frequency}\n" +
                                  $"enableTintblue: {tintBlue}\n" +
                                  $"Camera Position: {_cameraPosition}";

                // Draw the formatted text
                _spriteBatch.DrawString(infoFont, infoText, new Vector2(10, 10), Color.YellowGreen);
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
                    MapsSpaceFighter.UseDiffuseMap = enabled;
                    break;
                case "Specular":
                    enableSpecular = enabled;
                    MapsSpaceFighter.UseSpecularHighlights = enabled;

                    break;
                case "Normal":
                    enableNormal = enabled;
                    MapsSpaceFighter.UseNormalMap = enabled;
                    break;
                case "Wireframe":
                    enableWireframe= enabled;
                    
                    break;
            }
        }
        /// <summary>
        /// set scenes
        /// </summary>
        /// <param name="option"></param>
        /// <param name="enabled"></param>
        public void SetScene(string option, bool enabled)
        {
            switch (option)
            {
                case "SpaceFighterMaps":
                    RemoveExistingSpaceFighter();
                    isSpaceFighterMaps = enabled;
                    break;
                case "SpaceScene":
                    RemoveExistingSpaceFighter();
                    isSpaceScene = enabled;
                    break;
                case "PostProcessing":
                    RemoveExistingSpaceFighter();
                    PostProcessing = enabled;
                    _cameraPosition = new Vector3(0, 20, 20);
                    _view = Matrix.CreateLookAt(_cameraPosition, Vector3.Zero, Vector3.UnitY);
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

        /// <summary>
        /// for moving existing space fighter
        /// </summary>
        private void RemoveExistingSpaceFighter()
        {
            spaceFighters.Clear();

        }

        protected void DrawSceneToTexture(RenderTarget2D _renderTarget)
        {
            //set the render target
            GraphicsDevice.SetRenderTarget(_renderTarget);

            //Draw the scene

            Fish.UseDiffuseMap = true;
            Fish.UseSpecularHighlights = true;
            Fish.UseNormalMap = true;
            Fish.Render(_view, _projection, _cameraPosition);

            GraphicsDevice.SetRenderTarget(null);
        }

        public void SetPPValue(string option, double value)
        {
            switch (option)
            {
                case "Frequency":
                    frequency = (float)value;
                    UnderwaterShader.Parameters["frequency"].SetValue(frequency);
                    break;
                case "Amplitude":
                    amplitude =(float) value;
                    UnderwaterShader.Parameters["amplitude"].SetValue(amplitude);
                    break;
            }
        }
        public void SetPostProcessing(string option, bool enabled)
        {
            switch (option)
            {
                case "BlackAndWhite":
                    enableBlackAndWhite = enabled;
                    break;
                case "Underwater":
                    enableUnderwater = enabled;
                    break;
                case "TintBlue":
                    tintBlue = enabled;
                    break;
            }
        }



    }
}