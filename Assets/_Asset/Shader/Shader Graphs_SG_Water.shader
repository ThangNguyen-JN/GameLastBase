Shader "Shader Graphs/SG_Water" {
    Properties {
        _Depth ("Water Depth", Float) = 10
        _DepthFalloff ("Depth Falloff", Float) = 5
        _MainColor ("Main Color", Color) = (0.0, 0.5, 1.0, 1.0)
        _ShoreColor ("Shore Color", Color) = (1.0, 1.0, 0.8, 1.0)
        [NoScaleOffset] _CausticTexture ("Caustic Texture", 2D) = "white" {}
        _CausticCutouts ("Caustic Cutouts", Float) = 0.5
        _CausticTiling ("Caustic Tiling", Float) = 10.0
        _CausticSpeed ("Caustic Speed", Float) = 0.5
        _FoamShoreWidth ("Foam Shore Width", Float) = 2.0
        _FoamColor ("Foam Color", Color) = (1.0, 1.0, 1.0, 1.0)
        [NoScaleOffset] _FoamTexture ("Foam Texture", 2D) = "white" {}
        _FoamDepth ("Foam Depth", Float) = 1.0
        _FoamFalloff ("Foam Falloff", Float) = 0.5
        _FoamTiling ("Foam Tiling", Float) = 5.0
        _FoamSpeed ("Foam Speed", Vector) = (0.1, 0.1, 0.0, 0.0)
        _FoamAmount ("Foam Amount", Float) = 0.8
        _FoamCutout ("Foam Cutout", Float) = 0.3
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _CausticTexture;
        sampler2D _FoamTexture;

        float _Depth;
        float _DepthFalloff;
        fixed4 _MainColor;
        fixed4 _ShoreColor;
        float _CausticCutouts;
        float _CausticTiling;
        float _CausticSpeed;

        float _FoamShoreWidth;
        fixed4 _FoamColor;
        float _FoamDepth;
        float _FoamFalloff;
        float _FoamTiling;
        float4 _FoamSpeed;
        float _FoamAmount;
        float _FoamCutout;

        struct Input {
            float2 uv_CausticTexture;
            float2 uv_FoamTexture;
            float3 worldPos;
        };

        void surf(Input IN, inout SurfaceOutputStandard o) {
            // Calculate depth-based color blending
            float depthFactor = saturate((IN.worldPos.y + _DepthFalloff) / _Depth);
            fixed4 waterColor = lerp(_ShoreColor, _MainColor, depthFactor);

            // Caustics effect
            float2 causticUV = IN.uv_CausticTexture * _CausticTiling + float2(_CausticSpeed * _Time.x, _CausticSpeed * _Time.y);
            fixed4 caustic = tex2D(_CausticTexture, causticUV) * _CausticCutouts;
            waterColor.rgb += caustic.rgb;

            // Foam effect
            float foamFactor = saturate((1.0 - depthFactor) * _FoamDepth - _FoamFalloff);
            float2 foamUV = IN.uv_FoamTexture * _FoamTiling + (_FoamSpeed.xy * _Time.y);
            fixed4 foam = tex2D(_FoamTexture, foamUV) * _FoamAmount * foamFactor;

            // Apply foam and caustics to Albedo
            o.Albedo = waterColor.rgb + foam.rgb;

            // Specular for shininess
            o.Smoothness = 0.5;

            // Emission for glowing effects (optional)
            o.Emission = waterColor.rgb * 0.2;
        }
        ENDCG
    }
    Fallback "Diffuse"
}
