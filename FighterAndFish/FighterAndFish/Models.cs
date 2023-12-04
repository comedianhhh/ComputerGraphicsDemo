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


        //properties for shader flags
        public bool UseDiffuseMap { get; set; }
        public bool UseNormalMap { get; set; }
        public bool UseSpecularHighlights { get; set; }

        //Texturing
        public Texture2D Texture { get; set; }
        public float WrapAmount { get; set; }

        //property for the normal map texture
        public Texture2D NormalMap { get; set; }

        //Lighting variables
        public Vector3 DiffuseColor { get; set; }
        public float SpecularPower { get; set; }
        public Vector3 SpecularColor { get; set; }

        public Vector3 LightPosition { get; set; }
        public float LightAttenuation { get; set; }
        public float LightFalloff { get; set; }
        public float ConeAngle { get; set; }
        public float time { get; set; }

     
        public Vector3 LightColor { get; set; }
        public Vector3 LightDirection { get; set; }


        public Models(Model _mesh,
                       Texture2D _texture,
                       Texture2D _normalMap,
                       Vector3 _position,
                       float _scale)
        {
            Mesh = _mesh;
            Texture = _texture;
            NormalMap = _normalMap;
            Translation = Matrix.CreateTranslation(_position);
            Rotation = Matrix.Identity;
            Scale = Matrix.CreateScale(_scale);
            DiffuseColor = new Vector3(1.0f, 1.0f, 1.0f);
            SpecularPower = 4.0f;
            SpecularColor = new Vector3(1.0f, 1.0f, 1.0f);

            LightPosition = new Vector3(0, 50, 20);
            LightAttenuation = 5000;
            LightFalloff = 2.0f;
            ConeAngle = 45.0f;


            LightColor = new Vector3(1.0f, 1.0f, 1.0f); // White light
            LightDirection = new Vector3(0.0f, -2.0f, 1.0f); // Direction facing downwards
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
            // Calculate the World-View-Projection Matrix
            Matrix worldMatrix = GetTransform();
            Matrix worldViewProjection = worldMatrix * _view * _projection;


            // Set shader parameters for transformations
            Shader.Parameters["World"].SetValue(worldMatrix);
            Shader.Parameters["WorldViewProjection"].SetValue(worldViewProjection);


            // Set the texture
            if (Texture != null && UseDiffuseMap) // Assuming useDiffuseMap is a class property
            {
                Shader.Parameters["Texture"].SetValue(Texture);
            }

            // Set the normal map
            if (NormalMap != null && UseNormalMap) // Assuming NormalMap is a class property
            {
                Shader.Parameters["NormalMap"].SetValue(NormalMap);
            }

            // Set camera position
            Shader.Parameters["CameraPosition"].SetValue(_cameraPosition);

            // Set lighting variables
            Shader.Parameters["DiffuseColor"].SetValue(DiffuseColor);
            Shader.Parameters["SpecularPower"].SetValue(SpecularPower);
            Shader.Parameters["SpecularColor"].SetValue(SpecularColor);

            Shader.Parameters["LightDirection"].SetValue(LightDirection);
            Shader.Parameters["LightColor"].SetValue(LightColor);
            if (Shader.Parameters["time"] != null)
            { 
                Shader.Parameters["time"].SetValue(time);
            }
    
            // Set shader flags
            Shader.Parameters["useDiffuseMap"].SetValue(UseDiffuseMap);
            Shader.Parameters["useNormalMap"].SetValue(UseNormalMap);
            Shader.Parameters["useSpecularHighlights"].SetValue(UseSpecularHighlights);

            // Draw each mesh with the effect
            foreach (ModelMesh mesh in Mesh.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = Shader;
                }
                mesh.Draw();
            }
        }

        
    }
}
