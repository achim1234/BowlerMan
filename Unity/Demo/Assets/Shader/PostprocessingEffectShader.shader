Shader "Custom/PostprocessingEffectShader" 
{
	Properties 
	{
		_MainTex ("Render Texture", 2D) = "white" {}
		_BrightnessAmount("Brightness Amount", Range(0,2)) = 1.0
		_SaturationAmount("Saturation Amount", Range(0,3)) = 1.0
		_ContrastAmount("Contrast Amount", Range(0,3)) = 1.0
		_BlurFactor ("Blur Factor", Range(0,1)) = 0
		_VignetteTexture ("Vignette Texture", 2D) = "white" {}
		_VignetteAmount("VignetteAmount", Range(0,1)) = 0
		_RedVignetteTexture("Red Vignette Texture", 2D) = "white" {}
		_RedVignetteAmount("Red VignetteAmount", Range(0,1)) = 0
	}
	SubShader 
	{
		Pass
		{
			CGPROGRAM

			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform fixed _BrightnessAmount;
			uniform half _SaturationAmount;
			uniform half _ContrastAmount;
			uniform fixed _BlurFactor;
			uniform sampler2D _VignetteTexture;
			uniform fixed _VignetteAmount;
			uniform sampler2D _RedVignetteTexture;
			uniform fixed _RedVignetteAmount;

			fixed4 frag(v2f_img i) : COLOR
			{
				//The color of our rendered image for the current UV
				fixed4 renderTex = tex2D(_MainTex, i.uv);

				/*
					Here goes the calculations of the shader
					
					Make it step by step
					The following Effects can be implemented, ordered by difficulty, from easy to hard

					1. VignetteShader
						--> get the VignetteTexture and blend it over the render image
					2. Image Brightness
						--> make the colors more bright, more white (255,255,255)
					3. Image Saturation
						--> strengthen the intensity of the color, tip: use Luminance Coefficient
					4. Image Contrast
						--> tip: use grey and the calculated Saturation
					5. Blur Effect
						--> modify the uv to shift the render image multiple times

				*/



				// http://armedunity.com/topic/5539-vignette-shader-optimized/
				/*
				float2 dist = (i.uv - 0.5f) * 1.25f;
				dist.x = 1 - dot(dist, dist)  * _VignetteAmount;
				renderTex *= dist.x;
				*/
				
				float2 dist = (i.uv - 0.5f) * 1.25f;
				//dist.x = 1 - dot(dist, dist)  *  _RedVignetteAmount * 0.5f;
				dist.x = 1 - dot(dist, dist)  *  _RedVignetteAmount;
				renderTex.gb *= dist.x;
				//renderTex *= dist.x;
				


				if (_BlurFactor >= 0.8 && _BlurFactor < 0.9) {
					renderTex += tex2D(_MainTex, i.uv + 0.000085);
					//renderTex += tex2D(_MainTex, i.uv + 0.003);
					//renderTex += tex2D(_MainTex, i.uv + 0.005);

					renderTex += tex2D(_MainTex, i.uv - 0.000085);
					//renderTex += tex2D(_MainTex, i.uv - 0.003);
					//renderTex += tex2D(_MainTex, i.uv - 0.005);

					renderTex = renderTex / 3.25;
				}

				if (_BlurFactor >= 0.9) {
					renderTex += tex2D(_MainTex, i.uv + 0.00055);
					//renderTex += tex2D(_MainTex, i.uv + 0.003);
					//renderTex += tex2D(_MainTex, i.uv + 0.005);

					renderTex += tex2D(_MainTex, i.uv - 0.00055);
					//renderTex += tex2D(_MainTex, i.uv - 0.003);
					//renderTex += tex2D(_MainTex, i.uv - 0.005);
					renderTex = renderTex / 3.25;
				}

				

				return renderTex;
			}

			ENDCG
		}
	} 
	FallBack Off
}