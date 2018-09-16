Shader "Hidden/Hont/GridGlithFerryFX"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_NextTex ("NextTex", 2D) = "white" {}
		_NoiseMap("Noise Map", 2D) = "white"{}
		_GridAlpha("Grid Alpha", range(0,1)) = 0
		_NoiseMapRampSlider("Ramp Slider", range(0,1)) = 0
		_NextTexRampSlider("Ramp Slider", range(0,1)) = 0
	}
	
	CGINCLUDE

	#include "UnityCG.cginc"

	struct v2f
	{
		float4 pos : SV_POSITION;
		half2 uv : TEXCOORD0;
	};

	sampler2D _MainTex;
	half4 _MainTex_TexelSize;

	sampler2D _NextTex;
	sampler2D _NoiseMap;
	half _NoiseMapRampSlider;
	half _NextTexRampSlider;
	half _GridAlpha;


	float simSin(float t)
	{
		t = fmod(t, 1);
		t = 4 * (t - t * t);

		return t;
	}

	v2f vert(appdata_img v)
	{
		v2f o = (v2f)0;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	}

#define GRID_SCALE 10
#define EDGE_SIZE 0.003

#define NOISE_SCALE 3
#define NOISE_OFFSET_SCALE 100

	fixed4 frag(v2f i) : SV_Target
	{
		fixed4 result = tex2D(_MainTex, i.uv);
		half ratio = _MainTex_TexelSize.z / _MainTex_TexelSize.w;

		half2 adjustUV = i.uv;
		adjustUV.x = i.uv.x * _MainTex_TexelSize.w / (_MainTex_TexelSize.w / ratio);
		adjustUV.y = i.uv.y;

		half uv_x = floor(adjustUV.x * GRID_SCALE) / GRID_SCALE;
		half uv_y = floor(adjustUV.y * GRID_SCALE) / GRID_SCALE;

		half seed2 = tex2D(_NoiseMap, half2(uv_x, uv_y) + 0.5);
		if (seed2 < _NextTexRampSlider)
		{
			result = tex2D(_NextTex, i.uv);
		}
		else
		{
			half seed = tex2D(_NoiseMap, half2(uv_x, uv_y));
			if (seed < _NoiseMapRampSlider)
			{
				half noise_map_uv_x = simSin(_Time.x);
				half noise_map_uv_y = simSin(_Time.y);
				result = tex2D(_NoiseMap, adjustUV * NOISE_SCALE + half2(noise_map_uv_x, noise_map_uv_y) * NOISE_OFFSET_SCALE);
			}
		}

		if (adjustUV.x - uv_x < EDGE_SIZE || adjustUV.y - uv_y < EDGE_SIZE)
			result = lerp(result, 1.0, _GridAlpha);

		return result;
	}

	ENDCG

	Subshader
	{
		Pass
		{
			ZTest Always Cull Off ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
	}

	Fallback off
}
