Shader "Aubergine/Object/Surf/Sample/Diffuse-Electric" {
	Properties {
		_MainTex("Base (RGB)", 2D) = "white" {}
		/* ELECTRIC SHADER PROPERTIES */
		_Color("Electric Color", Color) = (0.687, 0.481, 1, 1)
		_NoiseTex("Noise Texture", 2D) = "white" { }
		_SampleDist("Sample Distance", Float) = 0.0076
		_Speed("Speed", Float) = 1.86
		_Noise("Noise", Float) = 0.78
		_Height("Wave Height", Float) = 0.44
		_Glow("Glow Amount", Float) = 0.5
		_GlowScale("Glow Height", Float) = 1.68
		_GlowFallOff("Glow Falloff", Float) = 0.024
		_GlowPower("Glow Power", Float) = 144.0
		_UvXScale("Uv X Scale", Float) = 1.0
		_UvYScale("Uv Y Scale", Float) = 0.25
	}

	SubShader {
		Tags { "RenderType" = "Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma exclude_renderers xbox360 ps3 flash
		#pragma surface surf Lambert

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG

		/* ELECTRIC SHADER PASS */
		UsePass "Aubergine/Object/BaseFX/Electric/BASE"
	} 

	FallBack "Diffuse"
}