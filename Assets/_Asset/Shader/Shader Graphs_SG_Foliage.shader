Shader "Shader Graphs/SG_Foliage" {
	Properties {
		[NoScaleOffset] _BaseMask ("BaseMask", 2D) = "white" {}
		[NoScaleOffset] _WindMask ("WindMask", 2D) = "white" {}
		_Color_base ("Color_base", Vector) = (0,0,0,0)
		_Color_add ("Color_add", Vector) = (0,0,0,0)
		_Emission ("Emission", Float) = 0
		_WindTiling ("WindTiling", Float) = 0
		_Wind_Speed ("Wind_Speed", Float) = 0
		_Wind_EmissionAdd ("Wind_EmissionAdd", Float) = 0
		_Wind_Power ("Wind_Power", Float) = 0
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