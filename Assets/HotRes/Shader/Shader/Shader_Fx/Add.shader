// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "FX/Add"
{
	Properties
	{
		[Enum(Default,2,Always,6)]_ZTestMode("ZTestMode", Int) = 2
		[Enum(A,0,R,1)]_MainTexAlpha("MainTexAlpha", Int) = 0
		[HDR]_MainColor("MainColor", Color) = (1,1,1,1)
		_MainTex("MainTex", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

		_StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
		_ColorMask ("Color Mask", Float) = 15

	}
	
	SubShader
	{
		
		Tags { "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline" = "UniversalPipeline" }
		//LOD 100
		
		Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp] 
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
  
        }
		HLSLPROGRAM
		#pragma target 3.0
		ENDHLSL
		Blend One One
		Cull Off
		ColorMask RGBA
		ZWrite Off
		ZTest [_ZTestMode]

		ColorMask [_ColorMask]

		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="UniversalForward" }
			HLSLPROGRAM
#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
		//only defining to not throw compilation error over Unity 5.5
		#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
#endif
			#pragma vertex vert
			#pragma fragment frag
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;
				float3 vpos : TEXCOORD2;
			};

			uniform int _ZTestMode;
			uniform half4 _MainColor;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform int _MainTexAlpha;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord.xy = v.ase_texcoord.xy;
				o.ase_color = v.color;
				o.vpos = v.vertex.xyz;
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				float3 vertexValue =  float3(0,0,0) ;
				v.vertex.xyz += vertexValue;
				o.vertex = TransformObjectToHClip(v.vertex);
				return o;
			}
			
			half4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				half4 finalColor;
				float2 uv_MainTex = i.ase_texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode1 = tex2D( _MainTex, uv_MainTex );
				float lerpResult5 = lerp( tex2DNode1.a , tex2DNode1.r , (float)_MainTexAlpha);
			
				finalColor = ( _MainColor * tex2DNode1 * i.ase_color * ( _MainColor.a * lerpResult5 * i.ase_color.a ) );
				// finalColor.a *= (i.vpos.x >= _MinX );
	           	// finalColor.a *= (i.vpos.x <= _MaxX);
	            // finalColor.a *= (i.vpos.y >= _MinY);
	            // finalColor.a *= (i.vpos.y <= _MaxY);
				// return inArea? finalColor:half4(0,0,0,0);
				return finalColor;
			}
			 ENDHLSL
		}
	}
	
	
}
/*ASEBEGIN
Version=16900
141;156;1433;677;687.5;166.5;1;True;False
Node;AmplifyShaderEditor.IntNode;7;-226.5,329.5;Float;False;Property;_MainTexAlpha;MainTexAlpha;1;1;[Enum];Create;True;2;A;0;R;1;0;False;0;0;0;0;1;INT;0
Node;AmplifyShaderEditor.SamplerNode;1;-333.5,-28.5;Float;True;Property;_MainTex;MainTex;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;5;15.5,68.5;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;4;-213.5,168.5;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;2;-250.5,-204.5;Half;False;Property;_MainColor;MainColor;2;1;[HDR];Create;True;0;0;False;0;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;183.5,43.5;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.IntNode;11;-203.5,405.5;Float;False;Property;_ZTestMode;ZTestMode;0;1;[Enum];Create;True;2;Default;2;Always;6;0;True;0;2;0;0;1;INT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;330.5,-44.5;Float;False;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;501,-44;Half;False;True;2;Half;ASEMaterialInspector;0;1;FX/Add;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;4;1;False;-1;1;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;False;True;2;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;True;11;True;False;0;False;-1;0;False;-1;True;2;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;2;0;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;0;0;1;True;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;5;0;1;4
WireConnection;5;1;1;1
WireConnection;5;2;7;0
WireConnection;9;0;2;4
WireConnection;9;1;5;0
WireConnection;9;2;4;4
WireConnection;3;0;2;0
WireConnection;3;1;1;0
WireConnection;3;2;4;0
WireConnection;3;3;9;0
WireConnection;0;0;3;0
ASEEND*/
//CHKSM=A8A5E2962069423E9A5D270029526551B5ED1295