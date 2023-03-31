Shader "FF/Lit/LitBokenShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BokenTex ("BokenTex", 2D) = "white" {}
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
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _SHADOWS_SOFT
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/NormalsRenderingShared.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            // TEXTURE2D(_MainTex);            SAMPLER(sampler_MainTex);
            // TEXTURE2D(_BokenTex);           SAMPLER(sampler_BokenTex);
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _BokenTex;
            float4 _BokenTex_ST;

            struct appdata
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float2 uv2:TEXCOORD1;
            };

            struct v2f
            {
                float4 positionWS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 uv2:TEXCOORD1;
                float4 shadowCoord :TEXCOORD2;
            };



            v2f vert (appdata v)
            {
                v2f o;
                VertexPositionInputs vertexInput = GetVertexPositionInputs(v.positionOS.xyz);
                o.positionWS = vertexInput.positionCS;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv2=TRANSFORM_TEX(v.uv2, _BokenTex);
                o.shadowCoord =TransformWorldToShadowCoord(vertexInput.positionWS);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half3 ambient = half3(unity_SHAr.w, unity_SHAg.w, unity_SHAb.w);
                // sample the texture
                half4 col = tex2D(_MainTex,i.uv);
                half4 b=tex2D(_BokenTex,i.uv2);
                col.rgb=col.rgb*b.r;
                //
                Light mainLight = GetMainLight(i.shadowCoord);
                col.rgb = lerp(col.rgb*ambient.rgb*mainLight.color.rgb, col.rgb*mainLight.color.rgb, mainLight.shadowAttenuation);
                // half3 attenuatedLightColor = mainLight.color * (mainLight.distanceAttenuation * mainLight.shadowAttenuation);
                // float3 normalTS = UnpackNormal(tex2D(_NormalMap, i.uv2));
                // normalTS.xy *= _NormalScale;
                // normalTS.z = sqrt(1.0 - saturate(dot(normalTS.xy, normalTS.xy)));
                // half3 normalWS = TransformTangentToWorld(normalTS,real3x3(i.tangentWS, i.bitangentWS, i.normalWS));
                // half3 temp=TransformTangentToWorld(float3(0, 0, 1),real3x3(i.tangentWS, i.bitangentWS, i.normalWS));
				// float Ramp_light=saturate(dot(temp, normalWS));
                // col.rgb=col.rgb*Ramp_light;
                return col;
            }
            ENDHLSL
        }
        Pass
        {
            Name "ShadowCaster"
            Tags{"LightMode" = "ShadowCaster"}

            ZWrite On
            ZTest LEqual
            ColorMask 0
            Cull[_Cull]

            HLSLPROGRAM
            // Required to compile gles 2.0 with standard srp library
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature _ALPHATEST_ON

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma shader_feature _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            #pragma vertex ShadowPassVertex
            #pragma fragment ShadowPassFragment

            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/ShadowCasterPass.hlsl"
            ENDHLSL
        }
    }
}
