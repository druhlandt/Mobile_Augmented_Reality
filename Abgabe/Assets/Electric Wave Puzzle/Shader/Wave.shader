// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// electricity/lightning shader
// pixel shader 2.0 based rendering of electric spark
// by Ori Hanegby
// Free for any kind of use.


Shader "FX/Lightning" {
Properties {
	_SparkDist  ("Spark Distribution", range(-1,1)) = 0


	_MainTex ("MainTex (RGB)", 2D) = "white" {}
	_BGTex ("_BGTex (RGB)", 2D) = "white" {}

	_Noise ("Noise", 2D) = "noise" {}	
	_StartSeed ("StartSeed", Float) = 0

	_Point1 ("_Point1" , range(0.1,1)) = 0.5
	_Point2 ("_Point2" , range(0,10)) = 5

	_color ("_color", Color) = (1, 0, 0, 1)
	_Size ("_Size" , range(0.1,1)) = 0.3
}

Category {

	// We must be transparent, so other objects are drawn before this one.
	Tags { "Queue"="Transparent" }


	SubShader {		
 		
 		// Main pass: Take the texture grabbed above and use the bumpmap to perturb it
 		// on to the screen
 		Blend one one
 		ZWrite off
		Pass {
			Name "BASE"
			Tags { "LightMode" = "Always" }
			
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"

struct appdata_t {
	float4 vertex : POSITION;
	float2 texcoord: TEXCOORD0;
};

struct v2f {	
	float4 vertex : POSITION;	
	float2 uvmain : TEXCOORD0;	
};

float _SparkDist;
float4 _Noise_ST;
float4 _MainTex_ST;
float4 _ObjectScale;
float4 _color;

v2f vert (appdata_t v)
{
	v2f o;
	o.vertex = UnityObjectToClipPos(v.vertex);
	
	o.uvmain = TRANSFORM_TEX( v.texcoord, _MainTex );
	return o;
}

sampler2D _GrabTexture;
float4 _GrabTexture_TexelSize;
sampler2D _Noise;
sampler2D _MainTex;
sampler2D _BGTex;

float _GlowSpread;
float _GlowIntensity;
float _StartSeed;
float _Point1;
float _Point2;
float _Size;

half4 frag( v2f i ) : COLOR
{
	float2 noiseVec = float2(i.uvmain.y + (_Time.x * 15) - (_Point1 * 2), _Time.x);	
	float4 noiseSamp = tex2D( _Noise, noiseVec / 5);
		
	float dvdr = 1.0 - abs(i.uvmain.y - 0.5) * 2;
	dvdr = clamp(dvdr + _SparkDist,0,1);
	
	float fullWidth = 1 - _Size * 2;
	float scaledTexel = (i.uvmain.x - _Size) / fullWidth;
			
	float offs = scaledTexel + ((0.5 - noiseSamp.x) / 50) * (_Point2 * 80);// * dvdr;
	offs = clamp(offs, 0 , 1);
			
	
	float2 texSampVec = float2(offs, i.uvmain.y);
	half4 col = tex2D( _MainTex, texSampVec);

	col *= _color;

	col += tex2D(_BGTex, i.uvmain);
	
	return col;
}
ENDCG
		}
	}


	// ------------------------------------------------------------------
	// Fallback for older cards 	
	SubShader {
		Blend one one
 		ZWrite off
		Pass {
			Name "BASE"
			SetTexture [_MainTex] {	combine texture }
		}
	}
}

}
