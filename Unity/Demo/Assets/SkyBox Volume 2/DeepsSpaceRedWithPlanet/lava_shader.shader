Shader "MyShaders/LavaTestSin"{
	Properties{
		_MainTex("Base(RGB)", 2D) = "white"{}
	}
		SubShader{
		Pass{
		CGPROGRAM

#pragma vertex vert
#pragma fragment frag

		uniform sampler2D _MainTex;


	struct vertexInput {
		float4 pos : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct vertexOutput {
		float4 pos : POSITION;
		float2 uv :TEXCOORD0;
	};


	vertexOutput vert(vertexInput i) {

		vertexOutput o;
		half4 c1 = tex2Dlod(_MainTex, float4(i.uv.x, sin(i.uv.y * +_Time.x), 0, 0));
		half4 c2 = tex2Dlod(_MainTex, float4(1 - i.uv.x * sin(i.uv.y + _Time.x), 1 - i.uv.y, 0, 0));

		o.pos = i.pos;
		o.pos.z += c1.r * c2.r;

		o.pos = mul(UNITY_MATRIX_MVP, o.pos);
		o.uv = i.uv; // für samplen in fragment fucntion da in Fragment function interpoliert

		return o;

	}

	float4 frag(vertexOutput o) : COLOR{
		float4 col = tex2D(_MainTex, o.uv.xy);
		return col;
	}


		ENDCG
	}
	}
}
