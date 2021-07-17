﻿Shader "RacingGame/BendShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
		_Curvature("Curvature", Float) = 0.0001
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 100

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert addshadow

		uniform sampler2D _MainTex;
		uniform float _Curvature;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;

		void vert(inout appdata_full v) {
			float4 worldSpace = mul(unity_ObjectToWorld, v.vertex);
			worldSpace.xyz -= _WorldSpaceCameraPos.xyz;
			worldSpace = float4(0.0f, (worldSpace.z * worldSpace.z) * -_Curvature, 0.0f, 0.0f);

			v.vertex += mul(unity_WorldToObject, worldSpace);
		}

		void surf(Input IN, inout SurfaceOutput o) {
			half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
		FallBack "Transparent/Diffuse"
}
