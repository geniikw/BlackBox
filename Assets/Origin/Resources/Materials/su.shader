Shader "Custom/su" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Color2 ("Color2", Color) = (1,1,1,1)
		_Width ("Width", Range(0,0.5)) = 0.01
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _Color2;
		float _Width;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			
			fixed4 c;
			if(IN.uv_MainTex.x < _Width || IN.uv_MainTex.x > 1- _Width)
				c = tex2D(_MainTex, IN.uv_MainTex) * _Color2;
			else if (IN.uv_MainTex.y < _Width || IN.uv_MainTex.y > 1 - _Width)
				c = tex2D(_MainTex, IN.uv_MainTex) * _Color2;
			else
				c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
