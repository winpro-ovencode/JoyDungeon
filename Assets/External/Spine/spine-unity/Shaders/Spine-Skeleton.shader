Shader "Spine/Skeleton" {
	Properties {
		_Cutoff ("Shadow alpha cutoff", Range(0,1)) = 0.1
		[NoScaleOffset] _MainTex ("Main Texture", 2D) = "black" {}
		_FillColor ("FillColor", Color) = (1,1,1,1)
		_FillPhase ("FillPhase", Range(0, 1)) = 0
		_Multiplier("Multiplier", Range(0,10)) = 0
		_BlinkColor("Blink Color", Color) = (1,1,1,1)
	}

	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }

		Fog { Mode Off }
		Cull Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		Lighting Off

		Pass {
			/*
			Fog { Mode Off }
			ColorMaterial AmbientAndDiffuse
			SetTexture [_MainTex] {
				Combine texture * primary
			}
			*/
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			sampler2D _MainTex;
			float4 _FillColor;
			float _FillPhase;
			fixed4 _BlinkColor;
			fixed _Multiplier;


			struct VertexInput {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float4 vertexColor : COLOR;
			};

			struct VertexOutput {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 vertexColor : COLOR;
			};

			VertexOutput vert (VertexInput v) {
				VertexOutput o = (VertexOutput)0;
				o.uv = v.uv;
				o.vertexColor = v.vertexColor;
				o.pos = UnityObjectToClipPos(v.vertex); // Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
				return o;
			}

			float4 frag (VertexOutput i) : COLOR {
				float4 rawColor = tex2D(_MainTex,i.uv);

				float finalAlpha = (rawColor.a * i.vertexColor.a);
				float3 finalColor = lerp((rawColor.rgb * i.vertexColor.rgb), (_FillColor.rgb * finalAlpha), _FillPhase); // make sure to PMA _FillColor.

				finalColor = (finalColor * ((_Multiplier * -1 ) + 1) + ((finalColor * 2) * _BlinkColor * _Multiplier ));

				return fixed4(finalColor, finalAlpha);
			}
			ENDCG
		

		}

		Pass {
			Name "Caster"
			Tags { "LightMode"="ShadowCaster" }
			Offset 1, 1
			ZWrite On
			ZTest LEqual

			Fog { Mode Off }
			Cull Off
			Lighting Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_shadowcaster
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			sampler2D _MainTex;
			fixed _Cutoff;

			struct v2f { 
				V2F_SHADOW_CASTER;
				float2  uv : TEXCOORD1;
			};

			v2f vert (appdata_base v) {
				v2f o;
				TRANSFER_SHADOW_CASTER(o)
				o.uv = v.texcoord;
				return o;
			}

			float4 frag (v2f i) : COLOR {
				fixed4 texcol = tex2D(_MainTex, i.uv);
				clip(texcol.a - _Cutoff);
				SHADOW_CASTER_FRAGMENT(i)
			}
			ENDCG
		}
	}

	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }

		Cull Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		Lighting Off

		Pass {
			ColorMaterial AmbientAndDiffuse
			SetTexture [_MainTex] {
				Combine texture * primary DOUBLE, texture * primary
			}
		}
	}
}
