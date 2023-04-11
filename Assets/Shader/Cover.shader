Shader "FF/Cover" {
    Properties {
        _Edge_Width ("Edge_Width", Range(0, 1)) = 0.5
        _MainColor ("MainColor", Color) = (1,1,1,1)
        _Alpha ("Alpha", Range(0, 1)) = 1
        
        _Smooth("Alpha Smooth",Range(1,5))=1.5
        _MaxDis("Alpha MaxDistance",Float)=372
        _disColor("Dis Color", Color) = (0.57,0.5,0.434,1)
        [MaterialToggle] _toggleDistAlpha("Affect By Light",int)=1
    }
    // CustomEditor "jinglingShaderEditor"
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "RenderPipeline" = "UniversalPipeline"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="UniversalForward"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_fwdbase
            CBUFFER_START(UnityPerMaterial)
            uniform half4 _MainColor;
            uniform half _Edge_Width;
            uniform half _Alpha;
#if _DISTANCE
            float _Smooth;
            float _MaxDis;
            int _toggleDistAlpha;
            half4 _disColor;
#endif
            CBUFFER_END
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float NdotV : TEXCOORD0;
                
#if _DISTANCE
                float eyeAlpha : TEXCOORD1;
#endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;

                float3 normalDir = SafeNormalize(TransformObjectToWorldNormal(v.normal));
                float3 viewDirection = GetWorldSpaceNormalizeViewDir(TransformObjectToWorld(v.vertex.xyz));
                o.pos = TransformObjectToHClip( v.vertex.xyz );
                o.NdotV = dot(normalDir, viewDirection);
#if _DISTANCE
                float dis=length(GetWorldSpaceViewDir(v.vertex)) - (_MaxDis-_Smooth);
                o.eyeAlpha = clamp(dis/(_Smooth*2),0,1);
#endif
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float3 finalColor = _MainColor.rgb;
                
#if _DISTANCE 
                finalColor.rgb *= lerp(half3(1,1,1), _disColor.rgb, i.eyeAlpha);
                if(_toggleDistAlpha>0.5){
                finalColor.rgb*= _LightColor0.rgb;
                    _Alpha *= _LightColor0.a;
                }
#endif
                return float4(finalColor,_Alpha*(_MainColor.a*smoothstep( _Edge_Width, 1.0, (1.0-max(0,i.NdotV)) )));
            }
            ENDHLSL
        }
    }
    
}
