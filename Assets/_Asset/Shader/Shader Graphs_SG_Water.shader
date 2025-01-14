Shader "Shader Graphs/SG_Water" {
	Properties {
		_Depth ("Depth", Float) = 0
		_DepthFalloff ("DepthFalloff", Float) = 0
		_MainColor ("MainColor", Vector) = (0,0,0,0)
		_ShoreColor ("ShoreColor", Vector) = (0,0,0,0)
		[NoScaleOffset] _CausticTexture ("CausticTexture", 2D) = "white" {}
		_CausticCutouts ("CausticCutouts", Float) = 0
		[HDR] _CausticColor ("CausticColor", Vector) = (0,0,0,0)
		_CausticTiling ("CausticTiling", Float) = 0
		_CausticSpeed ("CausticSpeed", Float) = 0
		_FoamShoreWidth ("FoamShoreWidth", Float) = 0
		_FoamColor ("FoamColor", Vector) = (0,0,0,0)
		[NoScaleOffset] _FoamTexture ("FoamTexture", 2D) = "white" {}
		_FoamDepth ("FoamDepth", Float) = 0
		_FoamFalloff ("FoamFalloff", Float) = 0
		_FoamTiling ("FoamTiling", Float) = 0
		_FoamSpeed ("FoamSpeed", Vector) = (0,0,0,0)
		_FoamAmount ("FoamAmount", Float) = 0
		_FoamCutout ("FoamCutout", Float) = 0
		[HideInInspector] _QueueOffset ("_QueueOffset", Float) = 0
		[HideInInspector] _QueueControl ("_QueueControl", Float) = -1
		[HideInInspector] [NoScaleOffset] unity_Lightmaps ("unity_Lightmaps", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_LightmapsInd ("unity_LightmapsInd", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_ShadowMasks ("unity_ShadowMasks", 2DArray) = "" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Hidden/Shader Graph/FallbackError"
	//CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
}