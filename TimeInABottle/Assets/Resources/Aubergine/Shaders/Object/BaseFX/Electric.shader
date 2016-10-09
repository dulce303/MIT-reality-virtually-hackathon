Shader "Aubergine/Object/BaseFX/Electric" {
	Properties {
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
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" "IgnoreProjector" = "True" }
		LOD 100

		Pass {
			Name "BASE"
			Tags { "LightMode" = "Always" }

			Fog { Mode off }
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma exclude_renderers xbox360 ps3 flash
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest

			fixed4 _Color;
			sampler2D _NoiseTex;
			half _SampleDist;
			fixed _Speed, _Noise, _Height, _Glow, _GlowScale;
			fixed _GlowFallOff, _GlowPower;
			fixed _UvXScale, _UvYScale;

			struct a2v {
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 xs : TEXCOORD1;
			};

			v2f vert(a2v v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = (v.texcoord.xy - 0.5);
				o.xs.x = o.uv.x - _SampleDist;
				o.xs.y = o.uv.x;
				o.xs.z = o.uv.x + _SampleDist;
				o.xs *= _UvXScale;
				return o;
			}

			fixed4 frag(v2f i) : COLOR {
				float t = _Speed * _Time.y * 0.5871 - _Noise * abs(i.uv.y);
				t *= _UvYScale;
				fixed n0 = tex2D(_NoiseTex, float2(i.xs.x, t)).r;
				fixed n1 = tex2D(_NoiseTex, float2(i.xs.y, t)).r;
				fixed n2 = tex2D(_NoiseTex, float2(i.xs.z, t)).r;
				half m0 = _Height * (n0 * 2.0 - 1.0) * (1.0 - i.xs.x * i.xs.x);
				half m1 = _Height * (n1 * 2.0 - 1.0) * (1.0 - i.xs.y * i.xs.y);
				half m2 = _Height * (n2 * 2.0 - 1.0) * (1.0 - i.xs.z * i.xs.z);
				half d0 = abs(i.uv.y - m0);
				half d1 = abs(i.uv.y - m1);
				half d2 = abs(i.uv.y - m2);
				half glow = 1.0 - pow(0.25 * (d0 + 2.0 * d1 + d2), _GlowFallOff);
				half amb = _Glow * (1.0 - i.xs.y * i.xs.y) * (1.0 - abs(_GlowScale * i.uv.y));
				return _Color * (_GlowPower * glow * glow + amb);
			}
			ENDCG
		}
	}

	FallBack Off
}