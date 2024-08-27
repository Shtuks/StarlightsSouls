sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float2 uTargetPosition;
float uOpacity;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;
float4 uLegacyArmorSourceRect;
float2 uLegacyArmorSheetSize;
float4 uShaderSpecificData;

float4 LCWings(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float frameY = (coords.y * uImageSize0.y - uSourceRect.y) / uSourceRect.w;
    float wave = 1 - sin((frameY.x - uTime * 3) + 1) / 2;
    color.rgb *= (wave * uColor) + ((1 - wave) * uSecondaryColor);
    
    return color * 2 * sampleColor;
}

float4 LCArmor(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float frameY = (coords.y * uImageSize0.y - uSourceRect.y) / uSourceRect.w;
    float wave = 1 - sin((frameY.x - uTime * 6) + 1) / 2;
    color.rgb *= (wave * uColor) + ((1 - wave) * uSecondaryColor);
    
    return color * 2 * sampleColor.b;
}

technique Technique1
{
    pass LCWings
    {
        PixelShader = compile ps_2_0 LCWings();
    }
    pass LCArmor
    {
        PixelShader = compile ps_2_0 LCArmor();
    }
}