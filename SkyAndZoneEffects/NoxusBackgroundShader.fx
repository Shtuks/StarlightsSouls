sampler baseTexture : register(s0);
sampler backgroundNoiseTexture : register(s1);
sampler flashNoiseTexture : register(s2);

float globalTime;
float luminanceThreshold;
float scrollSpeed;
float noiseZoom;
float intensity;
float flashNoiseZoom;
float flashIntensity;
float2 flashCoordsOffset;
float2 flashPosition;
float2 screenPosition;
float3 backgroundColor;

float2x2 fbmMatrix = float2x2(1.63, 1.2, -1.2, 1.63);

float turbulentNoise(float2 coords)
{
    float2 currentCoords = coords;
    
    // Approximate, somewhat basic FBM equations with time included.
    float result = 0.5 * tex2D(backgroundNoiseTexture, (currentCoords + float2(0, globalTime * scrollSpeed * -0.3)) * noiseZoom);
    currentCoords = mul(currentCoords, fbmMatrix);
    currentCoords.y += globalTime * scrollSpeed * 0.25;
    
    result += 0.25 * tex2D(backgroundNoiseTexture, currentCoords * noiseZoom);
    currentCoords = mul(currentCoords, fbmMatrix);
    currentCoords.x += globalTime * scrollSpeed * 0.4;
    
    return result * 1.0666667;
}

float3 swirl(float2 coords)
{
    // Start by using turbulence as a base for the background.
    float3 result = backgroundColor * (turbulentNoise(coords * 4) + turbulentNoise(coords * 12)) * 0.75;
    
    return pow(result, 2.75);
}

float4 PixelShaderFunction(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(baseTexture, coords);
    float luminance = dot(color.rgb, float3(0.299, 0.587, 0.114));
    float2 worldOffset = screenPosition * 0.00006;
    float4 finalColor = float4(swirl(coords + worldOffset), 1) * sampleColor * intensity;
    
    // Apply the flash effect.
    float flashDissipation = distance(coords, flashPosition) * 3 + 1;
    float flash = tex2D(flashNoiseTexture, coords * noiseZoom + flashCoordsOffset) * intensity * flashIntensity / pow(flashDissipation, 8.1);
    finalColor = finalColor * (1 + flash) + flash * 0.02;
    
    // The step multiplier is equivalant to multiplying everything by 0 if luminance is less than luminanceThreshold.
    return finalColor * step(luminanceThreshold, luminance);
}
technique Technique1
{
    pass AutoloadPass
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}