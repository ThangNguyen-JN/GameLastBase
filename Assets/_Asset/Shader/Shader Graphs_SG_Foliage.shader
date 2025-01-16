Shader "Shader Graphs/SG_Foliage" {
    Properties {
        [NoScaleOffset] _BaseMask ("Base Mask", 2D) = "white" {}
        [NoScaleOffset] _WindMask ("Wind Mask", 2D) = "white" {}
        _Color_base ("Base Color", Color) = (1, 1, 1, 1)
        _Color_add ("Additional Color", Color) = (0, 0, 0, 0)
        _Emission ("Emission Intensity", Float) = 1.0
        _WindTiling ("Wind Tiling", Float) = 1.0
        _Wind_Speed ("Wind Speed", Float) = 1.0
        _Wind_EmissionAdd ("Wind Emission Add", Float) = 0.1
        _Wind_Power ("Wind Power", Float) = 0.5
    }
    SubShader {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _BaseMask;
        sampler2D _WindMask;

        fixed4 _Color_base;
        fixed4 _Color_add;
        float _Emission;
        float _WindTiling;
        float _Wind_Speed;
        float _Wind_EmissionAdd;
        float _Wind_Power;

        struct Input {
            float2 uv_BaseMask;
            float2 uv_WindMask;
        };

        void surf(Input IN, inout SurfaceOutputStandard o) {
            fixed4 baseColor = tex2D(_BaseMask, IN.uv_BaseMask) * _Color_base;

            float2 windUV = IN.uv_WindMask * _WindTiling + _Wind_Speed * _Time.y;
            fixed4 windColor = tex2D(_WindMask, windUV) * _Wind_Power;

            o.Albedo = baseColor.rgb + windColor.rgb + _Color_add.rgb;
            o.Emission = baseColor.rgb * _Emission + windColor.rgb * _Wind_EmissionAdd;
        }
        ENDCG
    }

    Fallback "Diffuse"
}