using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FighterAndFish
{
    class Models
    {
        public Model Mesh { get; set; }
        public Matrix Translation { get; set; }
        public Matrix Rotation { get; set; }
        public Matrix Scale { get; set; }
        public Effect Shader { get;set; }

        //Texturing
        public Texture Texture { get; set; }
        public float WrapAmount { get; set; }

        //Lighting variables
        public Vector3 DiffuseColor { get; set; }
        public float SpecularPower { get; set; }
        public Vector3 SpecularColor { get; set; }

        public Vector3 LightPosition { get; set; }
        public float LightAttenuation { get; set; }
        public float LightFalloff { get; set; }

        public float ConeAngle { get; set; }
 
        public Vector3[] LightColor { get; set; }
        public Vector3[] LightDirection { get; set; }


        public Models(Model _mesh,
                       Texture _texture,
                       Vector3 _position,
                       float _scale)
        {
            Mesh = _mesh;
            Texture = _texture;
            Translation = Matrix.CreateTranslation(_position);
            Rotation = Matrix.Identity;
            Scale = Matrix.CreateScale(_scale);
            DiffuseColor = new Vector3(1.0f, 1.0f, 1.0f);
            SpecularPower = 4.0f;
            SpecularColor = new Vector3(3.0f, 3.0f, 3.0f);

            LightPosition = new Vector3(0, 50, 20);
            LightAttenuation = 5000;
            LightFalloff = 2.0f;
            ConeAngle = 45.0f;
  

            LightColor = new Vector3[] { new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 0, 0) };
            LightDirection = new Vector3[] { new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1) };
        }

        public void SetShader(Effect _effect)
        {
            Shader = _effect;

            foreach (ModelMesh mesh in Mesh.Meshes)
            {
                foreach(ModelMeshPart meshpart in mesh.MeshParts)
                {
                    meshpart.Effect = Shader;
                }
            }
        }

        public Matrix GetTransform()
        {
            return Scale * Rotation * Translation;
        }
        public void Render(Matrix _view,
                           Matrix _projection,
                           Vector3 _cameraPosition)
        {
            Matrix worldMatrix = GetTransform();
            Matrix worldViewProjection = worldMatrix * _view * _projection;

            Shader.Parameters["World"].SetValue(worldMatrix);
            Shader.Parameters["WorldViewProjection"].SetValue(worldViewProjection);
            Shader.Parameters["Texture"].SetValue(Texture);
            Shader.Parameters["CameraPosition"].SetValue(_cameraPosition);

            // Set additional texture samplers if applicable
            // E.g., Shader.Parameters["NormalMap"].SetValue(NormalMap);

            Shader.Parameters["DiffuseColor"].SetValue(DiffuseColor);
            Shader.Parameters["SpecularPower"].SetValue(SpecularPower);
            Shader.Parameters["SpecularColor"].SetValue(SpecularColor);

            // Update lighting parameters
            Shader.Parameters["LightPosition"].SetValue(LightPosition);
            Shader.Parameters["LightAttenuation"].SetValue(LightAttenuation);
            Shader.Parameters["LightFalloff"].SetValue(LightFalloff);
            Shader.Parameters["ConeAngle"].SetValue(ConeAngle);

            Shader.Parameters["LightColor"].SetValue(LightColor);
            Shader.Parameters["LightDirection"].SetValue(LightDirection);

            // Conditional features
            // E.g., Shader.Parameters["UseNormalMap"].SetValue(useNormalMap);


            foreach (ModelMesh mesh in Mesh.Meshes)
            {
                mesh.Draw();
            }
        }
    }
}
