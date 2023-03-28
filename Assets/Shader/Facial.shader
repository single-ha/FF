// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "FF/Facial" {
    Properties {
        _MainColor ("Main Color", Color) = (1,1,1,1)
		[HDR]_BloomColor ("BloomColor", Color) = (0,0,0,0)
        _MainTex ("Diffuse (RGB) Alpha (A)", 2D) = "white" {}

        _CellAmount("Max Column",float)=4.0
        _CellRowCnt("Max Row",float)=1.0
        _vNum("Row",float)=0.0
        _Num ("Column", float) = 0.0
        _Alpha("Alpha", Range(0,1)) = 1.0
        _layer("Emoji Layer",int)=1
        [HideInInspector]_offsetX("offsetX", int) = -10
    }
    // CustomEditor "jinglingShaderEditor"
    SubShader {
        Blend SrcAlpha OneMinusSrcAlpha
 
        Tags { "Queue" = "Transparent+1" "IgnoreProjector" = "True" "RenderType" = "Transparent" "ForceNoShadowCasting" = "True" "RenderPipeline" = "UniversalPipeline"}
        
        Offset [_offsetX], 0
        Pass {
            HLSLPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_builtin
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
				#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
                
                CBUFFER_START(UnityPerMaterial)
				float4 _MainColor;
 				float _CellAmount;
		        float _CellRowCnt;
		        float _vNum;
		        float _Num;
		        float _Alpha;
				uniform half4 _BloomColor;
                CBUFFER_END
                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);
 			    struct appdata
			    {
				    float4 vertex : POSITION;
                    float4 texcoord:TEXCOORD0;
                };
                struct v2f
                {
                    float4  pos : SV_POSITION;
                    float2  uv : TEXCOORD0;
                };



                v2f vert (appdata v)
                {
                    v2f o;
                    o.pos = TransformObjectToHClip(v.vertex.xyz);
                    float2 spriteUV= v.texcoord.xy;
					o.uv = float2((spriteUV.x + _Num) / _CellAmount, (spriteUV.y+_vNum)/_CellRowCnt);
                    return o;
                }

                half4 frag(v2f i) : COLOR
                {
                    half4 result = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,i.uv);
                    half3 resultRgb = result.rgb;
					//resultRgb *= half3( _LightColor0.x,_LightColor0.y,_LightColor0.z);
					resultRgb *= _MainColor.rgb;
					resultRgb+=_BloomColor.rgb;
                    return half4(resultRgb,_Alpha*result.w);
                }
            ENDHLSL
        }
    }
}