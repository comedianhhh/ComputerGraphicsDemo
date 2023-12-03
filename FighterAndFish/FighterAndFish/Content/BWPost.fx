#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

texture ScreenTexture;

sampler TextureSampler = sampler_state
{
    Texture = <ScreenTexture>;

};

struct PSInput
{
    float4 Position : SV_Position;
    float4 Color : COLOR0;
    float2 TextureCoordinate : TEXCOORD0;
};


float4 MainPS(PSInput input) : COLOR0
{
    float3 color = tex2D(TextureSampler, input.TextureCoordinate);
	
    
    float value = (color.r + color.g + color.b) / 3;
    color.r = value;
    color.g= value;
    color.b = value;
    
    
    
    return float4(color, 1);
}

technique BasicColorDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};