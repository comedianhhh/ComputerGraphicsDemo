#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif


matrix WorldViewProjection;

texture Texture;

float3 DiffuseColor = float3(1, 1, 1);
float3 AmbientColor = float3(0.15f, 0.15f, 0.15f);

float SpecularPower = 4;
float3 SpecularColor = float3(0, 0, 1);

float3 CameraPosition = float3(0, 30, 30);
matrix World;


float3 LightDirection = float3(0, 0, 1);
float3 LightColor = float3(1, 1, 1);


sampler BasicTextureSampler = sampler_state
{
    texture = <Texture>;
    MinFilter = Anisotropic;
    MagFilter = Anisotropic;
    MipFilter = LINEAR;
};

texture NormalMap;
sampler NormalMapSampler = sampler_state
{
    texture = <NormalMap>;
    MinFilter = Anisotropic;
    MagFilter = Anisotropic;
    MipFilter = LINEAR;
};

//setting
bool useDiffuseMap = false;
bool useNormalMap = false;
bool useSpecularHighlights = false;

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float2 UV : TEXCOORD0;
    float3 Normal : NORMAL0;
    float3 Tangent : TANGENT0; // Add tangent for normal mapping
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float2 UV : TEXCOORD0;
    float3 Normal : NORMAL0;
    float3 Tangent : TEXCOORD1; // Pass tangent to pixel shader
    float3 ViewDirection : TEXCOORD2;
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput) 0;

    float4 worldPosition = mul(input.Position, World);
    output.Position = mul(input.Position, WorldViewProjection);
    output.UV = input.UV;
    output.Normal = normalize(mul(input.Normal, World));
    output.ViewDirection = normalize(worldPosition - CameraPosition);

    // Pass tangent to pixel shader
    output.Tangent = normalize(mul(input.Tangent, (float3x3) World));
    
	return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    
    // Normal Mapping
    float3 normal = input.Normal;
    if (useNormalMap)
    {
        float3 tangentNormal = tex2D(NormalMapSampler, input.UV).xyz * 2.0 - 1.0;
        float3 binormal = cross(input.Normal, input.Tangent);
        float3x3 TBN = float3x3(input.Tangent, binormal, input.Normal);
        normal = mul(tangentNormal, TBN);
    }

    // Lambertian Lighting
    float3 lightDir = normalize(LightDirection);
    float3 lighting = saturate(dot(lightDir, normal)) * LightColor;

    // Specular Highlights
    float3 specular = float3(0, 0, 0);
    if (useSpecularHighlights)
    {
        float3 viewDir = normalize(input.ViewDirection);
        float3 reflectDir = reflect(-lightDir, normal);
        specular = pow(saturate(dot(reflectDir, viewDir)), SpecularPower) * SpecularColor;
    }
    // Diffuse Map
    float3 diffuse = DiffuseColor;
    if (useDiffuseMap)
    {
        diffuse *= tex2D(BasicTextureSampler, input.UV);
    }
    else
    {
        diffuse = float3(0.5, 0.5, 0.5); // Render in grey if no diffuse map
    }

    // Final Color
    float3 output = (saturate(AmbientColor) + lighting) * diffuse + specular;

    return float4(output, 1);
}

technique BasicColorDrawing
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};