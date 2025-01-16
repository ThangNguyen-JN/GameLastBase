// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D

Shader "Shader Graphs/SG_ZS_Terrain" {
    Properties {
        _QEmission ("QEmission", Float) = 1
        _Emmision ("Emmision", Float) = 0
        _Color_base ("Color_base", Vector) = (0, 0, 0, 1)
        _Color_alt ("Color_alt", Vector) = (0.5, 0.5, 0.5, 1)
        _Color_dirt ("Color_dirt", Vector) = (0.3, 0.2, 0.1, 1)
        _Color_dirt_alt ("Color_dirt_alt", Vector) = (0.4, 0.3, 0.2, 1)
        [NoScaleOffset] _CkeckerMask ("CheckerMask", 2D) = "white" {}
        _Tilling ("Tilling", Float) = 0.01
        [HideInInspector] _QueueOffset ("_QueueOffset", Float) = 0
        [HideInInspector] _QueueControl ("_QueueControl", Float) = -1
        [HideInInspector] [NoScaleOffset] unity_Lightmap ("unity_Lightmap", 2D) = "" {}
    }
    SubShader {
        Tags { "RenderType" = "Opaque" }
        LOD 200
        CGPROGRAM
        #pragma surface surf Standard

        struct Input {
            float2 uv_MainTex;
            float2 uv_CkeckerMask;
            float3 worldPos;
        };

        sampler2D _CkeckerMask;
        fixed4 _Color_base;
        fixed4 _Color_alt;
        fixed4 _Color_dirt;
        fixed4 _Color_dirt_alt;
        float _Tilling;
        float _QEmission;
        float _Emmision;

        // Declaring lightmap variables
        // sampler2D unity_Lightmap;

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Texture sampling for checker mask
            fixed4 checkerMask = tex2D(_CkeckerMask, IN.uv_CkeckerMask * _Tilling);
            fixed4 baseColor = lerp(_Color_base, _Color_alt, checkerMask.r);
            fixed4 dirtColor = lerp(_Color_dirt, _Color_dirt_alt, checkerMask.r);

            // Combine base color and dirt color
            o.Albedo = baseColor.rgb + dirtColor.rgb;

            // Emission
            o.Emission = _Emmision * (baseColor.rgb + dirtColor.rgb) * _QEmission;

            // Apply lightmap (if available)
            float2 lightmapUV = IN.worldPos.xz; // Simplified lightmap UV calculation
            fixed4 lightmap = UNITY_SAMPLE_TEX2D(unity_Lightmap, lightmapUV);
            o.Albedo *= lightmap.rgb;
        }
        ENDCG
    }

    Fallback "Hidden/Shader Graph/FallbackError"
}