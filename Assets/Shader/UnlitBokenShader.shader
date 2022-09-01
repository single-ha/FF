Shader "Unlit/UnlitBokenShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BokenTex ("BokenTex", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "bump" {}
        _NormalScale("法线强度", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/NormalsRenderingShared.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            // TEXTURE2D(_MainTex);            SAMPLER(sampler_MainTex);
            // TEXTURE2D(_BokenTex);           SAMPLER(sampler_BokenTex);
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _BokenTex;
            float4 _BokenTex_ST;
            sampler2D _NormalMap;
            float4 _NormalMap_ST;
            float _NormalScale;

            struct appdata
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float2 uv2:TEXCOORD1;
                float4 normalOS:NORMAL;
                float4 tangentOS  : TANGENT;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv2:TEXCOORD1;
                float3  normalWS		: TEXCOORD2;
                float3  tangentWS		: TEXCOORD3;
                float3  bitangentWS		: TEXCOORD4;
                float fogCoord  : TEXCOORD5;
                float4 positionWS : SV_POSITION;
            };



            v2f vert (appdata v)
            {
                v2f o;
                VertexPositionInputs vertexInput = GetVertexPositionInputs(v.positionOS.xyz);
                o.positionWS = vertexInput.positionCS;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv2=TRANSFORM_TEX(v.uv2, _BokenTex);
                VertexNormalInputs normalInputs=GetVertexNormalInputs(v.normalOS.xyz,v.tangentOS);
                o.normalWS =normalInputs.normalWS;
                o.tangentWS = normalInputs.tangentWS;
                o.bitangentWS =normalInputs.bitangentWS ;
                o.fogCoord = ComputeFogFactor(vertexInput.positionCS.z);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // sample the texture
                half4 col = tex2D(_MainTex,i.uv);
                half4 b=tex2D(_BokenTex,i.uv2);
                // apply fog
                col.rgb = MixFog(col, i.fogCoord);
                //
                col.rgb=col.rgb*b.r;
                //
                float3 normalTS = UnpackNormal(tex2D(_NormalMap, i.uv2));
                normalTS.xy *= _NormalScale;
                normalTS.z = sqrt(1.0 - saturate(dot(normalTS.xy, normalTS.xy)));
                half3 normalWS = TransformTangentToWorld(normalTS,real3x3(i.tangentWS, i.bitangentWS, i.normalWS));
                half3 temp=TransformTangentToWorld(float3(0, 0, 1),real3x3(i.tangentWS, i.bitangentWS, i.normalWS));
				float Ramp_light=saturate(dot(temp, normalWS));
                col.rgb=col.rgb*Ramp_light;
                return col;
            }
            ENDHLSL
        }
    }
}
