using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace FighterAndFish
{
    public class SkyBox
    {
        /// <summary>
        /// The skybox model, which will just be a cube
        /// </summary>
        private Model _skyBox;


        /// <summary>
        /// The skybox texture
        /// </summary>
        private TextureCube _skyBoxTexture;

        /// <summary>
        /// The skybox effect, which will be used to texture the skybox
        /// </summary>
        private Effect _skyBoxEffect;

        /// <summary>
        /// The size of the skybox
        /// </summary>
        private float _size = 20;


        /// <summary>
        /// create a new skybox
        /// </summary>
        /// <param name="_skyboxTexture"></param>
        /// <param name="_content"></param>
        public SkyBox(string _skyboxTexture, ContentManager _content)
        {
            _skyBox = _content.Load<Model>("Cube");
            _skyBoxTexture = _content.Load<TextureCube>(_skyboxTexture);
            _skyBoxEffect = _content.Load<Effect>("CubeMap");
        }
        /// <summary>
        /// Does the actual drawing of the skybox with the given view and projection matrices
        /// There is no world matrix, because we're assuming the skybox won't be moved around
        /// be moved around. 
        /// </summary>
        /// <param name="_view"></param>
        /// <param name="_projection"></param>
        /// <param name="_cameraPosition"></param>
        public void Draw(Matrix _view, Matrix _projection, Vector3 _cameraPosition)
        {
            foreach (ModelMesh mesh in _skyBox.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = _skyBoxEffect;
                    Matrix world=Matrix.CreateScale(_size) * Matrix.CreateTranslation(_cameraPosition);
                    part.Effect.Parameters["World"].SetValue(world);
                    part.Effect.Parameters["WorldViewProjection"].SetValue(world * _view * _projection);
                    part.Effect.Parameters["SkyBoxTexture"].SetValue(_skyBoxTexture);
                    part.Effect.Parameters["CameraPosition"].SetValue(_cameraPosition);
                }
                mesh.Draw();
            }
        }
    }
}
