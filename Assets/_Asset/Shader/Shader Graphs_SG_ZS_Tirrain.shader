Shader "Shader Graphs/SG_ZS_Tirrain" {
	Properties {
		_QEmission ("QEmission", Float) = 1
		_Emmision ("Emmision", Float) = 0
		_Color_base ("Color_base", Vector) = (0,0,0,0)
		_Color_alt ("Color_alt", Vector) = (0,0,0,0)
		_Color_dirt ("Color_dirt", Vector) = (0,0,0,0)
		_Color_dirt_alt ("Color_dirt_alt", Vector) = (0,0,0,0)
		[NoScaleOffset] _CkeckerMask ("CkeckerMask", 2D) = "white" {}
		_Tilling ("Tilling", Float) = 0.01
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