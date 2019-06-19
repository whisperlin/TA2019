Shader "Hidden/OutlineEffect"
{
	Properties
	{
		_MainTex ("MainTex", 2D) = "white" {}
		_MarkTex ("MarkTex", 2D) = "white" {}
		_Color("Color",Color) = (1,0, 0,1)
		_Factor("_Factor",Range(0.5,1)) = 0.5
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always
	
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			//Blend One One
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
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = v.vertex;
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _MarkTex;
			float4 _MainTex_TexelSize;
			half4 _Color;
			half _Factor;
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 mark = tex2D(_MarkTex, i.uv);


				fixed4 col1 = tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy*float2( _Factor, _Factor));
				fixed4 col2 = tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy*float2(-_Factor, _Factor));
				fixed4 col3 = tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy*float2( _Factor,-_Factor));
				fixed4 col4 = tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy*float2(-_Factor,-_Factor));
				 
				
				col = col*0.5+col1*0.125+col2*0.125+col3*0.125+col4*0.125;
				fixed s = step(col.r,0.9);
				//col*=s;
				return float4(_Color.rgb,col.r*s);
				return col*_Color;
			}
			ENDCG
		}
	}
}
