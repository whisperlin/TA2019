Shader ""
{
	Properties
	{
		_MainTex ("Texture", 2D) = "Black" {}
		_EffectTex ("SourceTex", 2D) = "black" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "TerrainEngine.cginc" 

			struct appdata
			{
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
				float2 uv : TEXCOORD1;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 uv01 : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _EffectTex;
			float4 _MainTex_ST;
			float _Bloomrange;
			float _Nose;
			float _Add;
			float _Red;
			float _Green;
			float _Blue;

			
			v2f vert (appdata v)
			{
				v2f o; 
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv01 = 0.002*float4(1, 0, 0, 2);  
				return o;
			}
			fixed4 bloom(fixed4 input,float bloomrgane){
				fixed gray = 0.2125*input.r+0.7154*input.g+0.0721*input.b+bloomrgane;
				input *= saturate(gray);
				return input;
			}
			fixed4 frag (v2f i) : SV_Target
			{
				//nose;
				i.uv += SmoothTriangleWave(i.uv.x*20+_Time.y*0.5)*_Nose-_Nose*0.5;
				fixed4 col = tex2D(_MainTex, i.uv)*0.8;
				//Vignette;
			//	col*=saturate(i.uv.y*i.uv.x*(1-i.uv.y)*(1-i.uv.x)*50+0.2);
				//bloom;
				fixed4 add1 = tex2D(_EffectTex, i.uv+i.uv01.xy)+tex2D(_EffectTex, i.uv+i.uv01.xy*-1); 
				fixed4 add2 = tex2D(_EffectTex, i.uv+i.uv01.zw)+tex2D(_EffectTex, i.uv+i.uv01.zw*-1); 

				fixed4 add3 = tex2D(_EffectTex, i.uv+i.uv01.xy*2)+tex2D(_EffectTex, i.uv+i.uv01.xy*-2); 
				fixed4 add4 = tex2D(_EffectTex, i.uv+i.uv01.zw*2)+tex2D(_EffectTex, i.uv+i.uv01.zw*-2); 

				fixed4 add5 = tex2D(_EffectTex, i.uv+i.uv01.xy*3)+tex2D(_EffectTex, i.uv+i.uv01.xy*-3); 
				fixed4 add6 = tex2D(_EffectTex, i.uv+i.uv01.zw*3)+tex2D(_EffectTex, i.uv+i.uv01.zw*-3); 

				fixed4 add7 = tex2D(_EffectTex, i.uv+i.uv01.xy*4)+tex2D(_EffectTex, i.uv+i.uv01.xy*-4); 
				fixed4 add8 = tex2D(_EffectTex, i.uv+i.uv01.zw*4)+tex2D(_EffectTex, i.uv+i.uv01.zw*-4); 

				col.r *= _Red;
				col.g *= _Green;
				col.b *= _Blue;
				col += (bloom(add1*0.5,_Bloomrange)+bloom(add2*0.5,_Bloomrange)+bloom(add3*0.4,_Bloomrange)+bloom(add4*0.4,_Bloomrange)+bloom(add5*0.3,_Bloomrange)+bloom(add6*0.3,_Bloomrange)+bloom(add7*0.2,_Bloomrange)+bloom(add8*0.2,_Bloomrange))*_Add;


				return col;
			}
			ENDCG
		}
	}
}
