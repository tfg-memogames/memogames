Shader "Hidden/Blackout Effect" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
}

SubShader {
	Pass {
		ZTest Always Cull Off ZWrite Off
				
CGPROGRAM
#pragma vertex vert_img
#pragma fragment frag
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
uniform half _RampOffset;

fixed4 frag (v2f_img i) : SV_Target
{
	fixed4 original = tex2D(_MainTex, i.uv);
	fixed4 output = fixed4 (original.r + _RampOffset, original.g + _RampOffset, original.b + _RampOffset, original.a);
	return output;
}
ENDCG

	}
}

Fallback off

}
