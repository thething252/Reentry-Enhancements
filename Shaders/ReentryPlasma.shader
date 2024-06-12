Shader "Custom/ReentryPlasma"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _EmissionColor ("Emission Color", Color) = (1, 0, 0, 1)
        _GlowIntensity ("Glow Intensity", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed4 _EmissionColor;
        float _GlowIntensity;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            float2 offsetUV = IN.uv_MainTex - float2(0.0, 0.05); // Offset UV coordinates slightly
            fixed4 c = tex2D(_MainTex, offsetUV);
            o.Albedo = c.rgb;
            o.Emission = _EmissionColor.rgb * _GlowIntensity; // Apply glow intensity to emission color
        }
        ENDCG
    }
    FallBack "Diffuse"
}
