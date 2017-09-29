Shader "Hidden/ObscenePixelation"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _GlobalObjectMask;
			sampler2D _NoiseTex;
			float2 _MainTex_TexelSize;
			float2 BlockCount;
			float2 BlockSize;

			int Random (int minV, int maxV, float2 uv)  
			{
				if (minV > maxV)
					return 1;
			
				float cap = maxV - minV;
				int rand = tex2D (_NoiseTex, uv + _Time.x * 2).r * cap + minV;
				return rand;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 mask = tex2D(_GlobalObjectMask, i.uv);

				if(mask.a > 0.0)
				{
					
					float2 blockPos = floor(i.uv * BlockCount);
					float2 blockCenter = blockPos * BlockSize + BlockSize * 0.5;

					int nx = BlockSize.x / _MainTex_TexelSize.x;
					int ny = BlockSize.y / _MainTex_TexelSize.y;

					fixed4 pixelated = 0;

					//for(int i = 0; i < nx; i++)
					//	for(int j = 0; j < ny; j++)
					//	{
					//		uv = blockPos * BlockSize + float2(i, j) * _MainTex_TexelSize;
					//		pixelated += tex2D(_MainTex,uv) / (nx * ny);
					//	}

					int x = Random(0, nx, blockCenter);
					int y = Random(0, ny, blockCenter);
					float2 uv = blockPos * BlockSize + float2(x, y) * _MainTex_TexelSize;
					pixelated = tex2D(_MainTex, uv);
				
					col = pixelated;
				}
				return col;
			}
			ENDCG
		}
	}
}
