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

float4 GaiaShader(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
	float4 color = tex2D(uImage0, coords);
	float frameY = (coords.y * uImageSize0.y - uSourceRect.y) / uSourceRect.w;
    float wave = frac(uTime + (frameY * 0.5) + (sin(coords.x * 3.14f) / 4));
	color.rgb *= (wave * uColor) + ((1 - wave) * uSecondaryColor);
	

	return color * sampleColor * 1.5f;

	//return color * tex2D(uImage0, coords).a;
}

technique Technique1
{
	pass GaiaGlow
	{
		PixelShader = compile ps_2_0 GaiaShader();
	}

	pass GaiaArmor
	{
		PixelShader = compile ps_2_0 GaiaShader();
	}
}