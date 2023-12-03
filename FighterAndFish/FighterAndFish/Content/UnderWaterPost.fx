#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif



float time;
float frequency;
float amplitude;
bool tintBlue;
texture sceneTexture;
sampler2D sceneSampler = sampler_state
{
    Texture = <sceneTexture>;
};


struct PSInput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
    
};


float4 MainPS(PSInput input) : COLOR
{
    // Calculate sine wave offset
    float wave = sin(input.TexCoord.x * frequency + time) * amplitude;

    // Adjust texture coordinate with wave offset
    float2 texCoord = input.TexCoord;
    texCoord.y += wave;

    // Sample the texture at the adjusted coordinates
    float4 color = tex2D(sceneSampler, texCoord);

    // Apply blue tint if enabled
    if (tintBlue)
    {
        color.r /= 2;
        color.g /= 2;
    }

    return color;
}

technique BasicColorDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};