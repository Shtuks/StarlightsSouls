sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float2 uTargetPosition;
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

float4 WCWings(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float2 noiseCoords = (coords * uImageSize0 - uSourceRect.xy) / uImageSize1;
    float4 noise = tex2D(uImage1, noiseCoords);
    float frameY = (coords.y * uImageSize0.y - uSourceRect.y) / uSourceRect.w;
    float time = frac(uTime * 2 + noise);
    float wave = 1 - sin((frameY.x - uTime * 3) + 1) / 2;
    noise.y *= sin(uTime);
    noise *= clamp(frac(uTime), tan(sin(noise.b)), time.x);
    color.rgb *= (wave * uColor * noise.w) + ((1 - wave * noise.w) * uSecondaryColor) * 2;
    
    return color;
}

float4 WCArmor(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float2 noiseCoords = (coords * uImageSize0 - uSourceRect.xy) / uImageSize1;
    float4 noise = tex2D(uImage1, noiseCoords);
    float frameY = (coords.y * uImageSize0.y - uSourceRect.y) / uSourceRect.w;
    float time = frac(uTime * 2 + noise);
    float wave = 1 - sin((frameY.x - uTime * 3) + 1) / 2;
    noise.y *= sin(uTime);
    noise *= clamp(frac(uTime), tan(sin(noise.b)), time.x);
    color.rgb *= (wave * uColor * noise.w) + ((1 - wave * noise.w) * uSecondaryColor) * 2;
    
    return color * 2 * sampleColor.b;
}

technique Technique1
{
    pass WCWings
    {
        PixelShader = compile ps_2_0 WCWings();
    }
    pass WCArmor
    {
        PixelShader = compile ps_2_0 WCArmor();
    }
}