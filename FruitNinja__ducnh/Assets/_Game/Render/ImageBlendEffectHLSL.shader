// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ImageBlendEffectHLSL"
{
	Properties
	{
		
		_MainTex ("Base", 2D) = "" {}
		_BlendTex ("Image", 2D) = "" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		
	}
	
	
	
	Subshader
	{
		Tags { "RenderType"="Opaque" }
		Pass
		{
			ZTest Always Cull Off ZWrite Off
			Fog
			{
				Mode off
			}

			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			
			struct Varyings
			{
				float4 positionHCS : SV_POSITION;
				float2 uv 		   : TEXCOORD0;
			};

			// struct appdata_img
			// {
			// 	float4 vertex : POSITION;
			// 	float2 texcoord : TEXCOORD0;
			// };

			struct Attributes{
				float4 position0S : POSITION;
				float2 uv		  : TEXCOORD0;
			};

			
			CBUFFER_START(UnityPerMaterial)
			sampler2D _MainTex;
			sampler2D _BlendTex;
			sampler2D _BumpMap;
			CBUFFER_END

			float _BlendAmount;
			float _EdgeSharpness;
			float _SeeThroughness;
			float _Distortion;
			

				
			Varyings vert(Attributes IN)
			{
				Varyings o;
				o.positionHCS = TransformObjectToHClip(IN.position0S.xyz);
				o.uv = IN.uv;
				return o;
			} 
			
			half4 frag(Varyings i) : SV_Target
			{ 
				float4 blendColor = tex2D(_BlendTex, i.uv);

				blendColor.a = blendColor.a + (_BlendAmount * 2 - 1);
				blendColor.a = saturate(blendColor.a * _EdgeSharpness - (_EdgeSharpness - 1) * 0.5);
				
				//Distortion:
				half2 bump = UnpackNormal(tex2D(_BumpMap, i.uv)).rg;
				float4 mainColor = tex2D(_MainTex, i.uv+bump*blendColor.a*_Distortion);
				
				//return float4(i.uv.x+bump.x,i.uv.y+bump.y,0.5,0)*blendColor.a*_Distortion; //Test
				
				float4 overlayColor = blendColor;
				overlayColor.rgb = mainColor.rgb*(blendColor.rgb+0.5)*(blendColor.rgb+0.5); //double overlay
				
				blendColor = lerp(blendColor,overlayColor,_SeeThroughness);

				return lerp(mainColor, blendColor, blendColor.a);
			}

			ENDHLSL
		}
	}

	Fallback off	
} 