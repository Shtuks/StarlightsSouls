sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;
float4 uLegacyArmorSourceRect;
float2 uLegacyArmorSheetSize;
float4 uShaderSpecificData;

float4 FinalSpark(float2 coords : TEXCOORD0) : COLOR0
{
	float4 colour = tex2D(uImage0, coords);

	//contrast
	colour = float4(colour.r + (colour.r - 0.5) * uOpacity, colour.g + (colour.g - 0.5) * uOpacity, colour.b + (colour.b - 0.5) * uOpacity, colour.a);

	//hue shift
	//float lerp = 1 - uOpacity;
	//colour = float4(colour.r * lerp + colour.b * uOpacity, colour.g * lerp + colour.r * uOpacity, colour.b * lerp + colour.g * uOpacity, colour.a);
	
	return colour;
}

technique Technique1
{
	pass FinalSpark
	{
		PixelShader = compile ps_2_0 FinalSpark();
	}
}