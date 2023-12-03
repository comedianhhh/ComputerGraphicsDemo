#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

bool useBlackandWite=false;
bool useUnderWater=false;



float time;
float frequency;
float amplitude;
bool tintBlue;

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
    float4 color = tex2D(TextureSampler, input.TextureCoordinate);
    if (useBlackandWite)
    {
	    // Convert to grayscale
        float gray = (color.r + color.g + color.b) / 3;
        color = float4(gray, gray, gray, 1);
        
    }
    else if( useUnderWater)
    {
        // Calculate sine wave offset
        float wave = sin(input.TextureCoordinate * frequency + time) * amplitude;

        // Adjust texture coordinate with wave offset
        float2 texCoord = input.TextureCoordinate;
        texCoord+= wave;

        // Sample the texture at the adjusted coordinates
        color = tex2D(TextureSampler, texCoord);

        // Apply blue tint if enabled
        if (tintBlue)
        {
            color.r /= 2;
            color.g /= 2;
        }
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