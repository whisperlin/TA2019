Shader "Custom/Skin Shader" {
  Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Diffuse (RGB)", 2D) = "white" {}
		_SpecularTex ("Specular (R) Gloss (G) SSS Mask (B)", 2D) = "yellow" {}
		_BumpMap ("Normal (Normal)", 2D) = "bump" {}
		// BRDF Lookup texture, light direction on x and curvature on y.
		_BRDFTex ("BRDF Lookup (RGB)", 2D) = "gray" {}
		// Curvature scale. Multiplier for the curvature - best to keep this very low - between 0.02 and 0.002.
		_CurvatureScale ("Curvature Scale", Float) = 0.005
		// Controller for fresnel specular mask. For skin, 0.028 if in linear mode, 0.2 for gamma mode.
		_Fresnel ("Fresnel Value", Float) = 0.2
		// Which mip-map to use when calculating curvature. Best to keep this between 1 and 2.
		_BumpBias ("Normal Map Blur Bias", Float) = 1.5
	}
 
	SubShader{
		Tags { "Queue" = "Geometry" "RenderType" = "Opaque" }
 
		CGPROGRAM
 
			#pragma surface surf SkinShader fullforwardshadows
			#pragma target 3.0
			// Bit complex for non-desktop.
			#pragma only_renderers d3d9 d3d11 opengl
			// Required for tex2Dlod function.
			#pragma glsl
 
			struct SurfaceOutputSkinShader {
				fixed3 Albedo;
				fixed3 Normal;
				fixed3 NormalBlur;
				fixed3 Emission;
				fixed3 Specular;
				fixed Alpha;
				float Curvature;
			};
 
			struct Input
			{
				float2 uv_MainTex;
				float3 worldPos;
				float3 worldNormal;
				INTERNAL_DATA
			};
 
			sampler2D _MainTex, _SpecularTex, _BumpMap, _BRDFTex;
			float _BumpBias, _CurvatureScale, _Fresnel;
 
			void surf (Input IN, inout SurfaceOutputSkinShader o)
			{
				float4 albedo = tex2D ( _MainTex, IN.uv_MainTex );
				o.Albedo = albedo.rgb;
 
				o.Normal = UnpackNormal ( tex2D ( _BumpMap, IN.uv_MainTex ) );
 
				o.Specular = tex2D ( _SpecularTex, IN.uv_MainTex ).rgb;
 
				// Calculate the curvature of the model dynamically.
				
				// Get a mip of the normal map to ignore any small details for regular shading.
				o.NormalBlur = UnpackNormal( tex2Dlod ( _BumpMap, float4 ( IN.uv_MainTex, 0.0, _BumpBias ) ) );
				// Transform it back into a world normal so we can get good derivatives from it.
				float3 worldNormal = WorldNormalVector( IN, o.NormalBlur );
				// Get the scale of the derivatives of the blurred world normal and the world position.
				// From these it's possible to work out the rate of change of the surface normal; or it's curvature.
				//#if SHADER_API_D3D11
					// In DX11, ddx_fine should give nicer results.
				//	float deltaWorldNormal = length( abs(ddx_fine(worldNormal)) + abs(ddy_fine(worldNormal)) );
				//	float deltaWorldPosition = length( abs(ddx_fine(IN.worldPos)) + abs(ddy_fine(IN.worldPos)) );
				//#else
					// Otherwise stick with ddx or dFdx, which can be replaced with fwidth.
					float deltaWorldNormal = length( fwidth( worldNormal ) );
					float deltaWorldPosition = length( fwidth ( IN.worldPos ) );
				//#endif
				
				o.Curvature = ( deltaWorldNormal / deltaWorldPosition ) * _CurvatureScale;
			}
 
			inline fixed4 LightingSkinShader( SurfaceOutputSkinShader s, fixed3 lightDir, fixed3 viewDir, fixed atten )
			{
				viewDir = normalize( viewDir );
				lightDir = normalize( lightDir );
				s.Normal = normalize( s.Normal );
				s.NormalBlur = normalize( s.NormalBlur );
				
				float NdotL = dot( s.Normal, lightDir );
				float3 h = normalize( lightDir + viewDir );
 
				float specBase = saturate( dot( s.Normal, h ) );
 
				float fresnel = pow( 1.0 - dot( viewDir, h ), 5.0 );
				fresnel += _Fresnel * ( 1.0 - fresnel );
 
				float spec = pow( specBase, s.Specular.g * 128 ) * s.Specular.r * fresnel;
 
				float2 brdfUV;
				float NdotLBlur = dot( s.NormalBlur, lightDir );
				// Half-lambert lighting value based on blurred normals.
				brdfUV.x = NdotLBlur * 0.5 + 0.5;
				//Curvature amount. Multiplied by light's luminosity so brighter light = more scattering.
				brdfUV.y = s.Curvature * dot( _LightColor0.rgb, fixed3(0.22, 0.707, 0.071 ) );
				float3 brdf = tex2D( _BRDFTex, brdfUV ).rgb;
				
				float m = atten; // Multiplier for spec and brdf.
				#if !defined (SHADOWS_SCREEN) && !defined (SHADOWS_DEPTH) && !defined (SHADOWS_CUBE)
					// If shadows are off, we need to reduce the brightness
					// of the scattering on polys facing away from the light
					// as it won't get killed off by shadow value.
					// Same for the specular highlights.
					//m *= saturate( ( (NdotLBlur * 0.5 + 0.5) * 2.0) * 2.0 - 1.0);
				#endif
 
				fixed4 c;
				c.rgb = (lerp(s.Albedo * saturate(NdotL) * atten, s.Albedo * brdf * m, s.Specular.b ) * _LightColor0.rgb + (spec * m * _LightColor0.rgb) ) * 2;
				c.a = s.Curvature; // Output the curvature to the frame alpha, just as a debug.
				return c;
			}
 
		ENDCG
	}
	FallBack "VertexLit"
}