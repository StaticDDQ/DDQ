// If possible, attach this to objects with a max height of 10
Shader "Custom/Dissolve"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DissolveTex("Dissolve Texture", 2D) = "white" {}
		_DissolveY("Current Y of Dissolve Effect", Range(0,5)) = 0
		_DissolveSize("Length of the effect", float) = 2
	}
	SubShader
	{
		Tags {"RenderType"="Opaque"}
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 worldPos : TEXCOORD1;
				float3 localPos : TEXCOORD2;
			};

			sampler2D _MainTex;
			sampler2D _DissolveTex;
			float _DissolveY;
			float _DissolveSize;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.localPos = o.worldPos - mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).xyz;
				o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{

				if (i.localPos.y > 0) {
					float transitionUp = i.localPos.y - _DissolveY;
					clip(transitionUp + tex2D(_DissolveTex, i.uv)*_DissolveSize);
				}
				else {
					float transitionDown = - _DissolveY - i.localPos.y;
					clip(transitionDown + tex2D(_DissolveTex, i.uv)*_DissolveSize);
				}

				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
	}
}
