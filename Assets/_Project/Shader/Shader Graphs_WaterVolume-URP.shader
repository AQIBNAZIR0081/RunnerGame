Shader "Shader Graphs/WaterVolume-URP" {
	Properties {
		Color_F01C36BF ("_ShallowColor", Vector) = (0.1019608,0.4627451,0.5764706,1)
		Color_7D9A58EC ("_DeepColor", Vector) = (0.1403524,0.1653288,0.3584906,0)
		Vector1_47683D42 ("_Glossiness", Range(0, 1)) = 0.85
		Vector1_3D886DA1 ("_Metallic", Range(0, 1)) = 0
		[NoScaleOffset] [Normal] Texture2D_6490A223 ("_NormalMap", 2D) = "bump" {}
		Vector2_37B21477 ("_UVScale", Vector) = (1,1,0,0)
		Vector1_F38B44AA ("_DetailScale", Float) = 0.2
		Vector1_46E42935 ("_DetailStrength", Range(0, 1)) = 0.2
		Vector1_244B0600 ("_ScrollSpeed", Float) = 1
		_WaveFrequency ("_WaveFrequency", Float) = 3
		_WaveScale ("_WaveScale", Float) = 0.1
		_WaveSpeed ("_WaveSpeed", Float) = 1
		Vector1_B9F56378 ("_BumpStrength", Float) = 0.5
		Vector1_A6A0BC26 ("_RefractStrength", Float) = 0.08
		Vector1_36E8227 ("_FoamWidth", Float) = 0.5
		Vector1_C360A163 ("_FoamNoise", Float) = 2
		Vector1_17E53C12 ("_DepthScale", Float) = 0.75
		Vector1_A0EAD698 ("_DepthPower", Float) = 1
		Vector1_E5C51606 ("_DepthFoam", Float) = 0
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