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

float4 PulseShader(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
	float4 color = tex2D(uImage0, coords);
    float wave = frac(uTime + (coords.y));
	color.rgb *= (wave * uColor) + ((1 - wave) * uSecondaryColor);
	

	return color * sampleColor * 2;

	//return color * tex2D(uImage0, coords).a;
}

float4 DiagonalShader(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float wave = frac(uTime/2 + coords.y/2 + coords.x/2);
    color.rgb *= (wave * uColor) + ((1 - wave) * uSecondaryColor);
	

    return color * sampleColor * 2;

	//return color * tex2D(uImage0, coords).a;
}

float4 CircleShader(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float dist = sqrt(pow((coords.x - 0.5), 2) + pow((coords.y - 0.5), 2));
    
    float wave = frac(uTime + dist);
    color.rgb *= (wave * uColor) + ((1 - wave) * uSecondaryColor);
	

    return color * sampleColor * 2;

	//return color * tex2D(uImage0, coords).a;
}

technique Technique1
{
	pass PulseUpwards
	{
		PixelShader = compile ps_2_0 PulseShader();
	}

    pass PulseDiagonal
    {
        PixelShader = compile ps_2_0 DiagonalShader();
    }

    pass PulseCircle
    {
        PixelShader = compile ps_2_0 CircleShader();
    }
}